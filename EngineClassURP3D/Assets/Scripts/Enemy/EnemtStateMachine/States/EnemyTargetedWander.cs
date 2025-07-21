using System;
using UnityEngine;
public class EnemyTargetedWander : EnemyStateBase
{
    private EnemyMovement m_movement;
    private EnemyWanderTargetProvider m_targetProvider;
    private EnemyModel m_model;

    private IDetector m_detector;

    private Vector3 m_targetPosition;
    private Single m_wanderSpeed;
    public EnemyTargetedWander(EnemyStateMachine pStateMachine)
        : base(pStateMachine)
    {
        m_movement 
            = pStateMachine.GetComponent<EnemyMovement>();
        m_targetProvider 
            = pStateMachine.GetComponent<EnemyWanderTargetProvider>();
        m_model 
            = pStateMachine.GetComponent<EnemyModel>();

        m_detector
    = pStateMachine.GetComponent<IDetector>();
    }
    public override void Enter()
    {
        m_targetPosition = m_targetProvider.GetNextWanderTarget();
        m_wanderSpeed = m_model.WanderSpeed;
    }

    public override void Update()
    {
        Vector3 current
            = StateMachine.transform.position;
        current.y = 0;
        Single distance = Vector3.Distance(current, m_targetPosition);

        if (distance <= 0.001f)
        {
            StateMachine.transform.position 
                = new Vector3(m_targetPosition.x,
                StateMachine.transform.position.y,
                m_targetPosition.z);
            StateMachine.ChangeState(EnemyState.Idle);
        }
        Vector3 moveVector = m_targetPosition - StateMachine.transform.position;
        moveVector.y = 0;
        moveVector.Normalize();

        m_movement.Move(moveVector * m_wanderSpeed);

        if (m_detector.Detect(out var _))
        {
            StateMachine.ChangeState(EnemyState.Chase);
        }

    }
}