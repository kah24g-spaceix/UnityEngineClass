using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public partial class DialogView
{
    [Header("Assign")]
    [SerializeField] private Text m_talkLineText;
    [SerializeField] private Text m_talkerText;
    [SerializeField] private Image m_talkerPortrait;

    [SerializeField] private CanvasGroup m_selectView;
    [SerializeField] private GameObject m_selectContent;
    [SerializeField] private Text m_selectText;
}