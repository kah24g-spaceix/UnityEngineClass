using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogInjector : MonoBehaviour
{
    [SerializeField] private TextAsset m_textAsset;
    [SerializeField] private String m_parserName;
    private DialogController m_controller;

    private void Awake()
    {
        m_controller = GetComponent<DialogController>();
    }

    private void Start()
    {
        Type parserType = Type.GetType(m_parserName);       
        if(typeof(ITextParser<DialogData>).IsAssignableFrom(parserType))
        {
            var parser = (ITextParser<DialogData>)Activator.CreateInstance(parserType);
            BindParser(parser);
        }
        else if (typeof(ITextParser<SimpleDialogData>).IsAssignableFrom(parserType))
        {
            var parser = (ITextParser<SimpleDialogData>)Activator.CreateInstance(parserType);
            BindParser(parser);
        }
        else
        {
            Debug.LogError($"could not find parser : found [{parserType}] from [{m_parserName}], but not assignable");
        }
    }

    public void BindParser(ITextParser<DialogData> pDialogParser)
    {
        DialogData data = pDialogParser.Parse(m_textAsset.text);
        m_controller.ShowDialog(data);
    }

    public void BindParser(ITextParser<SimpleDialogData> pDialogParser)
    {
        SimpleDialogData data = pDialogParser.Parse(m_textAsset.text);
        m_controller.ShowDialog(data);
    }
}