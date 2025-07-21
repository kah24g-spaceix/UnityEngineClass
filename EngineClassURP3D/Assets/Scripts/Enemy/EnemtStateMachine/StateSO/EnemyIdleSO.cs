using System;
using UnityEngine;

[CreateAssetMenu(
    fileName = "EnemyIdle",
    menuName = "State/Enemy/Idle",
    order = Int32.MinValue)]
public class EnemyIdleSO : EnemyStateSO
{
    public override EnemyStateBase CreateState(EnemyStateMachine pStateMachine)
    {
        return new EnemyIdle(pStateMachine);
    }
}