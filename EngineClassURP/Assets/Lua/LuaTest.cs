using UnityEngine;
using NLua;
using System.Text;
public class LuaTest : MonoBehaviour
{
    [SerializeField, TextArea] private string m_luaScript;
    private Lua m_lua;
    public int randomValue;
    public object[] result;
    LuaFunction randomFunc;

    private void Awake()
    {
        m_lua = new Lua();
        m_lua.State.Encoding = Encoding.UTF8;
        m_lua.LoadCLRPackage();
        m_lua.DoString("import ('UnityEngine')");
        m_lua.DoString(@"
            math.randomseed(os.time())  -- 시드 설정
            function getRandomValue(min, max)
                local randomValue = math.random(min, max)
                Debug.Log('Random Value: ' .. randomValue)
                return randomValue
            end
        ");

        randomFunc = m_lua.GetFunction("getRandomValue");
    }

    [InspectorButton("RunLua")] // 리플렉션
    private void RunLua()
    {
        m_lua.DoString(m_luaScript);
    }
}