using System;
using UnityEngine;

[CreateAssetMenu(
    fileName ="Rifle",
    menuName = "Weapon/Rifle",
    order = Int32.MinValue)]
public class RifleWeaponFactory : WeaponFactory
{
    public override IWeaponStrategy CreateWeaponStrategy()
    {
        return new RifleWeaponStrategy(m_damage);
    }
}