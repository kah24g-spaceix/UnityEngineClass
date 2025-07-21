using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class ShooterPlayerController : MonoBehaviour
{
    private static readonly KeyCode[] PATTERN_KEYS = { KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B };

    [SerializeField] private Single m_playerSpeed;

    private Rigidbody2D m_rigidbody;
    private ShooterPattern[] m_patterns;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_patterns = GetComponents<ShooterPattern>();
    }

    void Update()
    {
        Single xInput = Input.GetAxisRaw("Horizontal");
        Single yInput = Input.GetAxisRaw("Vertical");

        m_rigidbody.velocity = new Vector2(xInput, yInput).normalized * m_playerSpeed;

        for(Int32 i=0;i<PATTERN_KEYS.Length;i++)
        {
            if (m_patterns.Length <= i)
                break;

            if (Input.GetKeyDown(PATTERN_KEYS[i]))
                m_patterns[i].Shoot();
        }
    }
}
