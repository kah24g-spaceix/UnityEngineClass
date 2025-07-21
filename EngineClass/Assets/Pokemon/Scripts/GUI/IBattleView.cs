using System;
using System.Collections.Generic;
using UnityEngine;


public struct MonsterDisplayModel
{
    public Sprite FrontSprite { get; }
    public Sprite BackSprite { get; }

    public String Name { get; }
    public Int32 Health { get; }
    public Int32 MaxHealth { get; }

    public MonsterDisplayModel(Sprite pFrontSprite,
        Sprite pBackSprite,String pName,Int32 pHealth,
        Int32 pMaxHealth)
    {
        FrontSprite = pFrontSprite;
        BackSprite = pBackSprite;
        Name = pName;
        Health = pHealth;
        MaxHealth = pMaxHealth;
    }
}


public interface IBattleView 
{
    void StartBattle();
    void ShowText(String pText);

    void ShowSelectMenu(); //전투 도망간다 교체 
    void HideSelectMenu(); //전투 도망간다 교체 가려달라

    //이 포켓몬 보여달라
    void ShowMonster(MonsterDisplayModel pModel,Boolean pIsPlayer);
    //이 포켓몬 업데이트해달라
    void UpdateMonster(MonsterDisplayModel pModel,Boolean pIsPlayer);
    //이 포켓몬 가려달라
    void HideMonster(Boolean pIsplayer);

    void ShowMonsterSelect(
        MonsterDisplayModel[] pModel,Boolean pIsClosable);
    //교체 UI 열어달라(내 포켓몬이 기절한 상태면 못닫게 하려고 Boolean)
    void HideMonsterSelect(); //교체 UI 가려달라

    void ShowMonsterSkill(String[] pSkillNames);
    void HideMonsterSkill();  
}
