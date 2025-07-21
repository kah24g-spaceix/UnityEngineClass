using System;

public enum SkillHitPower
{
    None, //�׷��� �����ϰ� ���Ҵ�

    NoEffect, //ȿ���� ���� �� �ϴ�... 0 �븻 > ��Ʈ
    Normal, //ȿ���� ������ �� �ϴ� 1 �� �� 
    Great //ȿ���� �����ߴ�! 2 �� > �� 
} 

public struct SkillResult //��
{
    public Boolean IsCritical { get; } //�޼ҿ� �¾Ҵ�!
    public Boolean IsSuccess { get; }
    public SkillHitPower HitPower { get; }

    public SkillResult(Boolean pIsCritical,
        Boolean pIsSuccess,SkillHitPower pPower)
    {
        IsCritical = pIsCritical;
        IsSuccess = pIsSuccess;
        HitPower = pPower;
    }
}

public class MonsterBattle //����
{
    private Random m_random;

    private MonsterState[] m_playerMonsters;
    private MonsterState m_opponentMonsterState;
    private MonsterState m_currentPlayerMonster;

    public MonsterState[] PlayerMonsters => m_playerMonsters;
    public MonsterState CurrentPlayerMonster => m_currentPlayerMonster;
    public MonsterState OpponentMonster => m_opponentMonsterState;


    public MonsterBattle()
    {
        m_random = new Random();
    }

    public void StartBattle(MonsterState[] playerMonsters,
        MonsterState pOpponentMonster)
    {
        m_playerMonsters = playerMonsters;
        m_opponentMonsterState = pOpponentMonster;

        m_currentPlayerMonster = m_playerMonsters[0];
    }

    //MonsterBattle :: ChangePlayerMonsterTo
    public Boolean ChangePlayerMonsterTo(Int32 pIndex)
    {
        MonsterState target = m_playerMonsters[pIndex];

        if (!target.IsAlive || target == m_currentPlayerMonster)
            return false;

        //�� ���� ����
        m_currentPlayerMonster = target;

        return true;
    }

    public Boolean HasChangableMonster()
    {
        foreach (var monster in m_playerMonsters)
        {
            if (monster.IsAlive)
                return true;
        }

        return false;
    }

    public Boolean IsPlayerAttackFirst()
        => m_currentPlayerMonster.Data.Speed 
            > m_opponentMonsterState.Data.Speed;

    public MonsterState GetFirstAttack()
        => IsPlayerAttackFirst() ?
            m_currentPlayerMonster : m_opponentMonsterState;
    public MonsterState GetSecondAttack()
        => IsPlayerAttackFirst() ?
           m_opponentMonsterState : m_currentPlayerMonster;



    public SkillResult UseSkill(MonsterState pUser,
        MonsterState pTarget,Int32 pIndex)
    {
        MonsterSkillState skillState = pUser.SkillStates[pIndex];

        //��������
        if (m_random.Next(0, 100) > skillState.Data.HitRate)
            return new SkillResult(false, false, SkillHitPower.None);

        //�޼� ����
        Boolean isCritical = m_random.Next(0,100) <= 30;

        //���� �Ӽ� ����
        Boolean isSameType = skillState.Data.Type == pUser.Data.Type;

        //�Ӽ� ����
        SkillHitPower power = SkillHitPower.Normal;
        if ((Int32)skillState.Data.Type > (Int32)pTarget.Data.Type)
            power = SkillHitPower.Great;
        else if ((Int32)skillState.Data.Type == (Int32)pTarget.Data.Type)
        {
            power = SkillHitPower.NoEffect;
            isCritical = false; //ȿ���� ���µ� �޼Ҹ� �ȵ�
        }
        else
            power = SkillHitPower.Normal;

        Int32 damage = skillState.Data.Attack;

        if (isCritical)
            damage *= 2;
        if (isSameType)
            damage *= 2;
        if(power == SkillHitPower.Great)
            damage *= 2;

        damage += pUser.Data.Attack;

        if (power == SkillHitPower.NoEffect)
            damage = 0;

        damage -= pTarget.Data.Deffence;

        if(damage <= 0)
            damage = 0;

        pTarget.TakeDamage(damage);
        return new SkillResult(isCritical, true, power);
    }
}