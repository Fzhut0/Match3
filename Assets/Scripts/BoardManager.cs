using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    wait,
    move
}

public class BoardManager : MonoBehaviour
{
    public GameState currentState = GameState.move;
    public int width;
    public int height;
    public int offset;

    [SerializeField] GameObject backgroundPrefab;
    public GameObject[] balls;
    private BackgroundGrid[,] allTiles;
    public GameObject[,] allBalls;

    private MatchFinder findMatches;

    private void Start()
    {
        findMatches = FindObjectOfType<MatchFinder>();
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
                Vector2 spawnPos = new Vector2(x, y + offset);
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
                maxIterations = 0;

                GameObject ball = Instantiate(balls[ballToUse], spawnPos, Quaternion.identity);
                ball.GetComponent<Ball>().row = y;
                ball.GetComponent<Ball>().column = x;

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

    void CheckDestroyMatches(int column, int row)
    {
        if (allBalls[column, row].GetComponent<Ball>().isMatched)
        {
            findMatches.currentMatches.Remove(allBalls[column, row]);
            Destroy(allBalls[column, row]);
            allBalls[column, row] = null;
        }
    }

    public void DestroyMatches()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allBalls[x, y] != null)
                {
                    CheckDestroyMatches(x, y);
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
        StartCoroutine(FillBalls());
    }


    void AddNewBalls()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allBalls[x, y] == null)
                {
                    Vector2 tempPos = new Vector2(x, y + offset);
                    int ballToUse = Random.Range(0, balls.Length);
                    GameObject ball = Instantiate(balls[ballToUse], tempPos, Quaternion.identity);
                    allBalls[x, y] = ball;
                    ball.GetComponent<Ball>().row = y;
                    ball.GetComponent<Ball>().column = x;
                }
            }
        }
    }

    private bool MatchesOnBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allBalls[x, y] != null)
                {
                    if (allBalls[x, y].GetComponent<Ball>().isMatched)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    IEnumerator FillBalls()
    {
        AddNewBalls();
        yield return new WaitForSeconds(.5f);

        while (MatchesOnBoard())
        {
            yield return new WaitForSeconds(.5f);
            DestroyMatches();
        }
        yield return new WaitForSeconds(.5f);
        currentState = GameState.move;
    }
}
