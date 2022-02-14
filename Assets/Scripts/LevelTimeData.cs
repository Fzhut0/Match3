using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelTimingsData", menuName = "LevelData")]
public class LevelTimeData : ScriptableObject
{

    public float finishTimer;
    public bool timerStarted;


    private void Awake()
    {
        timerStarted = false;

    }
}
