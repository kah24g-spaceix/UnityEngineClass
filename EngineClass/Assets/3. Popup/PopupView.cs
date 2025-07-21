using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopupView : MonoBehaviour, IPopupView
{
    [SerializeField] private GameObject m_popupHolder;
    [SerializeField] private Image m_panel;    
    [SerializeField] private Image m_popup;
    [SerializeField] private Text m_titleText;
    [SerializeField] private Text m_contentText;
    [SerializeField] private Button[] m_buttons;

    private IPopupPresenter m_presenter;

    private void Awake()
    {
        m_presenter = GetComponent<IPopupPresenter>();
        m_presenter.BindView(this);

        for(Int32 i=0;i<m_buttons.Length;i++)
        {
            Int32 index = i;
            m_buttons[i].onClick.AddListener(()=>m_presenter.OnButtonClick(index));
        }
    }

    public void OpenPopup(string pTitle, string pContent, PopupMode pMode)
    {
        m_popupHolder.gameObject.SetActive(true);

        m_popup.transform.localScale = Vector3.zero;
        
        m_popup.transform.DOKill();
        m_popup.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);

        m_titleText.text = pTitle;
        m_contentText.text = pContent;

        Int32 buttonCount = (Int32)pMode;
        for(Int32 i=0;i<m_buttons.Length;i++)
        {
            if (i >= buttonCount)
            {
                m_buttons[i].gameObject.SetActive(false);
                continue;
            }

            m_buttons[i].gameObject.SetActive(true);
        }
    }


    public void ClosePopup()
    {
        m_popup.transform.localScale = Vector3.one;
        m_popup.transform.DOKill();
        m_popup.transform
            .DOScale(0, 0.5f)
            .SetEase(Ease.InBack)
            .OnComplete(() => 
            {
                m_popupHolder.gameObject.SetActive(false);
            });
    }
}
