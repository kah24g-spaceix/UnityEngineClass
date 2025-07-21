using System;
using System.Collections.Generic;

public class DoubleSelectPopupModel : IPopupModel
{
    private Action m_onConfirm;
    private Action m_onCancel;
    public string Title { get; private set; }
    public string Content { get; private set; }
    public PopupMode PopupMode => PopupMode.TwoButton;

    public DoubleSelectPopupModel(
        String pTitle,
        String pContent,
        Action pOnConfirm,
        Action pOnCancel
        )
    {
        Title = pTitle;
        Content = pContent;
        m_onConfirm = pOnConfirm;
        m_onCancel = pOnCancel;
    }

    public void OnSelect(int pIndex)
    {
        if (pIndex == 0)
            m_onConfirm.Invoke();
        else if (pIndex == 1)
            m_onCancel.Invoke();
        else
            throw new IndexOutOfRangeException($"DoubleSelectPopupModel has two options, but got [{pIndex}]");
    }
}