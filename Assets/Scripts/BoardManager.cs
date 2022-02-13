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
                int maxIterations = 0;
                while (SpawnMatchCheck(x, y, balls[ballToUse]) && maxIterations < 100)
                {
                    ballToUse = Random.Range(0, balls.Length);
                    maxIterations++;

                }
                GameObject ball = Instantiate(balls[ballToUse], spawnPos, Quaternion.identity);
                ball.transform.parent = backgroundTile.transform;
                ball.name = gameObject.name;
                allBalls[x, y] = ball;
            }
        }
    }

    private bool SpawnMatchCheck(int column, int row, GameObject tile)
    {
        if (column > 1 && row > 1)
        {
            if (allBalls[column - 1, row].tag == tile.tag && allBalls[column - 2, row])
            {
                return true;
            }
            if (allBalls[column, row - 1].tag == tile.tag && allBalls[column, row - 2])
            {
                return true;
            }
            else if (column <= 1 || row <= 1)
            {
                if (row > 1)
                {
                    if (allBalls[column, row - 1].tag == tile.tag && allBalls[column, row - 2].tag == tile.tag)
                    {
                        return true;
                    }
                }
                if (column > 1)
                {
                    if (allBalls[column - 1, row].tag == tile.tag && allBalls[column - 2, row].tag == tile.tag)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    void DestroyMatches(int column, int row)
    {
        if (allBalls[column, row].GetComponent<Ball>().isMatched)
        {
            Destroy(allBalls[column, row]);
            allBalls[column, row] = null;
        }
    }

    public void CheckDestroyMatches()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allBalls[x, y] != null)
                {
                    DestroyMatches(x, y);
                }
            }
        }
        StartCoroutine(DecreaseRow());
    }

    IEnumerator DecreaseRow()
    {
        int nullCount = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allBalls[x, y] == null)
                {
                    nullCount++;
                }
                else if (nullCount > 0)
                {
                    allBalls[x, y].GetComponent<Ball>().row -= nullCount;
                    allBalls[x, y] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(.4f);
    }
}
