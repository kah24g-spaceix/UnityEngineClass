public class ShooterStateBase : StateBase
{
    private PlayerStateMahine m_stateMahine;
    protected PlayerStateMahine StateMachine => m_stateMahine;

    public ShooterStateBase(PlayerStateMahine pStateMahine)
    {
        m_stateMahine = pStateMahine;
    }
}