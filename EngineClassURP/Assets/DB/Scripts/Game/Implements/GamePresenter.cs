using System;
using System.Collections.Generic;
using UnityEngine;

public class GamePresenter : MonoBehaviour, IGamePresenter
{
    private IGameModel m_model;
    private IGameView m_gameView;

    private CharacterState[] m_states;
    private ItemData[] m_datas;

    private Int32 m_selectedCharaterID = -1;
    private Int32 m_selectedItemID = -1;

    private void Awake()
    {
        m_model = GetComponent<IGameModel>();
    }

    public void BindView(IGameView pView)
    {
        m_gameView = pView;
    }

    private void Start()
    {
        ReloadData();
    }

    private Sprite LoadItemSprite(Int32 pID)
    {
        return Resources.Load<Sprite>($"Sprites/Items/{pID}");
    }
    private Sprite LoadCharacterSprite(Int32 pID)
    {
        return Resources.Load<Sprite>($"Sprites/Characters/{pID}");
    }

    private void ReloadData()
    {
        m_states = m_model.GetCharacterStates();
        List<ScrollElementViewModel> characterList = new List<ScrollElementViewModel>();
        foreach (var state in m_states)
        {
            ScrollElementViewModel viewModel = new ScrollElementViewModel(state.Name, LoadCharacterSprite(state.ID));
            characterList.Add(viewModel);
        }
        m_gameView.ShowCharacters(characterList.ToArray());

        m_datas = m_model.GetItemDatas();
        List<ScrollElementViewModel> itemList = new List<ScrollElementViewModel>();
        foreach (var data in m_datas)
        {
            ScrollElementViewModel viewModel = new ScrollElementViewModel(data.Name, LoadItemSprite(data.ID));
            itemList.Add(viewModel);
        }
        m_gameView.ShowGameItems(itemList.ToArray());
    }

    public void OnDoEnchant()
    {
        m_model.DoEnchant(m_selectedCharaterID, m_selectedItemID);
        ReloadData();
        ShowCharacter(m_selectedCharaterID);
    }

    public void SelectCharacter(int pIndex)
    {
        m_selectedCharaterID = m_states[pIndex].ID;
        if (m_selectedItemID != -1 && m_selectedCharaterID != -1)
            m_gameView.ShowEnchantButton();
        ShowCharacter(m_selectedCharaterID);
    }

    private void ShowCharacter(Int32 pID)
    {
        foreach (var state in m_states)
        {
            if (state.ID == pID)
            {
                String name = state.Name;
                Sprite icon = LoadCharacterSprite(state.ID);
                String info = $"체력 : {state.Health}\n공격력 : {state.Attack}";

                m_gameView.ShowTargetCharacter(new GameUnitElementViewModel(name, icon, info));
                return;
            }
        }

        m_gameView.HideTargetCharacter();
        m_gameView.HideEnchantButton();
    }

    public void SelectItem(int pIndex)
    {
        var data = m_datas[pIndex];

        m_selectedItemID = data.ID;

        if (m_selectedItemID != -1 && m_selectedCharaterID != -1)
            m_gameView.ShowEnchantButton();

        String name = data.Name;
        Sprite icon = LoadItemSprite(data.ID);
        String info = $"성공 확률 : {(Int32)(data.Chance * 100)}%\n성공 시 : 공격력 {data.Amount} 증가\n실패 시 : {data.Damage} 피해";

        m_gameView.ShowTargetItem(new GameUnitElementViewModel(name, icon, info));

    }

    public void LoadGame()
    {
        m_model.LoadGame();

        ReloadData();
        m_gameView.HideTargetCharacter();
        m_gameView.HideTargetItem();
        m_selectedCharaterID = -1;
        m_selectedItemID = -1;
    }

    public void SaveGame()
    {
        m_model.SaveGame();
    }
}