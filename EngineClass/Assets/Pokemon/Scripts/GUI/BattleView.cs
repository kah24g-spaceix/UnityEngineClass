using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;


public class BattleView : MonoBehaviour, IBattleView
{
    [Header("Text UI")]
    [SerializeField] private Text m_text;

    [Header("Select UI")]
    [SerializeField] private GameObject m_selectUI;
    [SerializeField] private TextButton m_fightButton;
    [SerializeField] private TextButton m_changeButton;

    [Header("Monster Select UI")]
    [SerializeField] private GameObject m_monsterSelectUI;
    [SerializeField] private MonsterDisplayButton[] m_monsterButtons;
    [SerializeField] private TextButton m_monsterSelectExitButton;

    [Header("Skill Select UI")]
    [SerializeField] private GameObject m_skillSelectUI;
    [SerializeField] private TextButton[] m_skillSelectButtons;

    [Header("Monster State UI")]
    [SerializeField] private MonsterStateView m_playerMonsterView;
    [SerializeField] private MonsterStateView m_opponentMonsterView;

    [Header("Monster Image")]
    [SerializeField] private Image m_playerMonsterImage;
    [SerializeField] private Image m_opponentMonsterImage;


    private BattlePresenter m_presenter;

    private void Awake()
    {
        m_presenter = GetComponent<BattlePresenter>();
        m_presenter.Bind(this);
    }

    private void Start()
    {
        m_fightButton.Button
            .onClick.AddListener(m_presenter.OnFightButton);
        m_changeButton.Button
            .onClick.AddListener(m_presenter.OnChangeButton);

        for (Int32 i = 0; i < m_skillSelectButtons.Length; i++)
        {
            Int32 index = i;
            m_skillSelectButtons[i].Button
                .onClick.AddListener(
                () => m_presenter.OnPlayerSkillButton(index)
            );
        }

        for (Int32 i = 0; i < m_monsterButtons.Length; i++)
        {
            Int32 index = i;
            m_monsterButtons[i].Button.
                onClick.AddListener(
                () => m_presenter.OnChangeSelectButton(index)
            );
        }

        HideMonsterSelect();
        HideSelectMenu();
        HideMonsterSkill();
    }

    public void ShowMonster(MonsterDisplayModel pModel, bool pIsPlayer)
    {
        UpdateMonster(pModel, pIsPlayer);
        Image targetImage = pIsPlayer ?
            m_playerMonsterImage : m_opponentMonsterImage;
        targetImage.color = Color.white;
        targetImage.sprite = pIsPlayer ?
            pModel.BackSprite : pModel.FrontSprite;
        targetImage.transform.localScale = Vector3.zero;
        targetImage.transform.DOKill();
        targetImage.transform.DOScale(1, 0.1f).SetEase(Ease.OutBack);
    }
    public void UpdateMonster(MonsterDisplayModel pModel, bool pIsPlayer)
    {
        if (pIsPlayer)
            m_playerMonsterView.Bind(pModel);
        else
            m_opponentMonsterView.Bind(pModel);
    }
    public void HideMonster(bool pIsplayer)
    {
        if (pIsplayer)
        {
            m_playerMonsterImage.transform.DOKill();
            m_playerMonsterImage.transform.DOScale(0, 0.1f).SetEase(Ease.InBack);
        }
        else
        {
            m_opponentMonsterImage.transform.DOKill();
            m_opponentMonsterImage.transform.DOScale(0, 0.1f).SetEase(Ease.InBack);
        }
    }

    public void ShowMonsterSelect(MonsterDisplayModel[] pModel, bool pIsClosable)
    {
        m_monsterSelectUI.gameObject.SetActive(true);
        m_monsterSelectExitButton.gameObject
            .SetActive(pIsClosable);

        for (Int32 i = 0; i < m_monsterButtons.Length; i++)
        {
            if (i < pModel.Length)
            {
                m_monsterButtons[i].gameObject.SetActive(true);
                m_monsterButtons[i].Bind(pModel[i]);
            }
            else
            {
                m_monsterButtons[i].gameObject.SetActive(false);
            }
        }
    }
    public void HideMonsterSelect()
    {
        m_monsterSelectUI.gameObject.SetActive(false);
    }

    public void ShowMonsterSkill(string[] pSkillNames)
    {
        m_skillSelectUI.gameObject.SetActive(true);
        for (Int32 i = 0; i < pSkillNames.Length; i++)
        {
            m_skillSelectButtons[i].Text.text = pSkillNames[i];
        }
    }
    public void HideMonsterSkill()
    {
        m_skillSelectUI.gameObject.SetActive(false);
    }

    public void ShowSelectMenu()
    {
        Vector3 postion = m_selectUI.transform.position;
        m_selectUI.transform.position = postion + new Vector3(1000, 0, 0);
        m_selectUI.transform.DOKill();
        m_selectUI.transform.DOMove(postion, 0.1f);
        m_selectUI.gameObject.SetActive(true);
    }
    public void HideSelectMenu()
    {
        m_selectUI.gameObject.SetActive(false);
        //CanvasGroup << 화면에서 안보이고 안눌리게
        //근데 GameObject는 켜져있게

        //GameObject가 꺼져있을때 
    }

    public void ShowText(string pText)
    {
        m_text.text = "";
        m_text.DOKill();
        m_text.DOText(pText, 0.1f).SetEase(Ease.Linear);
    }

    public void StartBattle()
    {

    }
}