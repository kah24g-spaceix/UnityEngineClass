using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    public void StartFire()
    {
        StartCoroutine("Shoot");
    }
    public void StopFire()
    {
        StopCoroutine("Shoot");
    }
    private IEnumerator Shoot ()
    {
        while (true)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
