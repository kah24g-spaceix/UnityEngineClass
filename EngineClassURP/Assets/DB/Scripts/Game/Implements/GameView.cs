using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour, IGameView
{
    [SerializeField] private GameUnitElement m_characterUnit;
    [SerializeField] private GameUnitElement m_itemUnit;
    [SerializeField] private Transform m_characterScrollContent;
    [SerializeField] private Transform m_itemScrollContent;
    [SerializeField] private Button m_enchantButton;

    [SerializeField] private Button m_saveButton;
    [SerializeField] private Button m_loadButton;

    [Header("Prefabs")]
    [SerializeField] private ScrollButtonElement m_buttonElementPrefab;

    private IGamePresenter m_presenter;

    private void Awake()
    {
        m_presenter = GetComponent<IGamePresenter>();
        m_presenter.BindView(this);

        m_enchantButton.onClick.AddListener(m_presenter.OnDoEnchant);
        m_saveButton.onClick.AddListener(m_presenter.SaveGame);
        m_loadButton.onClick.AddListener(m_presenter.LoadGame);

        HideEnchantButton();
        HideTargetCharacter();
        HideTargetItem();
    }

    public void ShowCharacters(ScrollElementViewModel[] pModels)
    {
        foreach (Transform item in m_characterScrollContent)
        {
            GameObject.Destroy(item.gameObject);
        }

        Int32 index = 0;
        foreach (var model in pModels)
        {
            Int32 i = index;
            ScrollButtonElement buttonElement = GameObject.Instantiate(m_buttonElementPrefab, m_characterScrollContent);
            buttonElement.Bind(model);
            buttonElement.OnClick += () => m_presenter.SelectCharacter(i);
            index++;
        }
    }

    public void ShowGameItems(ScrollElementViewModel[] pModels)
    {
        foreach (Transform item in m_itemScrollContent)
        {
            GameObject.Destroy(item.gameObject);
        }

        Int32 index = 0;
        foreach (var model in pModels)
        {
            Int32 i = index;
            ScrollButtonElement buttonElement = GameObject.Instantiate(m_buttonElementPrefab, m_itemScrollContent);
            buttonElement.Bind(model);
            buttonElement.OnClick += () => m_presenter.SelectItem(i);
            index++;
        }
    }

    public void ShowTargetCharacter(GameUnitElementViewModel pCharacterViewModel)
    {
        m_characterUnit.Bind(pCharacterViewModel);
        m_characterUnit.Show();
    }
    public void HideTargetCharacter()
    {
        m_characterUnit.Hide();
    }

    public void ShowTargetItem(GameUnitElementViewModel pItemViewModel)
    {
        m_itemUnit.Bind(pItemViewModel);
        m_itemUnit.Show();
    }
    public void HideTargetItem()
    {
        m_itemUnit.Hide();
    }

    public void ShowEnchantButton()
    {
        m_enchantButton.interactable = true;
    }

    public void HideEnchantButton()
    {
        m_enchantButton.interactable = false;
    }
}