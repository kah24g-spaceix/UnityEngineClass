using System;
using UnityEngine;

[CreateAssetMenu(
    fileName = "EnemyWander",
    menuName = "State/Enemy/Wander",
    order = Int32.MinValue)]
public class EnemyWanderSO : EnemyStateSO
{
    public override EnemyStateBase CreateState(EnemyStateMachine pStateMachine)
    {
        return new EnemyWander(pStateMachine);
    }
}