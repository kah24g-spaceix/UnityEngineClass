using System;
using UnityEngine;

public class EnemyWander : EnemyStateBase
{
    private EnemyModel m_model;
    private EnemyMovement m_movement;
    private IDetector m_detector;

    private Single m_leftWanderTime;
    private Vector3 m_moveVector;
    public EnemyWander(EnemyStateMachine pStateMachine)
        : base(pStateMachine)
    {
        m_model = pStateMachine.GetComponent<EnemyModel>();
        m_movement = pStateMachine.GetComponent<EnemyMovement>();
        m_detector
            = pStateMachine.GetComponent<IDetector>();
    }

    public override void Enter()
    {
        m_leftWanderTime = m_model.WanderTime;
        Vector2 random = UnityEngine.Random.insideUnitCircle;
        m_moveVector = new Vector3(random.x, 0, random.y) * m_model.WanderSpeed;
    }
    public override void Update() 
    {
        m_leftWanderTime -= Time.deltaTime;
        if (m_leftWanderTime < 0)
        {
            StateMachine.ChangeState(EnemyState.Idle);
            return;
        }

        if (m_detector.Detect(out var _))
        {
            StateMachine.ChangeState(EnemyState.Chase);
        }

        Debug.DrawRay(StateMachine.transform.position, m_moveVector);
        m_movement.Move(m_moveVector);
    }
}