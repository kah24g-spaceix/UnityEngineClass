using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBullet : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    

    private void Awake()
    {
        m_rigidbody =GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// ź���� �ӵ��� �����Ѵ�. 
    /// </summary>
    /// <param name="pVelocity">�ӵ����� �����Ѵ�.</param>
    public void SetVelocity(Vector2 pVelocity)
    {
        m_rigidbody.velocity = pVelocity;
    }

}
