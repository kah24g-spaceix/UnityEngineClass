using System;
using Unity.VisualScripting;
using UnityEngine;
public class EnemyChase : EnemyStateBase
{
    private IDetector m_detector;
    private EnemyMovement m_movement;
    private EnemyModel m_model;
    
    private GameObject m_target;
    private Single m_chaseSpeed;
    public EnemyChase(EnemyStateMachine pStateMachine)
        : base(pStateMachine)
    {
        m_detector = pStateMachine
            .GetComponent<IDetector>();
        m_movement = pStateMachine
            .GetComponent<EnemyMovement>();
        m_model = pStateMachine
            .GetComponent<EnemyModel>();

    }
    public override void Enter()
    {
        m_chaseSpeed = m_model.ChaseSpeed;
        if (m_detector.Detect(out var target))
        {
            m_target = target;
        }
        else
        {
            Debug.LogWarning($"target not detected but state is changed to chase");
            StateMachine.ChangeState(EnemyState.Idle);
        }
    }

    public override void Update()
    {
        if (m_detector.Detect(out var target))
        {
            m_target = target;
        }
        else
        {
            StateMachine.ChangeState(EnemyState.Idle);
            return;
        }
        Vector3 moveVector = m_target.transform.position - StateMachine.transform.position;
        moveVector.y = 0;
        moveVector.Normalize();
        
        m_movement.Move(moveVector * m_chaseSpeed);
    }
}