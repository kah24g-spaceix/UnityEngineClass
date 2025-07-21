using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XMLSimpleDialogParser : ITextParser<SimpleDialogData>
{

   /*  TODO :: �־��� String�� XML ���˿� ���� Parse �Ͽ� SimpleDialogData�� ��ȯ�Ѵ�.
    *  
    *  �Ű������� �־��� String�� XML ���˿� ���� ���ϴ� ���, ���ϴ� ����, �׷��� sprite�� ���� �̸����� ������ 
    *  SimpleDialogLine�� list�� lineList�� ����. 
    *  
    *  �ۼ��� �ڵ��� ������ �Ϸ�Ǹ� list�� ToArray �޼��尡 ȣ��Ǿ� SimpleDialogData�� ����� return �� ���̴�.
    */

    /// <summary>
    /// Ÿ Ŭ�������� ������ Parse�� ȣ��Ǹ� �� �Լ��� ȣ��ȴ�.
    /// </summary>

    public SimpleDialogData Parse(String pData)
    {
        List<SimpleDialogLine> dialogLines = new List<SimpleDialogLine>();

        ///////////////////////////////////////////
        // �̰��� �ڵ带 �ۼ�
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
