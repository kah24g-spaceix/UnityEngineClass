using System;
using UnityEngine;


public class RifleWeaponStrategy : IWeaponStrategy
{
    private Int32 m_damage;

    public RifleWeaponStrategy(Int32 pDamage)
    {
        m_damage = pDamage;
    }
    public void OnShoot()
    {
        Vector3 direction =
            Quaternion.Euler(UnityEngine.Random.insideUnitSphere * 3) *
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