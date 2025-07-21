using Steamworks;
using UnityEngine;

public class Ahivements : MonoBehaviour
{
    private int CoinCount = 0;
    private int MaxCoinCount = 10;

    public void Coin()
    {
        if (SteamManager.Initialized) // ���� ������ �ʱ�ȭ Ȯ��
        {
            // ���� �����Ȳ Ȯ��
            // Update Coin
            SteamUserStats.GetStat("Coin_Ahievement_01", out CoinCount);
            CoinCount++;
            SteamUserStats.SetStat("Coin_Ahievement_01", CoinCount);
            SteamUserStats.StoreStats();
            if (CoinCount >= MaxCoinCount)
            {
                // GetAchievement() �ż��带 ����Ͽ� ���� �Ϸ� (���� �Ϸ����� ���� ��� �Ϸ� ���¸� ������ �Ҵ�)
                Steamworks.SteamUserStats.GetAchievement("Coin_Ahievement_01", out bool achievementCompleted);

                if (!achievementCompleted)
                { 
                    // Steam ������ �����͸� ���������� �����Ϸ��� �� ������ StoreStats() �ż��带 ����� ���� ������ ����
                    SteamUserStats.SetAchievement("Coin_Ahievement_01");
                    SteamUserStats.StoreStats();
                }
            }
        }
    }
}
