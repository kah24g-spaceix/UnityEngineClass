using System;


public enum PopupMode
{
    NoButton = 0,
    OneButton = 1,
    TwoButton = 2
}

public interface IPopupView
{
    void OpenPopup(String pTitle,String pContent, PopupMode pMode);
    void ClosePopup();
}