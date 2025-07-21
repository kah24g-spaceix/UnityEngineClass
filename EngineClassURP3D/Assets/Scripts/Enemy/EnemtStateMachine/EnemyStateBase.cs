public class EnemyStateBase : StateBase
{
    private EnemyStateMachine m_stateMachine;

    protected EnemyStateMachine StateMachine => m_stateMachine;

    public EnemyStateBase(
        EnemyStateMachine stateMachine)
    {
        m_stateMachine = stateMachine;
    }
}