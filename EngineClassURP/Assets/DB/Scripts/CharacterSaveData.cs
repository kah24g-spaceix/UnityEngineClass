using System;
using System.Collections.Generic;

public struct CharacterSaveData
{
    public Int32 ID { get; set; }
    public Int32 Health { get; set; }
    public Int32 Attack { get; set; }

    public CharacterSaveData(CharacterState pState)
    {
        ID = pState.ID;
        Health = pState.Health;
        Attack = pState.Attack;
    }
}