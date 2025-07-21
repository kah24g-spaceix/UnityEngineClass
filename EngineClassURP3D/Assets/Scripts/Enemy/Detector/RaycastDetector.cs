using System;
using UnityEngine;

public class RaycastDetector : MonoBehaviour, IDetector
{
    [SerializeField] private Single m_angle;
    [SerializeField] private Single m_distance;

    [SerializeField] private Int32 m_rayCount;
    [SerializeField] private LayerMask m_layerMask;
    public bool Detect(out GameObject pTarget)
    {
        Single startAngle = -(m_angle / 2);
        Single angleStep = m_angle / m_rayCount;

        for (Int32 i = 0; i < m_rayCount; i++)
        {
            Single currentAngle = startAngle 
                + angleStep * i;

            Vector3 direction = Quaternion.Euler(0, currentAngle, 0) * transform.forward;

            Debug.DrawRay(
                transform.position, 
                direction * m_distance, 
                Color.red);
        
            if(Physics.Raycast(
                transform.position, 
                direction, 
                out var hit,
                m_distance,
                m_layerMask.value))
            {
                pTarget = hit.transform.gameObject;
                return true;
            }
        }

        pTarget = null;
        return false;
    }
}