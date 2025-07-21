using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[RequireComponent(typeof(IView<DialogModel>))]
public class DialogController : MonoBehaviour
{
    private IView<DialogModel> m_view;
    private Coroutine m_routine;
    private Boolean m_doSkip;
    private Int32 m_select;

    private void Awake()
    {
        m_view = GetComponent<IView<DialogModel>>();
    }

    public void ShowDialog(DialogData pData)
    {
        m_routine = StartCoroutine(DialogRoutine(pData.DialogLines));
    }

    public void ShowDialog(SimpleDialogData pData)
    {
        m_routine = StartCoroutine(SimpleDialogRoutine(pData));
    }

    private Sprite LoadSprite(String pPath)
    {
        String path = Regex.Replace(pPath, @"\s+", "");
        return Resources.Load<Sprite>(path);
    }


    private IEnumerator DialogRoutine(DialogLine[] pData,Int32 pStartIndex = 0)
    {
        m_view.Show();

        for(Int32 i=pStartIndex; i<pData.Length;i++)
        {
            DialogLine line = pData[i];
            Sprite sprite = LoadSprite($"Sprites/{line.Sprite}");


            if (line.Options != null)
            {
                List<String> selects = new List<string>();
                foreach (var item in line.Options)
                {
                    selects.Add(item[0].Text);
                }
                m_view.Bind(new DialogModel(line.Text, selects.ToArray(), OnSelect));

                yield return new WaitUntil(() => m_doSkip);

                yield return StartCoroutine(DialogRoutine(line.Options[m_select],1));

                yield break;
            }

            m_view.Bind(new DialogModel(line.Text, line.Talker, sprite, OnNext));
            yield return new WaitUntil(() => m_doSkip);
            m_doSkip = false;
        }

        m_view.Hide();
    }


    private IEnumerator SimpleDialogRoutine(SimpleDialogData pData)
    {
        m_view.Show();

        foreach (var line in pData)
        {
            Sprite sprite = LoadSprite($"Sprites/{line.Sprite}");
            m_view.Bind(new DialogModel(line.Text, line.Talker, sprite, OnNext));
            yield return new WaitUntil(() => m_doSkip);
            m_doSkip = false;
        }

        m_view.Hide();
    }

    private void OnNext()
    {
        m_doSkip = true;
    }

    private void OnSelect(Int32 pIndex)
    {
        m_select = pIndex;
        OnNext();
    }
}