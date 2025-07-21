using System;
using UnityEngine;

public class PopupPresenter : MonoBehaviour, IPopupPresenter
{
    private IPopupView m_view;
    private IPopupModel m_model;
    public void BindView(IPopupView pView)
    {
        m_view = pView;
        m_view.ClosePopup();
    }

    public void OpenPopup(IPopupModel pModel)
    {
        m_model = pModel;
        m_view.OpenPopup(m_model.Title, m_model.Content, m_model.PopupMode);
    }

    public void OnButtonClick(int pIndex)
    {
        if (m_model == null)
            return;

        m_model.OnSelect(pIndex);
        m_view.ClosePopup();
        m_model = null;
    }

    public void ClosePopup()
    {
        m_view.ClosePopup();
        m_model = null;
    }
}