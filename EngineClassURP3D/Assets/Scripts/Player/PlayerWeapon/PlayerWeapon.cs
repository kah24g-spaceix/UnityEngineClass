using System;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Transform m_weaponHolder;
    [SerializeField] private WeaponFactory m_defaultWeapon;

    private IWeaponStrategy m_weaponStrategy;
    private WeaponData m_weaponData;

    private Int32 m_currentBullet;

    private Boolean m_isWeaponInstalled;

    public Single ReloadTime => m_weaponData.ReloadTime;
    public Int32 MaxBullet => m_weaponData.MaxBullet;
    public Single ShootDelay => m_weaponData.ShootDelay;
    public Single ReloadAnimationSpeed 
        => m_weaponData.ReloadAnimationSpeed;

    public Int32 CurrentBullet => m_currentBullet;

    public void SetCurrentBullet(Int32 pBullet)
    {
        if (pBullet <0)
            m_currentBullet = 0;
        else
            m_currentBullet = pBullet;
    }
    public void ReloadBullet()
    {
        SetCurrentBullet(MaxBullet);
    }
    public void OnShoot()
    {
        m_weaponStrategy.OnShoot();
        SetCurrentBullet(m_currentBullet - m_weaponData.ShootCost);
    }

    public void InstallWeapon()
    {
        Debug.Log("Install");
        UnInstallWeapon();
        GameObject.Instantiate(m_weaponData.WeaponPrefab, 
            m_weaponHolder);
        m_isWeaponInstalled = true;
    }
    public void UnInstallWeapon() 
    {
        foreach (Transform child in m_weaponHolder)
        {
            GameObject.Destroy(child.gameObject);
        }
        m_isWeaponInstalled = false;
    }

    public void SetWeapon(
        IWeaponStrategy pStrategy,
        WeaponData pData)
    {
        m_weaponStrategy = pStrategy;
        m_weaponData = pData;
        m_currentBullet = 0;

        if (m_isWeaponInstalled)
            InstallWeapon();
    }

    private void Awake()
    {
        SetWeapon(
            m_defaultWeapon.CreateWeaponStrategy(),
            m_defaultWeapon.CreateWeaponData());
    }
}