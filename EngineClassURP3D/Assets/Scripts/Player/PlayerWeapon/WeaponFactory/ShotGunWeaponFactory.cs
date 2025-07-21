using System;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Shotgun",
    menuName = "Weapon/Shotgun",
    order = Int32.MinValue)]
public class ShotGunWeaponFactory : WeaponFactory
{
    [SerializeField] private Single m_spread;
    [SerializeField] private Int32 m_bulletCount;
    public override IWeaponStrategy CreateWeaponStrategy()
    {
        return new ShotGunWeaponStrategy(m_damage, m_spread, m_bulletCount);
    }
}