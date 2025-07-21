using System;
using UnityEngine;

[CreateAssetMenu(
    fileName = "EnemyTargetedWander",
    menuName = "State/Enemy/TargetedWander",
    order = Int32.MinValue)]
public class EnemyTargetedWanderSO : EnemyStateSO
{
    public override EnemyStateBase CreateState(EnemyStateMachine pStateMachine)
    {
        return new EnemyTargetedWander(pStateMachine);
    }
}