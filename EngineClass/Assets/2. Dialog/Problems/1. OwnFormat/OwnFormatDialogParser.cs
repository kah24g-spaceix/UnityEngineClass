using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnFormatDialogParser : ITextParser<SimpleDialogData>
{
    /*  TODO :: 주어진 String을 자체적인 포맷에 의해 Parse 하여 SimpleDialogData로 반환한다.
     *  
     *  매개변수로 주어진 String을 자신이 정한 포맷에 따라 말하는 사람, 말하는 내용, 그려질 sprite의 파일 이름으로 나누어 
     *  SimpleDialogLine의 list인 lineList에 넣자. 
     *  
     *  작성한 코드의 실행이 완료되면 list의 ToArray 메서드가 호출되어 SimpleDialogData를 만들어 return 할 것이다.
     */

    /*  사용 가능한 함수 설명
     * 
     *  String.Split()
     *  해당 string을 주어진 구분자에 의해 자른다.
     *  반환형 
     *      - String[] (잘라진 문자열의 배열)
     *  매개변수
     *      - Char (구분자)
     *      
     *  String.Split() 예시
     *  
     *  String helloWorld = "hello, world!";
     *  String[] splitted = helloWorld.Split(',');
     *  
     *  이 코드의 실행 이후
     *  splitted[0] 은 "hello"
     *  splitted[1] 은 " world!"
     *  이다.
     */

    /// <summary>
    /// 타 클래스에서 데이터 Parse가 호출되면 이 함수가 호출된다.
    /// </summary>
    public SimpleDialogData Parse(String pData)
    {
        List<SimpleDialogLine> lineList = new List<SimpleDialogLine>();

        ///////////////////////////////////////////
        // 이곳에 코드를 작성
        String[] lines = pData.Split('\n');

        for(Int32 i = 0; i < lines.Length; i += 3)
        {
            String talker = lines[i];
            String talkText = lines[i + 1];
            String sprite = lines[i + 2];

            SimpleDialogLine currentLine
                = new SimpleDialogLine(talker,talkText,sprite);
            lineList.Add(currentLine);
        }

        ///////////////////////////////////////////

        return new SimpleDialogData(lineList.ToArray());
    }
}
