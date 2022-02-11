using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tile and ball data")]
public class BoardData : ScriptableObject
{
    public List<Vector2> tilesList = new List<Vector2>();
    public GameObject[] ballPrefabs;

    private void OnEnable()
    {
        tilesList.Clear();
    }
}

