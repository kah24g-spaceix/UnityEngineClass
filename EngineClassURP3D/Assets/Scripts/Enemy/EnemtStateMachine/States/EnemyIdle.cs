using System;
using UnityEngine;
public class EnemyIdle : EnemyStateBase
{
    private EnemyModel m_enemyModel;
    private IDetector m_detector;
    private Single m_leftIdleTime;
    public EnemyIdle(EnemyStateMachine pStateMachine)
        : base(pStateMachine)
    {
        m_enemyModel 
            = pStateMachine.GetComponent<EnemyModel>();
        m_detector
            = pStateMachine.GetComponent<IDetector>();
}
    public override void Enter()
    {
        m_leftIdleTime = m_enemyModel.IdleTime;
        if (m_leftIdleTime <= 0)
        {
            StateMachine.ChangeState(EnemyState.Wander);
        }
    }
    public override void Update() 
    {
        m_leftIdleTime -= Time.deltaTime;
        if (m_leftIdleTime <= 0)
        {
            StateMachine.ChangeState(EnemyState.Wander);
        }
        if (m_detector.Detect(out var _))
        {
            StateMachine.ChangeState(EnemyState.Chase);
        }
    }
}