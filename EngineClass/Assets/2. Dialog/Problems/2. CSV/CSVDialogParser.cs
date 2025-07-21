using System;
using System.Collections.Generic;
using System.Xml;

public class CSVDialogParser : ITextParser<SimpleDialogData>
{
    /*  TODO :: �־��� String�� CSV ���˿� ���� Parse �Ͽ� SimpleDialogData�� ��ȯ�Ѵ�.
     *  
     *  �Ű������� �־��� String�� CSV ���˿� ���� ���ϴ� ���, ���ϴ� ����, �׷��� sprite�� ���� �̸����� ������ 
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

    /*  CSV ����
     * 
     *  CSV �� Comma-Separated Values�� ���ڷ� Comma(��ǥ ',')fh ���е� �����̶�� �ǹ��̴�.
     *  ���� ��� ������ ���� �����Ͱ� �ִٰ� �غ���.
     *  ö���� 21���̰� �����̴�. ����� 22���̰� �����̴�. 
     *  �� �����͸� CSV �������� ��Ÿ���� ������ ����.
     *  
     *  ö��,21,����
     *  ����,22,����
     *  
     *  �� ���� �����͵��� Comma(',')�� �����ϸ� ���� �ٹٲ����� �����Ѵ�.
     *  �̶� ��� ���� ���� ������ ���� ������ �Ѵ�.
     */

    /// <summary>
    /// Ÿ Ŭ�������� ������ Parse�� ȣ��Ǹ� �� �Լ��� ȣ��ȴ�.
    /// </summary>
    public SimpleDialogData Parse(String pData)
    {
        List<SimpleDialogLine> lineList = new List<SimpleDialogLine>();

        ///////////////////////////////////////////
        // �̰��� �ڵ带 �ۼ�
        String[] lines = pData.Split('\n',',');

        for (Int32 i = 0; i < lines.Length; i += 3)
        {
            String talker = lines[i];
            String talkText = lines[i + 1];
            String sprite = lines[i + 2];

            SimpleDialogLine currentLine
                = new SimpleDialogLine(talker, talkText, sprite);
            lineList.Add(currentLine);
        }
        ///////////////////////////////////////////

        return new SimpleDialogData(lineList.ToArray());
    }
}
