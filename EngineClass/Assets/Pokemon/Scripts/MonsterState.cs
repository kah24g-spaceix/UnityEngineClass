using System;
using UnityEngine;

public class MonsterState
{
    public MonsterData Data { get; }
    public MonsterSkillState[] SkillStates { get; }
    public Int32 Health { get; private set; }
    public Boolean IsAlive => Health > 0;

    public MonsterState(MonsterData pData,
        MonsterSkillState[] pSkillStates)
    {
        Data = pData;
        SkillStates = pSkillStates;
        Health = Data.Health;
    }

    public void TakeDamage(Int32 pDamage)
    {
        Health -= pDamage;
        if (Health <= 0)
            Health = 0;
    }
}