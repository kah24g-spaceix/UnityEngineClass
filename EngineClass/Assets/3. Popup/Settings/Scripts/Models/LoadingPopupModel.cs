using System;
using System.Collections.Generic;


public class LoadingPopupModel : IPopupModel
{
    private static LoadingPopupModel m_instance = new LoadingPopupModel();
    public static LoadingPopupModel Instance => m_instance;

    public string Title => "안내";

    public string Content => "로딩중입니다...";

    public PopupMode PopupMode => PopupMode.NoButton;

    public void OnSelect(int pIndex)
    {
    }
}