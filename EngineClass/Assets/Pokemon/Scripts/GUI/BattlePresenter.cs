using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattlePresenter : MonoBehaviour
{
    [SerializeField] private TextAsset m_textLoaderXML;

    private TextLoader m_loader;
    private IBattleView m_battleView;
    private MonsterBattle m_battle;

    private void Awake()
    {
        m_battle = new MonsterBattle();
        m_loader = new TextLoader();
        m_loader.Load(m_textLoaderXML.text);
    }

    /*
    private void Start()
    {
        MonsterData monsterA = new MonsterData(
            "A",
            "노말테스트",
            99,
            10,
            20,
            10,
            MonsterType.Normal,
            new string[] { "10001", "10002", "10003", "10004" }
            );

        MonsterData monsterB = new MonsterData(
            "B",
            "페어리테슷",
            99,
            10,
            20,
            10,
            MonsterType.Fairy,
            new string[] { "10001", "10002", "10003", "10004" }
            );

        MonsterData monsterC = new MonsterData(
            "C",
            "얼음테스트",
            99,
            10,
            20,
            10,
            MonsterType.Ice,
            new string[] { "10001", "10002", "10003", "10004" }
            );

        MonsterSkillState[] skillStates = new MonsterSkillState[]
        {
            new MonsterSkillState(
                new MonsterSkillData("10001","얼음",100,90,MonsterType.Ice)
                ),

            new MonsterSkillState(
                new MonsterSkillData("10002","노말공격",100,90,MonsterType.Normal)
                ),

            new MonsterSkillState(
                new MonsterSkillData("10003","페어리공격",100,90,MonsterType.Fairy)
                ),

            new MonsterSkillState(
                new MonsterSkillData("10004","불공격",100,90,MonsterType.Fire)
                ),
        };

        StartBattle(new MonsterState[]
        {
            new MonsterState(monsterA,skillStates),
            new MonsterState(monsterB,skillStates),
            new MonsterState(monsterC,skillStates),
        }, new MonsterState(monsterA, skillStates));
    }
    */

    public void Bind(IBattleView pView)
    {
        m_battleView = pView;
    }

    public void StartBattle(
        MonsterState[] pPlayerMonster,
        MonsterState pOpponentMonster)
    {
        m_battle.StartBattle(pPlayerMonster, pOpponentMonster);
        m_battleView.StartBattle();
        StartCoroutine(StartBattleRoutine());
    }

    private MonsterDisplayModel CreateDisplayModel(
        MonsterState pState)
    {
        return new MonsterDisplayModel(
            Resources.Load<Sprite>(
                $"Sprites/Monster/{pState.Data.ID}_1"),
            Resources.Load<Sprite>(
                $"Sprites/Monster/{pState.Data.ID}_0"),
            pState.Data.Name,
            pState.Health,
            pState.Data.Health
            );
    }

    private void ShowText(TextID pID,params System.Object[] pObjects)
    {
        m_battleView.ShowText(m_loader.GetString(pID, pObjects));
    }

    private IEnumerator StartBattleRoutine()
    {
        MonsterState playerMonster = m_battle.CurrentPlayerMonster;
        MonsterState opponentMonster = m_battle.OpponentMonster;

        ShowText(TextID.POKEMON_ENCOUNTER, opponentMonster.Data.Name);
        m_battleView.ShowMonster(
            CreateDisplayModel(opponentMonster),false);

        yield return new WaitForSeconds(1);

        ShowText(TextID.POKEMON_GO,playerMonster.Data.Name);
        m_battleView.ShowMonster(
            CreateDisplayModel(playerMonster), true);

        yield return new WaitForSeconds(1);

        ShowText(TextID.MESSAGE_WHAT_TO_DO);
        m_battleView.ShowSelectMenu();
    }

    public void OnFightButton()
    {
        m_battleView.HideSelectMenu();
        m_battleView.ShowMonsterSkill(
            m_battle.CurrentPlayerMonster.SkillStates
            .Select(state => state.Data.Name).ToArray());
    }

    public void OnPlayerSkillButton(Int32 pIndex)
    {
        IEnumerator SkillFightRoutine(Int32 pPlayerSkillIndex)
        {
            Int32 opponentSkillIndex 
                = UnityEngine.Random.Range(0, 4);

            MonsterState firstAttack = m_battle.GetFirstAttack();
            MonsterState secondAttack = m_battle.GetSecondAttack();

            Boolean isPlayerAttackFirst = m_battle.IsPlayerAttackFirst();
            Int32 firstSkillIndex = 
                isPlayerAttackFirst ? pPlayerSkillIndex : opponentSkillIndex;
            Int32 secondSkillIndex =
                isPlayerAttackFirst ? opponentSkillIndex : pPlayerSkillIndex;

            yield return SkillUseRoutine(
                firstAttack, secondAttack, firstSkillIndex, isPlayerAttackFirst);
            if (!secondAttack.IsAlive)
                yield break;
            yield return SkillUseRoutine(
                secondAttack, firstAttack, secondSkillIndex, !isPlayerAttackFirst);
            if (!firstAttack.IsAlive)
                yield break;

            //여기까지 오면 둘 다 생존 
            ShowText(TextID.MESSAGE_WHAT_TO_DO);
            m_battleView.ShowSelectMenu();
        }

        m_battleView.HideMonsterSkill();
        StartCoroutine(SkillFightRoutine(pIndex));
    }

    private IEnumerator SkillUseRoutine(
        MonsterState pUser,MonsterState pTarget,Int32 pSkillIndex,
        Boolean pIsPlayer)
    {
        if (pSkillIndex == -1 && pIsPlayer)
            yield break;
        //index -1 : 플레이어가 스킬 안씀
        //교체 한 뒤 
        //내 포켓몬을 내보냄 -> 상대만 스킬을 씀 
        SkillResult result = m_battle
            .UseSkill(pUser, pTarget, pSkillIndex);

        ShowText(TextID.SKILL_USE, pUser.Data.Name, pUser.SkillStates[pSkillIndex].Data.Name);
        yield return new WaitForSeconds(1);

        if(!result.IsSuccess)
        {
            ShowText(TextID.SKILL_FAIL);
            yield return new WaitForSeconds(1);
            yield break;
        }

        m_battleView.UpdateMonster(CreateDisplayModel(pTarget), !pIsPlayer);
        yield return new WaitForSeconds(1);

        if(result.HitPower == SkillHitPower.NoEffect)
            ShowText(TextID.SKILL_HIT_NOEFFECT);
        else if(result.HitPower == SkillHitPower.Normal)
            ShowText(TextID.SKILL_HIT_NORMAL);
        else if (result.HitPower == SkillHitPower.Great)
            ShowText(TextID.SKILL_HIT_GREAT);

        yield return new WaitForSeconds(1);

        if(result.IsCritical)
        {
            ShowText(TextID.SKILL_HIT_CRITICAL);
            yield return new WaitForSeconds(1);
        }

        if(!pTarget.IsAlive)
        {
            ShowText(TextID.POKEMON_FAINT,pTarget.Data.Name);
            m_battleView.HideMonster(!pIsPlayer);

            yield return new WaitForSeconds(1);

            if(!pIsPlayer) //죽은게 플레이어
            {
                if(m_battle.HasChangableMonster())
                {
                    //교체 열어줌
                    OpenMonsterChange(false);
                }
                else
                {
                    ShowText(TextID.MESSAGE_GAME_LOSE);
                    yield return new WaitForSeconds(1f);
                    ShowText(TextID.MESSAGE_GAME_LOSE_2);
                    yield break;
                }
            }
            else
            {
                ShowText(TextID.MESSAGE_GAME_WIN);
                yield break;
            }
        }
    }

    public void OnChangeButton() //교체 버튼을 눌렀을때
    {
        OpenMonsterChange(true);
    }

    private void OpenMonsterChange(Boolean pIsClosable)
        //내 포켓몬 죽어서 교체해야하면 UI가 닫히면 안됨
    {
        /*
        List<MonsterDisplayModel> displayList = new List<MonsterDisplayModel>();
        foreach (MonsterState state in m_battle.PlayerMonsters)
            displayList.Add(CreateDisplayModel(state));
        displayList.ToArray();
        */

        m_battleView.HideSelectMenu();
        m_battleView.ShowMonsterSelect(
            m_battle.PlayerMonsters.Select(
                state=>CreateDisplayModel(state)).ToArray(),
            pIsClosable
            );
    }

    public void OnChangeSelectButton(Int32 pIndex) 
        //실제로 포켓몬을 선택했을때
    {
        if(!m_battle.ChangePlayerMonsterTo(pIndex))
        {
            //그 포켓몬으로 교체할 수 없어! 정도를 출력
            return;
        }

        IEnumerator MonsterSelectRoutine()
        {
            m_battleView.HideMonsterSelect();
            m_battleView.HideMonster(true);
            ShowText(TextID.POKEMON_CHANGE, m_battle.CurrentPlayerMonster.Data.Name);
            yield return new WaitForSeconds(1f);
            m_battleView.ShowMonster(
                CreateDisplayModel(m_battle.CurrentPlayerMonster), true);
            OnPlayerSkillButton(-1);
        }

        StartCoroutine(MonsterSelectRoutine());
    }
}