using System;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    [SerializeField] private EnemyData m_enemyData;

    public Single IdleTime => m_enemyData.IdleTime;
    public Single WanderSpeed => m_enemyData.WanderSpeed;
    public Single WanderTime => m_enemyData.WanderTime;
    public Single ChaseSpeed => m_enemyData.ChaseSpeed;
}