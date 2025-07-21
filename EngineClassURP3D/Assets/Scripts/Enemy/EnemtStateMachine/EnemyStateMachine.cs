using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle, // 가만히있는 상태
    Wander, // 돌아다니는 상태
    Chase, // 플레이어 발견 추격
    Death
}

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyStateSO[] m_stateSO;
    private Dictionary<EnemyState, EnemyStateBase> m_stateByKey;
    private StateMachine m_stateMachine;
    private EnemyState m_currentState;

    public EnemyState CurrentState => m_currentState;

    private void Awake()
    {
        m_stateMachine = new StateMachine();
        m_stateByKey = new Dictionary<EnemyState, EnemyStateBase>();

        foreach(var stateSO in m_stateSO)
        {
            EnemyStateBase state = stateSO.CreateState(this);
            m_stateByKey.Add(stateSO.State, state);
        }

        m_currentState = EnemyState.Idle;
        m_stateMachine.Init(m_stateByKey[m_currentState]);

    }
    private void Update()
    {
        m_stateMachine.Update();
    }
    public void ChangeState(EnemyState pState)
    {
        if (m_currentState == pState)
            return;
        m_currentState = pState;
        m_stateMachine.ChangeState(m_stateByKey[pState]);
    }
}
