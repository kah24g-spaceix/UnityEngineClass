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

    [InspectorButton("옵션 두개 테스트")]
    private void OnTwoOption()
    {
        m_presenter.OpenPopup(new DoubleSelectPopupModel(
            "게임 종료",
            "게임을 종료하시겠습니까?",
            () => Debug.Log("게임 종료"),
            () => Debug.Log("게임 종료 취소")
            ));
    }

    [InspectorButton("안내 테스트")]
    private void OnSingleOption()
    {
        m_presenter.OpenPopup(new NotifyPopupModel("현재 게임을 종료할 수 없습니다."));
    }

    [InspectorButton("로딩중 테스트")]
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

    [InspectorButton("강제 팝업 닫기")]
    private void OnClose()
    {
        m_presenter.ClosePopup();
    }
}
