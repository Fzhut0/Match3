using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelTimerHandler : MonoBehaviour
{
    public LevelTimeData levelData;
    [SerializeField] TextMeshProUGUI levelTimer;
    [SerializeField] GameObject lostLevel;
    public int levelFinishTime;
    private void OnEnable()
    {
        lostLevel.SetActive(false);
        levelData.timerStarted = false;
        levelData.finishTimer = levelFinishTime;
    }
    private void Update()
    {
        if (levelData.timerStarted)
        {
            Time.timeScale = 1f;
            levelData.finishTimer -= Time.deltaTime;
            float currentTime = Mathf.Round(levelData.finishTimer);
            levelTimer.text = currentTime.ToString();
        }
        if (levelData.finishTimer <= 0)
        {
            Time.timeScale = 0f;
            lostLevel.SetActive(true);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void PressStartButton()
    {
        levelData.timerStarted = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
