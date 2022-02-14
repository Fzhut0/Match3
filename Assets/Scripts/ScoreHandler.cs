using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreHandler : MonoBehaviour
{
    public SpecialObjectHandler scoreData;
    [SerializeField] TextMeshProUGUI scoreDisplay;
    [SerializeField] TextMeshProUGUI levelInfo;


    private void Start()
    {
        scoreData.score = 0;
        levelInfo.text = "You need " + scoreData.scoreToWin + " score to win";
    }

    void Update()
    {
        // scoreDisplay.text = scoreData.score.ToString();
        scoreDisplay.text = scoreData.score.ToString() + " / " + scoreData.scoreToWin;
        if (scoreData.score >= scoreData.scoreToWin)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
