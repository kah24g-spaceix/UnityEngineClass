using System;
using UnityEditor;
using UnityEngine;

public class PlayerReloading : ShooterStateBase
{
    private Animator m_animator;
    private PlayerWeapon m_playerWeapon;

    private Single m_leftTime; // 남은 재장전 시간
    
    public PlayerReloading(PlayerStateMahine pStateMahine) : base(pStateMahine)
    {
        m_animator = pStateMahine.GetComponent<Animator>();
        m_playerWeapon = pStateMahine.GetComponent<PlayerWeapon>();
    }

    public override void Enter()
    {
        m_leftTime = m_playerWeapon.ReloadTime;
        m_animator.SetFloat("reloadSpeed", m_playerWeapon.ReloadAnimationSpeed);
        m_animator.SetTrigger("reload");
    }
    public override void Update()
    {
        m_leftTime -= Time.deltaTime;
        if (m_leftTime <= 0)
            OnReloadFinished();
    }
    private void OnReloadFinished()
    {
        m_playerWeapon.ReloadBullet();
        if (Input.GetKey(KeyCode.Mouse1))
            StateMachine.ChangeState(PlayerShooterState.Aiming);
        else
            StateMachine.ChangeState(PlayerShooterState.Armed);
    }
}