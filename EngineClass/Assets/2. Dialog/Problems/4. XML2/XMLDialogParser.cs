using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XMLDialogParser : ITextParser<DialogData>
{
    /*  TODO :: 주어진 String을 XML 포맷에 의해 Parse 하여 DialogData로 반환한다.
     *  
     *  매개변수로 주어진 String을 XML 포맷에 따라 DiloagLine으로 파싱하고 이를 모아
     *  DialogLine의 list인 lineList에 넣자. 
     *  
     *  작성한 코드의 실행이 완료되면 list의 ToArray 메서드가 호출되어 DialogData를 만들어 return 할 것이다.
     */

    /// <summary>
    /// 타 클래스에서 데이터 Parse가 호출되면 이 함수가 호출된다.
    /// </summary>
    public DialogData Parse(String data)
    {
        List<DialogLine> dialogLineList = new List<DialogLine>();

        ///////////////////////////////////////////
        // 이곳에 코드를 작성
        XmlDocument document = new XmlDocument();
        document.LoadXml(data);

        XmlNodeList nodeList = document.FirstChild.ChildNodes;
        foreach (XmlNode node in nodeList)
        {
            DialogLine dialogLine = ParseDialogLine(node);
            dialogLineList.Add(dialogLine);
        }
        ///////////////////////////////////////////

        return new DialogData(dialogLineList.ToArray());
    }

    //재귀적인 Parse를 위한 함수
    private DialogLine ParseDialogLine(XmlNode node)
    {
        ///////////////////////////////////////////
        // 이곳에 코드를 작성
        if(node.HasChildNodes && node.FirstChild.NodeType != XmlNodeType.Text)
        {
            List<DialogLine[]> optionList = new List<DialogLine[]>();
            foreach (XmlNode selectElement in node.ChildNodes) 
            {
               List<DialogLine> currentOption = new List<DialogLine>();
                DialogLine selectLine = 
                    new DialogLine("", selectElement.Attributes["text"].Value,"");
                currentOption.Add(selectLine);

                foreach (XmlNode selectChildElement in selectElement.ChildNodes)
                {
                    DialogLine line = ParseDialogLine(selectChildElement);
                    currentOption.Add(line);
                }

                optionList.Add(currentOption.ToArray());
            }
            DialogLine[][] options = optionList.ToArray();

            String selectTalker = node.Attributes["talker"].Value;
            String selectText = node.Attributes["text"].Value;
            String selectSprite = node.Attributes["sprite"].Value;

            return new DialogLine(selectTalker, selectText, selectSprite, options);
        }
        
        String talker = node.Attributes["talker"].Value;
        String text = node.InnerText;
        String sprite = node.Attributes["sprite"].Value;

        return new DialogLine(talker, text, sprite);
        ///////////////////////////////////////////
    }
}
