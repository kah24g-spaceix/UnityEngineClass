using System;


public class MonsterSkillData
{
    public String ID { get; }
    public String Name { get; }
    public Int32 Attack { get; }
    public Int32 HitRate { get; } //0~100 %
    public MonsterType Type { get; }

    public MonsterSkillData(String pID,String pName,
        Int32 pAttack,Int32 pHitRate,MonsterType pType)
    {
        ID = pID;
        Name = pName;
        Attack = pAttack;
        HitRate = pHitRate;
        Type = pType;
    }
}