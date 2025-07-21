  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    private void Awake()
    {
        StartCoroutine("SpawnEnemy");
    }
    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(enemyPrefab, new Vector3(Random.Range(-2.6f, 2.6f),5,0),Quaternion.identity);
            yield return new WaitForSeconds( ( Random.Range(0.0f, 1.0f) ) );
        }
    }
}
