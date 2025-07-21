using System;
using UnityEngine;

[CreateAssetMenu(
    fileName = "EnemyDeath",
    menuName = "State/Enemy/Death",
    order = Int32.MinValue)]
public class EnemyDeathSO : EnemyStateSO
{
    public override EnemyStateBase CreateState(EnemyStateMachine pStateMachine)
    {
        return new EnemyDeath(pStateMachine);
    }
}