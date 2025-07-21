using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public struct CharacterState
{
    public Int32 ID { get; }
    public String Name { get; }
    public Int32 Attack { get; }
    public Int32 Health { get; }

    public CharacterState(Int32 pID,String pName,Int32 pAttack,Int32 pHealth)
    {
        ID = pID;
        Name = pName;
        Attack = pAttack;
        Health = pHealth;
    }
}

public struct ItemData
{
    public Int32 ID { get; }
    public String Name { get; }
    public Single Chance { get; }
    public Int32 Amount { get; }
    public Int32 Damage { get; }

    public ItemData(Int32 pID,String pName,Single pChance,Int32 pAmount,Int32 pDamage)
    {
        ID = pID;
        Name = pName;
        Chance = pChance;
        Amount = pAmount;
        Damage = pDamage;
    }
}

public interface IGameModel
{
    CharacterState[] GetCharacterStates();
    ItemData[] GetItemDatas();
    Boolean DoEnchant(Int32 pCharacterID, Int32 pItemID);

    Boolean LoadGame();
    void SaveGame();
}