using System;
using System.Collections.Generic;
using UnityEngine;

internal class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] m_spawnGameObjects;
    [SerializeField] private Single m_spawnMinDelay;
    [SerializeField] private Single m_spawnMaxDelay;
    [SerializeField] private Single m_spawnRange;

    private Single m_spawnLeft;

    private void Update()
    {
        if (m_spawnLeft > 0)
            return;

        m_spawnLeft = UnityEngine.Random.Range(m_spawnMinDelay, m_spawnMaxDelay);
        Vector2 spawnPoint = transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * m_spawnRange;
        GameObject.Instantiate(m_spawnGameObjects[UnityEngine.Random.Range(0, m_spawnGameObjects.Length)], spawnPoint,Quaternion.identity);
    }
}