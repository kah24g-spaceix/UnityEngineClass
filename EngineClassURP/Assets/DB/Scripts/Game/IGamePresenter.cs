using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGamePresenter
{
    void BindView(IGameView pView);
    void SelectCharacter(Int32 pIndex);
    void SelectItem(Int32 pIndex);

    void OnDoEnchant();

    void LoadGame();
    void SaveGame(); 
}
