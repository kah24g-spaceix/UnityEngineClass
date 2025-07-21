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
    /// 탄막의 속도를 설정한다. 
    /// </summary>
    /// <param name="pVelocity">속도값을 지정한다.</param>
    public void SetVelocity(Vector2 pVelocity)
    {
        m_rigidbody.velocity = pVelocity;
    }

}
