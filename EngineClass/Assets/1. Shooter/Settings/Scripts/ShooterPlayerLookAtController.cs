using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPlayerLookAtController : MonoBehaviour
{
    [SerializeField] private Single m_lookAtDelay;
    [SerializeField] private Transform m_target;

    private Single m_elapsed;

    private void Update()
    {
        m_elapsed += Time.deltaTime;
        if(m_elapsed >= m_lookAtDelay)
        {
            m_elapsed = 0;
            transform.LookAt2D(m_target.position);
        }
    }
}
