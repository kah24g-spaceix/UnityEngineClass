using System;
using System.Collections.Generic;
using UnityEngine;
using SimpleTableGen;

public class TableTest : MonoBehaviour
{
    [SerializeField] private Int32 m_id;
    private DataTable<TestTableRow> m_table;

    private void Awake()
    {
        m_table = DataTableLoder<TestTableRow>.GetTable();
    }
    private void Update()
    {
           if (Input.GetKeyDown(KeyCode.Space))
        {
            var row = m_table[m_id];
            if (row == null) return;
            Debug.Log($"ID : {row.ID}, {row.Attack}, {row.Deffence}, {row.Lore}");
        }
    }
}