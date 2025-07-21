using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

public enum TextID
{
    POKEMON_ENCOUNTER,
    POKEMON_GO,
    POKEMON_FAINT,
    POKEMON_CHANGE,
    
    SKILL_USE,
    SKILL_FAIL,
    SKILL_HIT_GREAT,
    SKILL_HIT_NORMAL,
    SKILL_HIT_NOEFFECT,
    SKILL_HIT_CRITICAL,
    
    MESSAGE_WHAT_TO_DO,
    MESSAGE_GAME_WIN,
    MESSAGE_GAME_LOSE,
    MESSAGE_GAME_LOSE_2,
}

public class TextLoader
{
    private Dictionary<TextID, String> m_textByID;
    public TextLoader()
    {
        m_textByID = new Dictionary<TextID, string>();
    }

    public void Load(String pData)
    {
        /*
         * TODO :: 매개변수로 들어온 XML 형태의 데이터 pData를 파싱해서
         *         m_textBtyID
         *         각각의 Dictionary의 Key는 불러온 데이터에서의 ID여야한다.
         *         
         *  pData는 텍스트 데이터에 관한 XML (Pokemon/Datas/TextData.txt파일 참고)
         *  Attribute 로 적혀있는 ID를 ID로, inner text를 값으로 가져온다.
         *  
         *  2일차 데이터 관리때 했던 내용을 참고해서 XML을 파싱해보자.
         *  
         *  
         *  또, BattlePresenter에 적혀있는 문자열들 (ex : "눈앞이 캄캄해졌다!" , "전투에서 승리했다!")
         *  들에 각각 ID를 부여해서, 그 ID를 TextID enum에 추가하고, TextData.xml에 추가한다.
         *  그 후 BattlePresenter에서 이 TextLoader를 사용해서 텍스트를 불러온다. 
         *  예시로, "전투에서 승리했다!" 라는 텍스트에 BATTLE_WIN 이라는 ID를 부여하면 
         *  1. enum TextID에 BATTLE_WIN을 추가하고
         *  2. <Text ID="BATTLE_WIN">전투에서 승리했다!</Text> 를 xml 데이터에 TextData의 자식으로 넣는다. 순서는 상관이 없다.
         *  3. BattlePresenter에서 
         *     m_view.ShowText($"전투에서 승리했다!"); 를
         *     m_view.ShowText(m_battleLoader.GetString(TextID.BATTLE_WIN)); 으로 교체한다. 
         *  즉, BattlePresenter에 남아있는 모든 String Literal을 제거하는것을 목표로 한다.
         *  
         *  이 Load함수를 잘 작성했다면, 데이터를 추가할 때에 TextID에 추가하는것 외에는 TextLoader를 수정할 일이 없어야 한다.
         */

        /* 사용 가능한 함수
        * 
        * 문자열로 되어있는 Enum을 Enum으로 바꿔주는 함수
        * Enum.Parse<Type>(s)
        * 사용예시
        * String s = "Normal"
        * MonsterType type = Enum.Parse<MonsterType>(s);
        * 하면 type은 MonsterType.Normal이 된다.
        */


        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(pData);

        foreach (XmlNode node in xmlDoc.FirstChild.ChildNodes)
        {
            String ID = node.Attributes["ID"].Value;
            String data = node.InnerText;
            if (Enum.TryParse<TextID>(ID, out TextID textID))
            {
                m_textByID.Add(textID, data);
            }
            else
            {
                UnityEngine.Debug.LogWarning($"{ID} is not found in TEXTID enum");
            }
        }
    }

    /// <summary>
    /// TextID에 따른 실제 String값을 불러오는 함수. 
    /// 가변 인자로 object들을 넣어 스트링 포맷도 사용할 수 있다.
    /// </summary>
    /// <param name="pID">원하는 텍스트의 ID</param>
    /// <param name="pFormats">스트링 포맷에 사용할 데이터</param>
    /// <returns></returns>

    public String GetString(TextID pID, params Object[] pFormats)
    {
        if (!m_textByID.TryGetValue(pID, out String text))
        {
            UnityEngine.Debug.LogWarning($"{pID} is not found");
            return pID.ToString();
        }

        return String.Format(text, pFormats);

        /*
         *  스트링 포맷 사용법
         *  String.Format은 C에서 사용헀던 sprintf와 비슷하다.
         *  예를 들어, "안녕하세요, {0} 님. 지금은 {1} 시 입니다." 
         *  라는 문자열에, 
         *  String.Format(s,"김누구",4) 하면
         *  "안녕하세요, 김누구 님. 지금은 4 시 입니다. 가 반환된다.
         *  
         *  String.Format의 2번째 인자부터는 가변 인자로 들어오는데, 
         *  가변 인자는 사실 배열이기 때문에 배열을 넣으면 그대로 사용할 수 있다.
         */
    }
}