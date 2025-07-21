using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Int32 m_health;

    private EnemyStateMachine m_enemyStateMachine;

    private void Awake()
    {
        m_enemyStateMachine = GetComponent<EnemyStateMachine>();
    }
    public void TakeDamage(Int32 pDamage)
    {
        if (pDamage < 0)
        {
            Debug.LogWarning(
                $"[{pDamage}] is invalid damage. " +
                $"damage must be over 0");
            return;
        }
        m_health -= pDamage;
        if (m_health <= 0)
        {
            Kill();
        }
    }
    public void Kill()
    {
        m_health = 0;
        OnDeath();
    }
    private void OnDeath()
    {
        m_enemyStateMachine.ChangeState(EnemyState.Death);
    }
}
