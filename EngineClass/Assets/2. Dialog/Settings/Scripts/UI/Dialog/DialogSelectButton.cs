using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogSelectButton : MonoBehaviour
{
    [SerializeField] private Button m_button;
    [SerializeField] private Text m_text;

    public void Bind(String pText,Action pOnClick)
    {
        m_text.text = pText;
        m_button.onClick.AddListener(new UnityAction(pOnClick));
    }
}