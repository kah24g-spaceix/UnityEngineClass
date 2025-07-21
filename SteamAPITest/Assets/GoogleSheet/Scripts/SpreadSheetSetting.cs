using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "SpreadSheetSetting",
    menuName = "SimpleTableGen/SpreadSheetSetting")]
public class SpreadSheetSetting : TableGenSetting
{
    [SerializeField] private String m_sheetID;
    [SerializeField] private String m_sheetAPIKey;
    [SerializeField] private List<String> m_sheetNames;

    public String SheetID => m_sheetID;
    public String SheetAPIKey => m_sheetAPIKey;
    public List<String> SheetNames => m_sheetNames;
}