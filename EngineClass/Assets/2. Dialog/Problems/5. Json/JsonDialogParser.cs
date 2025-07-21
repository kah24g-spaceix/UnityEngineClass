using Newtonsoft.Json;
using System;

public class JsonDialogParser : ITextParser<DialogData>
{
    /*  TODO :: 주어진 String을 Json 포맷에 의해 Parse 하여 DialogData로 반환한다.
     *  
     *  매개변수로 주어진 String을 Json 포맷에 따라 DiloagLine으로 파싱하고 이를 모아
     *  DialogLine의 array를 만든 뒤 이를 lines에 할당하자. 
     *  
     *  작성한 코드의 실행이 완료되면 DialogData를 만들어 return 할 것이다.
     */

    /// <summary>
    /// 타 클래스에서 데이터 Parse가 호출되면 이 함수가 호출된다.
    /// </summary>
    public DialogData Parse(String data)
    {
        DialogLine[] lines = null;

        ///////////////////////////////////////////
        // 이곳에 코드를 작성
        lines = JsonConvert.DeserializeObject<DialogLine[]>(data);
        ///////////////////////////////////////////

        return new DialogData(lines);
    }
}
