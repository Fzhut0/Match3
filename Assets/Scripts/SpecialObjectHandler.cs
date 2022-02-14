using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SpecialObjectData", menuName = "SpecialObjectManagerData")]
public class SpecialObjectHandler : ScriptableObject
{

    public List<GameObject> currentMatches = new List<GameObject>();

    public int score;
    public int scoreToWin;
}
