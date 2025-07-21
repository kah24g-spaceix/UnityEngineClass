using UnityEngine;

public class EnemyDeath : EnemyStateBase
{
    public EnemyDeath(EnemyStateMachine pStateMachine)
        : base(pStateMachine)
    {

    }
    public override void Enter()
    {
        GameObject.Destroy(StateMachine.gameObject);
    }
}