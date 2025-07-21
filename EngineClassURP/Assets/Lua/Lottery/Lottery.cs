using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using NLua;
using Zenject;

public class Lottery : MonoBehaviour
{
    [Inject] private readonly ILotteryLogic m_lotteryLogic;

    [InspectorButton("DoLottery")]
    private void DoLottery()
    {
        Boolean result = m_lotteryLogic.DoLottery();

        if (result)
            Debug.Log("success");
        else
            Debug.Log("fail");
    }
}