using System;
using System.Collections.Generic;
using UnityEngine;

public class TableGenSetting : ScriptableObject
{
    [SerializeField] private String m_nameSpace
    = "SimpleTableGen";
    [SerializeField] private String m_codeGenPath
    = "Assets/SimpleTableGen/Generated/Scripts";
    [SerializeField] private String m_assetPath
    = "Assets/SimpleTableGen/Generated/Assets";

    public String NameSpace => m_nameSpace;
    public String CodeGenPath => m_codeGenPath;
    public String AssetPath => m_assetPath;
}