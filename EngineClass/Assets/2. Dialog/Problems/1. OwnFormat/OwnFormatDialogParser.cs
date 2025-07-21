using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnFormatDialogParser : ITextParser<SimpleDialogData>
{
    /*  TODO :: �־��� String�� ��ü���� ���˿� ���� Parse �Ͽ� SimpleDialogData�� ��ȯ�Ѵ�.
     *  
     *  �Ű������� �־��� String�� �ڽ��� ���� ���˿� ���� ���ϴ� ���, ���ϴ� ����, �׷��� sprite�� ���� �̸����� ������ 
     *  SimpleDialogLine�� list�� lineList�� ����. 
     *  
     *  �ۼ��� �ڵ��� ������ �Ϸ�Ǹ� list�� ToArray �޼��尡 ȣ��Ǿ� SimpleDialogData�� ����� return �� ���̴�.
     */

    /*  ��� ������ �Լ� ����
     * 
     *  String.Split()
     *  �ش� string�� �־��� �����ڿ� ���� �ڸ���.
     *  ��ȯ�� 
     *      - String[] (�߶��� ���ڿ��� �迭)
     *  �Ű�����
     *      - Char (������)
     *      
     *  String.Split() ����
     *  
     *  String helloWorld = "hello, world!";
     *  String[] splitted = helloWorld.Split(',');
     *  
     *  �� �ڵ��� ���� ����
     *  splitted[0] �� "hello"
     *  splitted[1] �� " world!"
     *  �̴�.
     */

    /// <summary>
    /// Ÿ Ŭ�������� ������ Parse�� ȣ��Ǹ� �� �Լ��� ȣ��ȴ�.
    /// </summary>
    public SimpleDialogData Parse(String pData)
    {
        List<SimpleDialogLine> lineList = new List<SimpleDialogLine>();

        ///////////////////////////////////////////
        // �̰��� �ڵ带 �ۼ�
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
