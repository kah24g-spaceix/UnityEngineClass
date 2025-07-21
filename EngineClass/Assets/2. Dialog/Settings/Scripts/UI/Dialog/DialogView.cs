using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



[RequireComponent(typeof(CanvasGroup))]
public partial class DialogView : MonoBehaviour, IView<DialogModel>
{
    [Header("Setting")]
    [SerializeField] private Single m_printDelay = 0.08f;
    [SerializeField] private KeyCode m_nextKey = KeyCode.Space;
    [SerializeField] private DialogSelectButton m_buttonPrefab;

    private CanvasGroup m_canvasGroup;
    private Coroutine m_printRoutine;

    private Boolean m_doSkip;
    private Boolean m_isPrinting;

    private void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Bind(DialogModel pModel)
    {
        m_doSkip = false;
        m_selectView.Hide();
        if (pModel.Portrait == null)
            m_talkerPortrait.color = Color.clear;
        else
            m_talkerPortrait.color = Color.white;

        m_talkerText.text = pModel.Talker;
        m_talkerPortrait.sprite = pModel.Portrait;
        m_talkLineText.text = "";
        
        if (m_printRoutine != null)
            StopCoroutine(m_printRoutine);

        if (pModel.Options != null)
            m_printRoutine = StartCoroutine(SelectRoutine(pModel));
        else
            m_printRoutine = StartCoroutine(PrintRoutine(pModel));
    }

    private IEnumerator SelectRoutine(DialogModel pModel)
    {
        m_isPrinting = false;
        m_selectView.Show();
        for (Int32 i = 0; i < m_selectContent.transform.childCount; i++)
        {
            if (i == 0)
                continue;

            GameObject.Destroy(m_selectContent.transform.GetChild(i).gameObject);
        }

        for (Int32 i = 0; i < pModel.Options.Length; i++)
        {
            Int32 index = i;
            DialogSelectButton button = GameObject.Instantiate(m_buttonPrefab, m_selectContent.transform);
            button.Bind(pModel.Options[i], () => pModel.OnSelectOption(index));
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(m_selectContent.transform as RectTransform);

        m_selectText.text = pModel.Text;

        yield break;
    }

    private IEnumerator PrintRoutine(DialogModel pModel)
    {
        m_isPrinting = true;
        foreach (var ch in pModel.Text)
        {
            m_talkLineText.text += ch;
            yield return new WaitForSeconds(m_printDelay);
            if(m_doSkip)
            {
                m_talkLineText.text = pModel.Text;
                m_doSkip = false;
                break;
            }
        }

        yield return new WaitUntil(()=>m_doSkip);
        m_isPrinting = false;
        m_doSkip = false;
        pModel.OnNext?.Invoke();
        yield break;
    }

    private void Update()
    {
        if (!m_isPrinting)
            return;

        if(Input.GetKeyDown(m_nextKey))
            m_doSkip = true;
    }

    public void Show()
    {
        m_canvasGroup.Show();

        m_talkerPortrait.color = Color.clear;
        m_talkerText.text = "";
        m_talkLineText.text = "";
    }
    public void Hide()
    {
        m_canvasGroup.Hide();
    }
}
