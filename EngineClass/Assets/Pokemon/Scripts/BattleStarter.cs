using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleStarter : MonoBehaviour
{
    [SerializeField] private TextAsset m_monsterDataCSV;
    [SerializeField] private TextAsset m_monsterSkillDataCSV;
    private MonsterLoader m_monsterLoader;

    private void Start()
    {
        m_monsterLoader = new MonsterLoader();
        m_monsterLoader.Init(m_monsterDataCSV.text, m_monsterSkillDataCSV.text);

        MonsterState opponentMonsters = m_monsterLoader.CreateMonsterState("A");

        MonsterState[] playerMonsters = new MonsterState[]
        {
            m_monsterLoader.CreateMonsterState("B"),
            m_monsterLoader.CreateMonsterState("C"),
            m_monsterLoader.CreateMonsterState("D"),
            m_monsterLoader.CreateMonsterState("E"),
            m_monsterLoader.CreateMonsterState("F"),
        };

        GetComponent<BattlePresenter>().StartBattle(playerMonsters, opponentMonsters);
    }
}