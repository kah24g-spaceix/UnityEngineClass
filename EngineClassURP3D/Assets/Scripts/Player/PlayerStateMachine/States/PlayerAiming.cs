using System;
using UnityEngine;

public class PlayerAiming : ShooterStateBase
{
    private Animator m_animator;
    private Single m_aimingAnimationValue;
    private PlayerMovement m_playerMovement;
    private PlayerCamera m_playerCamera;
    public PlayerAiming(PlayerStateMahine pStateMahine) : base(pStateMahine)
    {
        m_playerCamera = pStateMahine.GetComponent<PlayerCamera>();
        m_playerMovement = pStateMahine.GetComponent<PlayerMovement>();
        m_animator = pStateMahine.GetComponent<Animator>();
    }
    public override void Enter()
    {
        m_playerCamera.SetAiming(true);
        m_playerMovement.SetIsAiming(true);
        m_aimingAnimationValue = m_animator.GetFloat("aiming");
    }
    public override void Update()
    {
        m_aimingAnimationValue = Mathf.Lerp(m_aimingAnimationValue, 1, 10 * Time.deltaTime);
        m_animator.SetFloat("aiming", m_aimingAnimationValue);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            StateMachine.ChangeState(PlayerShooterState.Shooting);
            return;
        }
        if (!Input.GetKey(KeyCode.Mouse1))
        {
            StateMachine.ChangeState(PlayerShooterState.Armed);
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StateMachine.ChangeState(PlayerShooterState.Reloading);
            return;
        }
    }
}