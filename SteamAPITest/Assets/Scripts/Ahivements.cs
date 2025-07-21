using Steamworks;
using UnityEngine;

public class Ahivements : MonoBehaviour
{
    private int CoinCount = 0;
    private int MaxCoinCount = 10;

    public void Coin()
    {
        if (SteamManager.Initialized) // 스팀 관리자 초기화 확인
        {
            // 현재 진행상황 확인
            // Update Coin
            SteamUserStats.GetStat("Coin_Ahievement_01", out CoinCount);
            CoinCount++;
            SteamUserStats.SetStat("Coin_Ahievement_01", CoinCount);
            SteamUserStats.StoreStats();
            if (CoinCount >= MaxCoinCount)
            {
                // GetAchievement() 매서드를 사용하여 업적 완료 (만약 완료하지 않은 경우 완료 상태를 변수에 할당)
                Steamworks.SteamUserStats.GetAchievement("Coin_Ahievement_01", out bool achievementCompleted);

                if (!achievementCompleted)
                { 
                    // Steam 서버에 데이터를 영구적으로 저장하려고 할 때마다 StoreStats() 매서드를 사용해 서버 데이터 유지
                    SteamUserStats.SetAchievement("Coin_Ahievement_01");
                    SteamUserStats.StoreStats();
                }
            }
        }
    }
}
