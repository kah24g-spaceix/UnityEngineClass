using Newtonsoft.Json;
using System;

public class JsonDialogParser : ITextParser<DialogData>
{
    /*  TODO :: �־��� String�� Json ���˿� ���� Parse �Ͽ� DialogData�� ��ȯ�Ѵ�.
     *  
     *  �Ű������� �־��� String�� Json ���˿� ���� DiloagLine���� �Ľ��ϰ� �̸� ���
     *  DialogLine�� array�� ���� �� �̸� lines�� �Ҵ�����. 
     *  
     *  �ۼ��� �ڵ��� ������ �Ϸ�Ǹ� DialogData�� ����� return �� ���̴�.
     */

    /// <summary>
    /// Ÿ Ŭ�������� ������ Parse�� ȣ��Ǹ� �� �Լ��� ȣ��ȴ�.
    /// </summary>
    public DialogData Parse(String data)
    {
        DialogLine[] lines = null;

        ///////////////////////////////////////////
        // �̰��� �ڵ带 �ۼ�
        lines = JsonConvert.DeserializeObject<DialogLine[]>(data);
        ///////////////////////////////////////////

        return new DialogData(lines);
    }
}
