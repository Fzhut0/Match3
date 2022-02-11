using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int width;
    public int height;
    [SerializeField] GameObject backgroundPrefab;


    private void Awake()
    {

        SpawnBackground();
    }



    void SpawnBackground()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 spawnPos = new Vector2(x, y);
                GameObject backgroundTile = Instantiate(backgroundPrefab, spawnPos, Quaternion.identity);
                backgroundTile.transform.parent = gameObject.transform;
                backgroundTile.name = "(" + x + "," + y + ")";
            }
        }
    }


}
