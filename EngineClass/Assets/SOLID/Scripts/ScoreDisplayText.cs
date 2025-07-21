using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayText : MonoBehaviour
{
    [SerializeField] private Text m_scoreText;

    private void Awake()
    {
        FindObjectOfType<GameController>().OnScoreChange += OnScoreChange;
    }

    private void OnScoreChange(int obj)
    {
        m_scoreText.text = obj.ToString();
    }
}