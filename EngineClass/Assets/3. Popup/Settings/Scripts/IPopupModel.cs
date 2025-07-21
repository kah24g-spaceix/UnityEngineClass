using System;
using System.Collections.Generic;

public interface IPopupModel
{
    String Title { get; }
    String Content { get; }
    PopupMode PopupMode { get; }
    void OnSelect(Int32 pIndex);
}