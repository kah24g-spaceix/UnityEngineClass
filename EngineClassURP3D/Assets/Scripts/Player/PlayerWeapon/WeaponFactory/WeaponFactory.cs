using System;
using UnityEngine;
public abstract class WeaponFactory
    : ScriptableObject
{
    [SerializeField] protected Single m_reloadTime;
    [SerializeField] protected Int32 m_maxBullet;
    [SerializeField] protected Single m_shootDelay;
    [SerializeField] protected Single m_reloadAnimationTime;
    [SerializeField] protected Int32 m_damage;
    [SerializeField] protected Int32 m_shootCost;
    [SerializeField] protected GameObject m_weaponPrefab;

    public WeaponData CreateWeaponData()
    {
        return new WeaponData(
            m_reloadTime,
            m_maxBullet,
            m_shootDelay,
            m_reloadAnimationTime / m_reloadTime,
            m_shootCost,
            m_weaponPrefab
            );
    }
    public abstract IWeaponStrategy CreateWeaponStrategy();
}