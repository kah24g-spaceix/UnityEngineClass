using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogLine
{
    public String Talker { get; set; }
    public String Text { get; set; }
    public String Sprite { get; set; }
    public DialogLine[][] Options { get; set; }

    public DialogLine(String pTalker, String pText, String pSprite)
    {
        Talker = pTalker;
        Text = pText;
        Sprite = pSprite;
        Options = null;
    }

    public DialogLine(String pTalker, String pText, String pSprite, DialogLine[][] pOptions)
    {
        Talker = pTalker;
        Text = pText;
        Sprite = pSprite;
        Options = pOptions;
    }
}

public class DialogData : IEnumerable<DialogLine>
{
    public DialogLine[] DialogLines { get; }

    public DialogData(DialogLine[] dialogLines)
    {
        DialogLines = dialogLines;
    }

    public IEnumerator<DialogLine> GetEnumerator()
    {
        foreach (var dialogLine in DialogLines)
        {
            yield return dialogLine;
        }
        yield break;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
