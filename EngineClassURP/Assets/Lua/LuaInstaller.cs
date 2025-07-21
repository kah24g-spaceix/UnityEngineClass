using UnityEngine;
using Zenject;

public class LuaInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IScriptEnv>()
            .FromInstance(KeraLuaScriptEnv.Instance)
            .AsSingle();

        Container.Bind<ILotteryLogic>()
            .To<ScripttedLottery>()
            .AsTransient();
    }
}