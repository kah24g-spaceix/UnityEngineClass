using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System;
public class ButtonDecorator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Single m_scaleFactor = 1.08f;
    [SerializeField] private Single m_scaleTime = 0.15f;
    [SerializeField] private Ease m_scaleEase = Ease.InCirc;

    private Vector3 m_initialScale;
    private void Awake()
    {
        m_initialScale = transform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(m_initialScale * m_scaleFactor, m_scaleTime).SetEase(m_scaleEase);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(m_initialScale, m_scaleTime).SetEase(m_scaleEase);
    }
    public void OnDisable()
    {
        transform.DOKill();
        transform.localScale = m_initialScale;
    }
}
