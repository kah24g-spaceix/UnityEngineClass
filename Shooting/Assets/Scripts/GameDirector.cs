using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    GameObject hpGauge; 
    GameObject score;

    int tempScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        hpGauge = GameObject.Find("hpGauge");
        score = GameObject.Find("Score");
    }

    // Update is called once per frame
    public void DecreaseHP()
    {
        hpGauge.GetComponent<Slider>().value -= 0.1f;
    }
    public void AddScore()
    {
        tempScore += 100;
        score.GetComponent<TextMeshProUGUI>().SetText("Score : " + tempScore);
    }
}
