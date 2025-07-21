using System;
using System.Collections.Generic;
using UnityEngine;

public struct ScrollElementViewModel
{
    public String Name { get; }
    public Sprite Icon { get; }

    public ScrollElementViewModel(String name, Sprite icon)
    {
        Name = name;
        Icon = icon;
    }
}

public struct GameUnitElementViewModel
{
    public String Name { get; }
    public Sprite Icon { get; }
    public String Info { get; }

    public GameUnitElementViewModel(String name, Sprite icon,String pInfo)
    {
        Name = name;
        Icon = icon;
        Info = pInfo;
    }
}
public interface IGameView
{
    void ShowGameItems(ScrollElementViewModel[] pModels);
    void ShowCharacters(ScrollElementViewModel[] pModels);

    void ShowTargetCharacter(GameUnitElementViewModel pCharacterViewModel);
    void HideTargetCharacter();
    void ShowTargetItem(GameUnitElementViewModel pItemViewModel);
    void HideTargetItem();

    void ShowEnchantButton();
    void HideEnchantButton();
}