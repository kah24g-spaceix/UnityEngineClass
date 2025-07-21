using System;

public class ScripttedLottery : ILotteryLogic
{
    private readonly IScriptEnv m_scriptEnv;

    public ScripttedLottery(IScriptEnv pEnv)
    {
        m_scriptEnv = pEnv;
    }
    public bool DoLottery()
    {
        Boolean result = (Boolean)m_scriptEnv.RunFunction("DoLottery");
        return result;
    }
}