using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipController: MonoBehaviour
{
    private Single m_time;
    private SpriteRenderer m_spriteRenderer;

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        m_time += Time.deltaTime;
        if(m_time >= 0.2f)
        {
            m_spriteRenderer.flipX = !m_spriteRenderer.flipX;
            m_time = 0;
        }
    }
}
