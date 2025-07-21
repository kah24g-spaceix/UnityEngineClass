using System;
using UnityEngine;


public abstract class EnemyStateSO : ScriptableObject
{
    [SerializeField] private EnemyState m_state;

    public EnemyState State => m_state;
    public abstract EnemyStateBase CreateState(EnemyStateMachine pStateMachine);

}