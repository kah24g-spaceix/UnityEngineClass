using System;
using UnityEngine;

public struct DialogModel
{
    public String Text { get; }
    public String Talker { get; }
    public Sprite Portrait { get; }
    public String[] Options { get; }
    public Action<Int32> OnSelectOption { get; }
    public Action OnNext { get; }

    public DialogModel(String pText, String pTalker, Sprite pPortrait,Action pOnNext)
    {
        Text = pText;
        Talker = pTalker;
        Portrait = pPortrait;
        Options = null;
        OnSelectOption = null;
        OnNext = pOnNext;
    }
    public DialogModel(String pText, String[] pOptions, Action<Int32> pOnSelectOptions)
    {
        Text = pText;
        Talker = null;
        Portrait = null;
        Options = pOptions;
        OnSelectOption = pOnSelectOptions;
        OnNext = null;
    }
}