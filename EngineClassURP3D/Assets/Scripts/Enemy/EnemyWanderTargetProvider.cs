using System;
using System.Collections.Generic;
using UnityEngine;
public class EnemyWanderTargetProvider : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_wanderTargets;

    private Int32 m_index;

    public Vector3 GetNextWanderTarget()
    {
        if (m_wanderTargets.Count <= m_index)
            m_index = 0;

        GameObject target = m_wanderTargets[m_index];
        m_index++;
        Vector3 targetPos = target.transform.position;
        targetPos.y = 0;
        return targetPos;
    }
}