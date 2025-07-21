using System;
public interface IScriptEnv
{
    void DoString(String pScript);
    void Reload();
    object RunFunction(
        String pFunctiobName, params Object[] args);

    T GetObject<T>(String pObjectName);
}