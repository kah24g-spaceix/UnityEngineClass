using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
public class SteamScript : MonoBehaviour
{
    // CallBack ��� ���� ����
    protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;

    // CallResult �ɹ� ���� ����
    private CallResult<NumberOfCurrentPlayers_t> m_NumberOfCurrentPlayers;

    // Start is called before the first frame update
    void Start()
    {
        if (SteamManager.Initialized) // �׻� Steam�� �ʱ�ȭ �Ǿ����� Ȯ��
        {
            // Steam ������� ǥ�� �̸�
            string name = SteamFriends.GetPersonaName();
            Debug.Log(name);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SteamAPICall_t handle = SteamUserStats.GetNumberOfCurrentPlayers();
            m_NumberOfCurrentPlayers.Set(handle);
            Debug.Log("Called GetNumberOfCurrentPlayers()");
        }
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        if (SteamManager.Initialized)
        {
            // Steam CallBacks
            m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
            
            // Steam CallResults
            m_NumberOfCurrentPlayers = CallResult<NumberOfCurrentPlayers_t>.Create(OnNumberOfCurrentPlayers);
        }
    }
    private void OnDisable()
    {
        // CallBack ����� ����
        if (m_GameOverlayActivated != null)
        {
            m_GameOverlayActivated.Dispose();
            m_GameOverlayActivated = null;
        }
    }

    // Steam CallBacks
    private void OnGameOverlayActivated(GameOverlayActivated_t pCallback)
    {
        // Steam �������̰� ���� �� ������ �Ͻ����� �ϴ� �ڵ�
        if (pCallback.m_bActive != 0)
        {
            Debug.Log("Steam Overlay has been activated");
        }
        else
        {
            Debug.Log("Steam Overlay has been closed");
        }
    }

    // Steam CallResults
    private void OnNumberOfCurrentPlayers(NumberOfCurrentPlayers_t pCallback, bool bIOFailure)
    {
        
        if (pCallback.m_bSuccess != 1 || bIOFailure)
        {
            Debug.Log("There was an error retrieving the NumberOfCurrentPlayers.");
        }
        else 
        {
            Debug.Log("The number of players playing your game: " + pCallback.m_cPlayers);
        }
    }
}
