using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameSaver
{
    void Save(List<CharacterSaveData> pSaveData);
    List<CharacterSaveData> Load();
}
