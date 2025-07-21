using System;


public class MonsterData
{
    public String ID { get; }
    public String Name { get; }
    public Int32 Attack { get; }
    public Int32 Deffence { get; }
    public Int32 Speed { get; }
    public Int32 Health { get; }
    public MonsterType Type { get; }
    public String[] SkillID { get; }

    public MonsterData(String pID,String pName,
        Int32 pAttack,Int32 pDeffence,Int32 pSpeed,Int32 pHealth,MonsterType pType,
        String[] pSkillID)
    {
        ID = pID;
        Name = pName;
        Attack = pAttack;
        Deffence = pDeffence;
        Speed = pSpeed;
        Health = pHealth;
        Type = pType;
        SkillID = pSkillID;
    }
}