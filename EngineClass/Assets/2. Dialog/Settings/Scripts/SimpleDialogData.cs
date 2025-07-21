using System;
using System.Collections;
using System.Collections.Generic;


public struct SimpleDialogLine
{
    public String Talker { get; }
    public String Text { get; }
    public String Sprite { get; }

    public SimpleDialogLine(String pTalker,String pText,String pSprite)
    {
        Talker = pTalker;
        Text = pText;
        Sprite = pSprite;
    }
}

public class SimpleDialogData : IEnumerable<SimpleDialogLine>
{
    public SimpleDialogLine[] DialogLines { get; }

    public SimpleDialogData(SimpleDialogLine[] dialogLines)
    {
        DialogLines = dialogLines;
    }

    public IEnumerator<SimpleDialogLine> GetEnumerator()
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