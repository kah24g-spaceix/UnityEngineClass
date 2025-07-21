
using System;
using UnityEngine;
[CreateAssetMenu(
    fileName = "EnemyData",
    menuName = "Data/EnemyData",
    order = Int32.MinValue)]
public class EnemyData : ScriptableObject
{
    [SerializeField] private Vector2 m_idleTimeRange;

    [SerializeField] private Vector2 m_wanderSpeedRange;
    [SerializeField] private Vector2 m_wanderTimeRange;

    [SerializeField] private Vector2 m_chaseSpeedRange;

    public Single IdleTime => GetRandomInRange(m_idleTimeRange);
    public Single WanderSpeed => GetRandomInRange(m_wanderSpeedRange);
    public Single WanderTime => GetRandomInRange(m_wanderTimeRange);
    public Single ChaseSpeed => GetRandomInRange(m_chaseSpeedRange);


    private Single GetRandomInRange(Vector2 pRange)
    {
        return UnityEngine.Random.Range(pRange.x, pRange.y);
    }
}