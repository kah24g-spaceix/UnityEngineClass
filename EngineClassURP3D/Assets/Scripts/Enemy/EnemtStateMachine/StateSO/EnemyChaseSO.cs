using System;
using UnityEngine;

[CreateAssetMenu(
    fileName = "EnemyChase",
    menuName = "State/Enemy/Chase",
    order = Int32.MinValue)]
public class EnemyChaseSO : EnemyStateSO
{
    public override EnemyStateBase CreateState(EnemyStateMachine pStateMachine)
    {
        return new EnemyChase(pStateMachine);
    }
}