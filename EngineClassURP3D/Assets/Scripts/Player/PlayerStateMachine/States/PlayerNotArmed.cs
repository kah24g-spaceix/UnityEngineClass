using System;
using UnityEngine;

public class PlayerNotArmed : ShooterStateBase
{
    private Animator m_animator;
    private PlayerMovement m_playerMovement;
    private PlayerWeapon m_playerWeapon;

    private Single m_armedAnimationValue;
    public PlayerNotArmed(PlayerStateMahine pStateMahine) : base(pStateMahine)
    {
        m_animator = pStateMahine.GetComponent<Animator>();
        m_playerMovement = pStateMahine.GetComponent<PlayerMovement>();
        m_playerWeapon = pStateMahine.GetComponent<PlayerWeapon>();
    }
    public override void Enter()
    {
        m_playerWeapon.UnInstallWeapon();
        m_playerMovement.SetIsAiming(false);
        m_armedAnimationValue = m_animator.GetFloat("armed");
    }
    public override void Update()
    {
        m_armedAnimationValue = Mathf.Lerp(m_armedAnimationValue, 0, 10 * Time.deltaTime);
        m_animator.SetFloat("armed", m_armedAnimationValue);

        if (Input.GetKeyDown(KeyCode.F))
            StateMachine.ChangeState(PlayerShooterState.Armed);
    }
}