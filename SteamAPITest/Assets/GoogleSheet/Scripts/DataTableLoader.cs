using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class DataTableLoder<TRow> where TRow : DataTableRow
{
    private static DataTable<TRow> m_table;

    public static DataTable<TRow> GetTable()
    {
        if (m_table == null)
        {
            var assets = Resources.LoadAll<DataTableAsset<TRow>>("");
            if (assets.Length == 0)
            {
                Debug.LogError($"no datatable<{typeof(TRow)}> in Resources");
                return null;
            }
            m_table = assets[0].DataTable;
        }

        return m_table;
    }
}