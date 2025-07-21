using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum PlayerShooterState
{
    NotArmed,
    Armed,
    Reloading,
    Aiming,
    Shooting
}
public class PlayerStateMahine : MonoBehaviour
{
    private Dictionary<PlayerShooterState, ShooterStateBase> m_stateByKey;
    private StateMachine m_stateMachine;
    private PlayerShooterState m_currentState;

    public PlayerShooterState CurrentState => m_currentState;

    private void Awake()
    {
        m_stateMachine = new StateMachine();
        m_stateByKey = new Dictionary<PlayerShooterState, ShooterStateBase>();

        m_stateByKey.Add(PlayerShooterState.NotArmed, new PlayerNotArmed(this));
        m_stateByKey.Add(PlayerShooterState.Armed, new PlayerArmed(this));
        m_stateByKey.Add(PlayerShooterState.Reloading, new PlayerReloading(this));
        m_stateByKey.Add(PlayerShooterState.Aiming, new PlayerAiming(this));
        m_stateByKey.Add(PlayerShooterState.Shooting, new PlayerShooting(this));

        m_currentState = PlayerShooterState.NotArmed;
        m_stateMachine.Init(m_stateByKey[m_currentState]);
    }
    private void Update()
    {
        m_stateMachine.Update();
    }
    public void ChangeState(PlayerShooterState pState)
    {
        if (m_currentState == pState)
            return;
        m_currentState = pState;
        m_stateMachine.ChangeState(m_stateByKey[pState]);
    }
}