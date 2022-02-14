using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    public SpecialObjectHandler scoreData;
    [SerializeField] TextMeshProUGUI scoreDisplay;


    private void Start()
    {
        scoreData.score = 0;
    }

    void Update()
    {
        scoreDisplay.text = scoreData.score.ToString() + " / " + scoreData.scoreToWin;
    }
}
