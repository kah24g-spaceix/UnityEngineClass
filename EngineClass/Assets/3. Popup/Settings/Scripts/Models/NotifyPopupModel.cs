using System;

public class NotifyPopupModel : IPopupModel
{
    public NotifyPopupModel(String pContent)
    {
        Content = pContent;
    }

    public string Title => "안내";

    public string Content { get; private set; }

    public PopupMode PopupMode => PopupMode.OneButton;

    public void OnSelect(int pIndex) { }
}