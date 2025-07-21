using System;
using System.Collections.Generic;
using UnityEngine;


public class Entity : MonoBehaviour
{
    [SerializeField] private Int32 m_health;
    public Int32 Health => m_health;

    public Boolean IsAlive => m_health > 0;    

    public void AddHealth(Int32 pHealth)
    {
        m_health += pHealth;
    }

    public void Kill()
    {
        GameObject.Destroy(gameObject);
    }
}