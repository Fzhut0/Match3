using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int width;
    public int height;
    [SerializeField] GameObject backgroundPrefab;
    public GameObject[] balls;
    private BackgroundGrid[,] allTiles;
    public GameObject[,] allBalls;

    private void Awake()
    {


    }

    private void Start()
    {
        allTiles = new BackgroundGrid[width, height];
        allBalls = new GameObject[width, height];
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
                int ballToUse = Random.Range(0, balls.Length);

                GameObject ball = Instantiate(balls[ballToUse], spawnPos, Quaternion.identity);
                ball.transform.parent = backgroundTile.transform;
                ball.name = gameObject.name;
                allBalls[x, y] = ball;
            }
        }


    }


}
