using System;
using UnityEngine;
public class BoxDetector : MonoBehaviour, IDetector
{
    [SerializeField] private LayerMask m_targetLayer;
    [SerializeField] private Vector3 m_offset;
    [SerializeField] private Vector3 m_size;

    private Collider[] m_results = new Collider[5];
    public bool Detect(out GameObject pTarget)
    {
        Int32 count = Physics.OverlapBoxNonAlloc(
            transform.position + transform.rotation * m_offset,
            m_size / 2,
            m_results,
            transform.rotation,
            m_targetLayer.value);

        if (count != 0)
        {
            pTarget = m_results[0].gameObject;
            return true;
        }
        pTarget = null;
        return false;
    
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(m_offset, m_size);
        
    }
}