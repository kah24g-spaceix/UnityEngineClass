using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XMLDialogParser : ITextParser<DialogData>
{
    /*  TODO :: �־��� String�� XML ���˿� ���� Parse �Ͽ� DialogData�� ��ȯ�Ѵ�.
     *  
     *  �Ű������� �־��� String�� XML ���˿� ���� DiloagLine���� �Ľ��ϰ� �̸� ���
     *  DialogLine�� list�� lineList�� ����. 
     *  
     *  �ۼ��� �ڵ��� ������ �Ϸ�Ǹ� list�� ToArray �޼��尡 ȣ��Ǿ� DialogData�� ����� return �� ���̴�.
     */

    /// <summary>
    /// Ÿ Ŭ�������� ������ Parse�� ȣ��Ǹ� �� �Լ��� ȣ��ȴ�.
    /// </summary>
    public DialogData Parse(String data)
    {
        List<DialogLine> dialogLineList = new List<DialogLine>();

        ///////////////////////////////////////////
        // �̰��� �ڵ带 �ۼ�
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

    //������� Parse�� ���� �Լ�
    private DialogLine ParseDialogLine(XmlNode node)
    {
        ///////////////////////////////////////////
        // �̰��� �ڵ带 �ۼ�
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
