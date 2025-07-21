using System;
using System.Collections.Generic;

public interface IPopupPresenter
{
    void BindView(IPopupView pView);
    void OpenPopup(IPopupModel pModel);
    void ClosePopup();
    void OnButtonClick(Int32 pIndex);
}
