using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

using System.CodeDom;
using System.CodeDom.Compiler;

using UnityEngine;
using UnityEditor;

using UObject = UnityEngine.Object;
using SObject = System.Object;
using System.Runtime.Remoting.Messaging;

public class TableGeneration
{
    public static void GenerateCode(
        TableGenSetting pSetting,
        ICSVLoader pCSVLoader,
        List<String> pSheetNames)
    {
        foreach (String sheetName in pSheetNames)
        {
            List<List<String>> data = pCSVLoader.LoadCSV(sheetName);
            List<(String, String)> headers = new List<(string, string)>();

            for (Int32 i = 0; i < data[0].Count; i++)
                headers.Add((data[0][i], data[1][i]));

            GenerateCode(pSetting, sheetName, headers);
        }

        AssetDatabase.Refresh();
    }

    private static void GenerateCode(
        TableGenSetting pSetting,
        String pSheetName,
        List<(String, String)> pSheetHeader)
    {
        CodeTypeDeclaration rowDelaration = new CodeTypeDeclaration($"{pSheetName}Row");
        rowDelaration.BaseTypes.Add((typeof(DataTableRow)));
        rowDelaration.IsClass = true;
        rowDelaration.CustomAttributes.Add(
            new CodeAttributeDeclaration(
                new CodeTypeReference(typeof(SerializableAttribute))
            )
        );

        foreach (var header in pSheetHeader)
        //header.Item2 "Int32"
        {
            if (header.Item1.Equals("ID"))
                continue;
            Type type = Type.GetType($"System.{header.Item2}");
            if (type == null)
                type = Type.GetType(header.Item2);
            if (type == null)
                type = Type.GetType($"{header.Item2}, Assembly-CSharp");
            if (type == null)
            {
                Debug.LogError($"Type {header.Item2} not found");
                continue;
            }

            CodeMemberField field = new CodeMemberField();
            field.Name = $"m_{header.Item1}";
            field.Type = new CodeTypeReference(type);
            field.Attributes = MemberAttributes.Private;
            field.CustomAttributes.Add(
                new CodeAttributeDeclaration(
                    new CodeTypeReference(typeof(SerializeField))
                )
            );
            rowDelaration.Members.Add(field);

            CodeMemberProperty property = new CodeMemberProperty();
            property.Name = header.Item1;
            property.Type = new CodeTypeReference(type);
            property.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            property.HasGet = true;
            property.HasSet = false;
            property.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(),
                        field.Name
                    )
                )
            );

            rowDelaration.Members.Add(property);
        }

        CodeTypeDeclaration assetDeclaration = new CodeTypeDeclaration($"{pSheetName}Asset");
        CodeTypeReference assetTypeReference = new CodeTypeReference(typeof(DataTableAsset<>));
        assetTypeReference.TypeArguments.Add(new CodeTypeReference($"{pSetting.NameSpace}.{pSheetName}Row"));
        assetDeclaration.BaseTypes.Add(assetTypeReference);
        assetDeclaration.IsClass = true;

        CodeNamespace codeNamespace = new CodeNamespace(pSetting.NameSpace);
        codeNamespace.Types.Add(rowDelaration);
        codeNamespace.Types.Add(assetDeclaration);

        CodeCompileUnit compileUnit = new CodeCompileUnit();
        compileUnit.Namespaces.Add(codeNamespace);

        CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

        if (!Directory.Exists(pSetting.CodeGenPath))
            Directory.CreateDirectory(pSetting.CodeGenPath);

        String filePath = Path.Combine(pSetting.CodeGenPath, $"{pSheetName}Asset.cs");
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            provider.GenerateCodeFromCompileUnit(compileUnit, sw, new CodeGeneratorOptions());
        }

        Debug.Log($"Generated {pSheetName}");
    }
    public static void SyncData(
        TableGenSetting pSetting,
        ICSVLoader pCSVLoader,
        List<String> pSheetNames)
    {
        foreach (String sheetName in pSheetNames)
        {
            List<List<String>> data = pCSVLoader.LoadCSV(sheetName);
            List<(String, String)> headers = new List<(string, string)>();

            for (Int32 i = 0; i < data[0].Count; i++)
                headers.Add((data[0][i], data[1][i]));

            SyncData(pSetting, sheetName, data, headers);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static SObject ParseString(String pString, Type pType)
    {
        if (pType == typeof(String))
            return pString;
        if (pType.IsEnum)
            return Enum.Parse(pType, pString);

        MethodInfo parseMethod = pType.GetMethod("Parse", new Type[] { typeof(String) });
        if (parseMethod == null)
        {
            Debug.LogError($"could not find parse method from {pType.Name}");
            return null;
        }

        return parseMethod.Invoke(null, new SObject[] { pString });
    }
    private static void SyncData(
        TableGenSetting pSetting,
        String pSheetName,
        List<List<String>> pData,
        List<(String, String)> pHeader)
    {
        Type rowType
            = Type.GetType($"{pSetting.NameSpace}.{pSheetName}Row,Assembly-CSharp");
        Type assetType
            = Type.GetType($"{pSetting.NameSpace}.{pSheetName}Asset,Assembly-CSharp");
        if (rowType == null || assetType == null)
        {
            Debug.LogError($"Could not find type of row or asset, please genearte code first.");
            return;
        }

        String path = Path.Combine(pSetting.AssetPath, $"{pSheetName}Asset.asset");
        UObject asset = AssetDatabase.LoadAssetAtPath<UObject>(path); // Only Editor
        if (asset == null)
        {
            asset = ScriptableObject.CreateInstance(assetType);

            if (!Directory.Exists(pSetting.AssetPath))
                Directory.CreateDirectory(pSetting.AssetPath);

            AssetDatabase.CreateAsset(asset, path);
        }

        if (pData.Count <= 2)
        {
            Debug.LogError($"data is not enough to generate asset");
            return;
        }

        List<(Int32, SObject)> tableValues = new List<(int, SObject)>();
        for (Int32 i = 2; i < pData.Count; i++)
        {
            SObject row = Activator.CreateInstance(rowType);
            Int32 id = 0;

            for (Int32 j = 0; j < pData[i].Count; j++)
            {
                if (pHeader[j].Item1.Equals("ID"))
                    id = Int32.Parse(pData[i][j]);

                FieldInfo fieldInfo = rowType.GetField(
                    $"m_{pHeader[j].Item1}",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                if (fieldInfo == null)
                {
                    Debug.LogError(
                        $"could not find field m_{pHeader[j].Item1} from {rowType.Name}");
                    continue;
                }
                
                SObject parsedValue = ParseString(pData[i][j], fieldInfo.FieldType);
                fieldInfo.SetValue(row, parsedValue);
            }
            tableValues.Add((id, row));
        }

        FieldInfo tableField = assetType
            .GetField("m_dataTable", BindingFlags.NonPublic | BindingFlags.Instance);
        SObject table = tableField.GetValue(asset);

        FieldInfo dictionaryField = table
            .GetType()
            .GetField("m_rowById", BindingFlags.NonPublic | BindingFlags.Instance);
        SObject dictionary = dictionaryField.GetValue(table);
        MethodInfo clearMethod = dictionary
            .GetType()
            .GetMethod("Clear", BindingFlags.Public | BindingFlags.Instance);
        clearMethod.Invoke(dictionary, null);
        
        MethodInfo addMethod = dictionary
            .GetType()
            .GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);
        foreach (var idRowTuple in tableValues)
            addMethod.Invoke(dictionary, new SObject[] { idRowTuple.Item1, idRowTuple.Item2});

        EditorUtility.SetDirty(asset);
        Debug.Log($"Sync {pSheetName}");
    }
}