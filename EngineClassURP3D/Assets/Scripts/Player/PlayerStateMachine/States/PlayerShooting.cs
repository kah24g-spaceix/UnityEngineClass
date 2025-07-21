using System;
using UnityEngine;

public class PlayerShooting : ShooterStateBase
{
    private PlayerWeapon m_playerWeapon;
    private Single m_leftDelay;
    public PlayerShooting(PlayerStateMahine pStateMahine) : base(pStateMahine)
    {
        m_playerWeapon = pStateMahine.GetComponent<PlayerWeapon>();
    }
    public override void Update()
    {
        if (!Input.GetKey(KeyCode.Mouse0))
        {
            StateMachine.ChangeState(PlayerShooterState.Aiming);
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StateMachine.ChangeState(PlayerShooterState.Reloading);
            return;
        }
        if (m_playerWeapon.CurrentBullet <= 0)
        {
            StateMachine.ChangeState(PlayerShooterState.Aiming);
            return;
        }
        m_leftDelay -= Time.deltaTime;
        if (m_leftDelay <= 0)
        {
            m_leftDelay = m_playerWeapon.ShootDelay;
            m_playerWeapon.OnShoot();
        }
    }
}