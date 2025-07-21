using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XMLSimpleDialogParser : ITextParser<SimpleDialogData>
{

   /*  TODO :: 주어진 String을 XML 포맷에 의해 Parse 하여 SimpleDialogData로 반환한다.
    *  
    *  매개변수로 주어진 String을 XML 포맷에 따라 말하는 사람, 말하는 내용, 그려질 sprite의 파일 이름으로 나누어 
    *  SimpleDialogLine의 list인 lineList에 넣자. 
    *  
    *  작성한 코드의 실행이 완료되면 list의 ToArray 메서드가 호출되어 SimpleDialogData를 만들어 return 할 것이다.
    */

    /// <summary>
    /// 타 클래스에서 데이터 Parse가 호출되면 이 함수가 호출된다.
    /// </summary>

    public SimpleDialogData Parse(String pData)
    {
        List<SimpleDialogLine> dialogLines = new List<SimpleDialogLine>();

        ///////////////////////////////////////////
        // 이곳에 코드를 작성
        XmlDocument document = new XmlDocument();
        document.LoadXml(pData);

        XmlNodeList nodeList = document.FirstChild.ChildNodes; //lines
        foreach (XmlNode node in nodeList)
        {
            String talker = node.Attributes["talker"].Value;
            String text = node.InnerText;
            String sprite = node.Attributes["sprite"].Value;

            SimpleDialogLine line = new SimpleDialogLine(talker, text, sprite);
            dialogLines.Add(line);
        }
        ///////////////////////////////////////////

        return new SimpleDialogData(dialogLines.ToArray());
    }
}
