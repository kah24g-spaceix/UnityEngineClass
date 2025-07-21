using System;
using System.Collections.Generic;
using UnityEngine;

public class DataTableAsset<T> : ScriptableObject where T: DataTableRow
{
    [SerializeField] protected DataTable<T> m_dataTable;
    public DataTable<T> DataTable => m_dataTable;
}