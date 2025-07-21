using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollButtonElement : MonoBehaviour
{
    [SerializeField] private Image m_icon;
    [SerializeField] private Text m_name;

    private Button m_button;

    public event Action OnClick;

    private void Awake()
    {
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(OnClickButton);
    }

    public void Bind(ScrollElementViewModel pModel)
    {
        m_icon.sprite = pModel.Icon;
        m_name.text = pModel.Name;
    }

    private void OnClickButton()
    {
        OnClick?.Invoke();
    }
}