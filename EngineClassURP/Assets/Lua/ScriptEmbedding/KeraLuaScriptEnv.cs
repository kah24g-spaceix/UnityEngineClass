using NLua;
using UnityEngine;
public class KeraLuaScriptEnv : 
    Singleton<KeraLuaScriptEnv>,
    IScriptEnv
{
    private Lua m_lua;

    private void Awake()
    {
        m_lua = new Lua();
        m_lua.State.Encoding = System.Text.Encoding.UTF8;
        m_lua.LoadCLRPackage();
        m_lua.DoString(@"import ('UnityEngine')");
        Reload();
    }
    public void DoString(string pScript)
    {
        m_lua.DoString(pScript);
    }

    public T GetObject<T>(string pObjectName)
    {
        return (T)m_lua[pObjectName];
    }
    public object RunFunction(string pFunctiobName, params object[] args)
    {
        LuaFunction function = m_lua.GetFunction(pFunctiobName);
        return function.Call(args)[0];
    }
    [InspectorButton("Reload")]
    public void Reload()
    {
        TextAsset[] luaAssets
            = Resources.LoadAll<TextAsset>("Lua");
        foreach (var luaAsset in luaAssets)
        {
            DoString(luaAsset.text);
        }
        Debug.Log("Loaded Lua Scripts");
    }
}