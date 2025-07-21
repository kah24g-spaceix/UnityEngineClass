using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Single m_playerSpeed = 10;

    private Rigidbody2D m_rigidbody;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Single xInput = Input.GetAxisRaw("Horizontal");
        Single yInput = Input.GetAxisRaw("Vertical");

        m_rigidbody.velocity = new Vector2(xInput, yInput).normalized * m_playerSpeed;
    }
}
