using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpreadSheetSetting))]
public class SpreadSheetSrttingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SpreadSheetSetting setting = (SpreadSheetSetting)target;

        if (GUILayout.Button("Generate Code"))
        {
            SpreadSheetCSVLoader loader = new SpreadSheetCSVLoader(
                setting.SheetAPIKey,
                setting.SheetID);

            TableGeneration.GenerateCode(
                setting,
                loader,
                setting.SheetNames);
            // List<List<String>> data = loader.LoadCSV(setting.SheetNames[0]);
            // Debug.Log(data[0][0]);
        }
        if (GUILayout.Button("Sync Data"))
        {
            SpreadSheetCSVLoader loader = new SpreadSheetCSVLoader(
                setting.SheetAPIKey,
                setting.SheetID);

            TableGeneration.SyncData(
                setting,
                loader,
                setting.SheetNames
                );
        }
    }
}