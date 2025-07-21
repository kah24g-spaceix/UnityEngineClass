public class StateMachine
{
    private StateBase m_currentState;

    public void Init(StateBase pState)
    {
        m_currentState = pState;
        m_currentState.Enter();
    }
    public void ChangeState(StateBase pState)
    {
        m_currentState?.Exit();
        m_currentState = pState;
        m_currentState.Enter();
    }

    public void Update()
    {
        m_currentState?.Update();
    }
}