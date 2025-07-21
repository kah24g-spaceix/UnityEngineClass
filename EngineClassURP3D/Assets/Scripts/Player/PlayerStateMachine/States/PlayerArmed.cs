using System;
using UnityEngine;

public class PlayerArmed : ShooterStateBase
{
    private Animator m_animator;
    private PlayerMovement m_playerMovement;
    private PlayerCamera m_playerCamera;
    private PlayerWeapon m_weapon;

    private Single m_armedAnimationValue;
    private Single m_aimingAnimationValue;
    public PlayerArmed(PlayerStateMahine pStateMahine) : base(pStateMahine)
    {
        m_playerCamera = pStateMahine.GetComponent<PlayerCamera>();
        m_animator = pStateMahine.GetComponent<Animator>();
        m_playerMovement = pStateMahine.GetComponent<PlayerMovement>();
        m_weapon = pStateMahine.GetComponent<PlayerWeapon>();
    }
    public override void Enter()
    {
        m_weapon.InstallWeapon();
        m_playerCamera.SetAiming(false);
        m_playerMovement.SetIsAiming(false);
        m_armedAnimationValue = m_animator.GetFloat("armed");
        m_aimingAnimationValue = m_animator.GetFloat("aiming");
    }
    public override void Update()
    {
        m_armedAnimationValue = Mathf.Lerp(m_armedAnimationValue, 1, 10 * Time.deltaTime);
        m_animator.SetFloat("armed", m_armedAnimationValue);

        m_aimingAnimationValue = Mathf.Lerp(m_aimingAnimationValue, 0, 10 * Time.deltaTime);
        m_animator.SetFloat("aiming", m_aimingAnimationValue);

        if (Input.GetKeyDown(KeyCode.F))
        {
            StateMachine.ChangeState(PlayerShooterState.NotArmed);
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StateMachine.ChangeState(PlayerShooterState.Reloading);
            return;
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            StateMachine.ChangeState(PlayerShooterState.Aiming);
            return;
        }
    }
}