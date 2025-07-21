using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTester : MonoBehaviour
{
    private IPopupPresenter m_presenter;

    private void Awake()
    {
        m_presenter = FindObjectOfType<PopupPresenter>();
    }

    [InspectorButton("�ɼ� �ΰ� �׽�Ʈ")]
    private void OnTwoOption()
    {
        m_presenter.OpenPopup(new DoubleSelectPopupModel(
            "���� ����",
            "������ �����Ͻðڽ��ϱ�?",
            () => Debug.Log("���� ����"),
            () => Debug.Log("���� ���� ���")
            ));
    }

    [InspectorButton("�ȳ� �׽�Ʈ")]
    private void OnSingleOption()
    {
        m_presenter.OpenPopup(new NotifyPopupModel("���� ������ ������ �� �����ϴ�."));
    }

    [InspectorButton("�ε��� �׽�Ʈ")]
    private void OnLoading()
    {
        IEnumerator LoadingRoutine()
        {
            m_presenter.OpenPopup(LoadingPopupModel.Instance);
            yield return new WaitForSeconds(1f);
            m_presenter.ClosePopup();
        }

        StartCoroutine(LoadingRoutine());
    }

    [InspectorButton("���� �˾� �ݱ�")]
    private void OnClose()
    {
        m_presenter.ClosePopup();
    }
}
