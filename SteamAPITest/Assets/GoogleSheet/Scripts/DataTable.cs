using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataTableRow
{
    [SerializeField] protected Int32 m_ID;
    public Int32 ID => m_ID;
}
[Serializable]
public class DataTable<T> where T: DataTableRow
{
    [SerializeField] private SerializableDictionary<Int32, T> m_rowById;
    public Boolean ContainsID(Int32 pID) => m_rowById.ContainsKey(pID);
    public T Get(Int32 pID)
    {
        if (m_rowById.TryGetValue(pID, out T row))
        {
            return row;
        }
        Debug.LogError($"{pID} is not included in table");
        return null;
    }
    public T this[Int32 pID] => Get(pID);
}