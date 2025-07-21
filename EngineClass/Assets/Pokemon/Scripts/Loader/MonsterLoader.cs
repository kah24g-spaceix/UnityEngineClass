using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Unity.Burst.CompilerServices;


public class MonsterLoader
{
    private Dictionary<String, MonsterData> m_monsterDatas;
    private Dictionary<String, MonsterSkillData> m_monsterSkillDatas;

    public MonsterLoader()
    {
        m_monsterDatas = new Dictionary<string, MonsterData>();
        m_monsterSkillDatas = new Dictionary<string, MonsterSkillData>();
    }


    public void Init(String pMonsterData,String pSkillData)
    {
        /*
         * TODO :: 매개변수로 들어온 CSV 형태의 데이터 pMonsterData와 pSkillData를 파싱해서
         *         m_monsterDatas와 m_monsterSkillDatas에 넣자.
         *         각각의 Dictionary의 Key는 불러온 데이터에서의 ID여야한다.
         *         
         *  pMonsterData는 몬스터 데이터에 관한 CSV (Pokemon/Datas/MonsterData.txt파일 참고)
         *  데이터 순서는 ID, 이름, 체력, 공격, 수비, 속도, 타입, 스킬ID1, 스킬ID2, 스킬ID3, 스킬ID4이다
         *  
         *  pSkillData는 몬스터의 스킬에 관한 CSV (Pokemon/Datas/MonsterSkillData.txt파일 참고)
         *  데이터 순서는 ID, 이름, 공격, 명중, 타입 이다.
         *  
         *  2일차 데이터 관리때 했던 내용을 참고해서 CSV를 파싱해 MosnterData와 MonsterSkillData를 만든다.
         *  데이터 내에서 CSV의 형태를 위한 구분자 외에는 콤마는 없다. 즉, 문자열 내에서 콤마가 있을 걱정은 하지 않아도 된다.
         */

        /* 사용 가능한 함수
         * 
         * 문자열로 되어있는 숫자를 Int로 바꿔주는 함수
         * Int32.Parse(s)
         * 사용예시
         * String s = "123";
         * Int32 value = Int32.Parse(s);
         * 하면 value는 123이 된다. 
         * 
         * 문자열로 되어있는 Enum을 Enum으로 바꿔주는 함수
         * Enum.Parse<Type>(s)
         * 사용예시
         * String s = "Normal"
         * MonsterType type = Enum.Parse<MonsterType>(s);
         * 하면 type은 MonsterType.Normal이 된다.
         */


        foreach (String line in pMonsterData.Split('\n'))
        {
            if (String.IsNullOrEmpty(line))
                continue;

            String[] splitted = line.Split(',');
            m_monsterDatas.Add(splitted[0], new MonsterData(
                splitted[0],
                splitted[1],
                Int32.Parse(splitted[2]),
                Int32.Parse(splitted[3]),
                Int32.Parse(splitted[4]),
                Int32.Parse(splitted[5]),
                Enum.Parse<MonsterType>(splitted[6]),
                new string[]
                {
                    splitted[7],
                    splitted[8],
                    splitted[9],
                    Regex.Replace(splitted[10], @"\s+", ""),
                }
                ));
        }

        foreach (String line in pSkillData.Split('\n'))
        {
            if (String.IsNullOrEmpty(line))
                continue;

            String[] splitted = line.Split(',');
            m_monsterSkillDatas.Add(splitted[0], new MonsterSkillData(
                splitted[0],
                splitted[1],
                Int32.Parse(splitted[2]),
                Int32.Parse(splitted[3]),
                Enum.Parse<MonsterType>(splitted[4])
                ));
        }

    }

    /// <summary>
    /// 새로운 MonsterState를 만들어주는 함수.
    /// </summary>
    /// <param name="pID">원하는 몬스터의 ID</param>
    /// <returns>만들어진 MonsterState가 반환된다.</returns>
    /// <exception cref="KeyNotFoundException">ID에 대한 데이터가 없는 경우</exception>
    public MonsterState CreateMonsterState(String pID)
    {
        if (!m_monsterDatas.TryGetValue(pID, out var monsterData))
            throw new KeyNotFoundException($"{pID} is not found from monsterData");

        List<MonsterSkillState> skillStateList = new List<MonsterSkillState>();

        foreach (String skillID in monsterData.SkillID)
        {
            MonsterSkillState skillState = CreateSkillState(skillID);
            skillStateList.Add(skillState);
        }

        return new MonsterState(monsterData, skillStateList.ToArray());
    }


    /// <summary>
    /// 새로운 MonsterSkillState를 만들어 주는 함수.
    /// </summary>
    /// <param name="pID">원하는 스킬의 ID</param>
    /// <returns>만들어진 MonsterSkillState가 반환된다.</returns>
    /// <exception cref="KeyNotFoundException">ID에 대한 데이터가 없는 경우</exception>
    private MonsterSkillState CreateSkillState(String pID)
    {
        if (m_monsterSkillDatas.TryGetValue(pID, out var skillData))
            return new MonsterSkillState(skillData);

        throw new KeyNotFoundException($"[{pID}] is not found from skillData");
    }
}