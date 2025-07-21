using System;
using System.Collections.Generic;
using UnityEngine;



public class GameController : MonoBehaviour
{
    private Int32 m_score;

    public Int32 Score => m_score;

    public event Action<Int32> OnScoreChange;

    public void AddScore(Int32 pScore)
    {
        m_score = pScore;
        OnScoreChange?.Invoke(m_score);
    }
}