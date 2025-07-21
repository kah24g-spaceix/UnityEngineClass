using System;
using UnityEngine;

public class WeaponData
{
    public Single ReloadTime { get; }
    public Int32 MaxBullet { get; }
    public Single ShootDelay { get; }
    public Single ReloadAnimationSpeed { get; }
    public Int32 ShootCost { get; }
    public GameObject WeaponPrefab { get; }
    public WeaponData(
        float reloadTime, 
        int maxBullet, 
        float shootDelay, 
        float reloadAnimationSpeed, 
        int shootCost,
        GameObject weaponPrefab
        )
    {
        ReloadTime = reloadTime;
        MaxBullet = maxBullet;
        ShootDelay = shootDelay;
        ReloadAnimationSpeed = reloadAnimationSpeed;
        ShootCost = shootCost;
        WeaponPrefab = weaponPrefab;
    }
}