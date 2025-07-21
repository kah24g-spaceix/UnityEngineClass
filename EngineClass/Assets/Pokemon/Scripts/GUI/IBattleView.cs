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

    void ShowSelectMenu(); //���� �������� ��ü 
    void HideSelectMenu(); //���� �������� ��ü �����޶�

    //�� ���ϸ� �����޶�
    void ShowMonster(MonsterDisplayModel pModel,Boolean pIsPlayer);
    //�� ���ϸ� ������Ʈ�ش޶�
    void UpdateMonster(MonsterDisplayModel pModel,Boolean pIsPlayer);
    //�� ���ϸ� �����޶�
    void HideMonster(Boolean pIsplayer);

    void ShowMonsterSelect(
        MonsterDisplayModel[] pModel,Boolean pIsClosable);
    //��ü UI ����޶�(�� ���ϸ��� ������ ���¸� ���ݰ� �Ϸ��� Boolean)
    void HideMonsterSelect(); //��ü UI �����޶�

    void ShowMonsterSkill(String[] pSkillNames);
    void HideMonsterSkill();  
}
