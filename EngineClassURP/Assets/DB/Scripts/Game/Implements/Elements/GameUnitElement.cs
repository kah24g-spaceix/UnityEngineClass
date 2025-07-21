using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUnitElement : MonoBehaviour
{
    [SerializeField] private Image m_icon;
    [SerializeField] private Text m_name;
    [SerializeField] private Text m_panelname;
    [SerializeField] private Text m_panelInfo;

    private CanvasGroup m_canvasGroup;

    private void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Bind(GameUnitElementViewModel pModel)
    {
        m_icon.sprite = pModel.Icon;
        m_name.text = pModel.Name;
        m_panelname.text = pModel.Name;
        m_panelInfo.text = pModel.Info;
    }

    public void Show()
    {
        m_canvasGroup.alpha = 1;
        m_canvasGroup.interactable = true;
        m_canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        m_canvasGroup.alpha = 0;
        m_canvasGroup.interactable = false;
        m_canvasGroup.blocksRaycasts = false;
    }
}