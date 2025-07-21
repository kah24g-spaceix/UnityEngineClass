using System;
using UnityEngine;


public class ShotGunWeaponStrategy : IWeaponStrategy
{
    private Int32 m_damage;
    private Single m_spread;
    private Int32 m_bulletCount;

    public ShotGunWeaponStrategy(Int32 pDamage, Single spread, Int32 bulletCount)
    {
        m_damage = pDamage;
        m_spread = spread;
        m_bulletCount = bulletCount;
    }
    public void OnShoot()
    {
        for (int i = 0; i < m_bulletCount; i++)
        {
            Vector3 direction =
                Quaternion.Euler(UnityEngine.Random.insideUnitSphere * m_spread) *
                Camera.main.transform.forward;
            Ray ray = new Ray(
                Camera.main.transform.position,
                direction);
            Debug.DrawRay(ray.origin, ray.direction * 30);
            if (Physics.Raycast(ray, out var hit, 30))
            {
                //IHittable?
                if (hit.collider.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.TakeDamage(m_damage);
                }

                GameObject hitObject
                    = Resources.Load<GameObject>("Hit");

                GameObject.Instantiate(hitObject, hit.point, Quaternion.identity);
            }
        }
    }
}