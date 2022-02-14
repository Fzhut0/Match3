using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BoardManager : MonoBehaviour
{
    public SpecialObjectHandler specialObject;
    public BoardData data;
    public GameState currentState = GameState.move;
    [SerializeField] GameObject mainBallPrefab;


    private MatchFinder findMatches;



    private void OnEnable()
    {
        foreach (GameObject ball in data.balls)
        {
            if (ball.GetComponent<Ball>().boardData != data)
            {
                ball.GetComponent<Ball>().boardData = data;
            }
        }

    }

    private void Start()
    {
        findMatches = FindObjectOfType<MatchFinder>();
        data.allTiles = new BackgroundGrid[data.width, data.height];
        data.allBalls = new GameObject[data.width, data.height];
        SpawnBackground();
        Time.timeScale = 0f;
    }

    public void SpawnBackground()
    {
        for (int x = 0; x < data.width; x++)
        {
            for (int y = 0; y < data.height; y++)
            {
                Vector2 spawnPos = new Vector2(x, y + data.offset);
                GameObject backgroundTile = Instantiate(data.backgroundPrefab, spawnPos, Quaternion.identity);
                backgroundTile.transform.parent = gameObject.transform;
                backgroundTile.name = "(" + x + "," + y + ")";
                int ballToUse = Random.Range(0, data.balls.Length);
                int maxIterations = 0;
                while (SpawnMatchCheck(x, y, data.balls[ballToUse]) && maxIterations < 100)
                {
                    ballToUse = Random.Range(0, data.balls.Length);
                    maxIterations++;
                }


                GameObject ball = Instantiate(data.balls[ballToUse], spawnPos, Quaternion.identity);
                ball.GetComponent<Ball>().row = y;
                ball.GetComponent<Ball>().column = x;

                ball.transform.parent = backgroundTile.transform;
                ball.name = transform.name;
                data.allBalls[x, y] = ball;

            }
        }
    }

    private bool SpawnMatchCheck(int column, int row, GameObject tile)
    {
        if (column > 1 && row > 1)
        {
            if (data.allBalls[column - 1, row].tag == tile.tag && data.allBalls[column - 2, row].tag == tile.tag)
            {
                return true;
            }
            if (data.allBalls[column, row - 1].tag == tile.tag && data.allBalls[column, row - 2].tag == tile.tag)
            {
                return true;
            }
            else if (column <= 1 || row <= 1)
            {
                if (row > 1)
                {
                    if (data.allBalls[column, row - 1].tag == tile.tag && data.allBalls[column, row - 2].tag == tile.tag)
                    {
                        return true;
                    }
                }
                if (column > 1)
                {
                    if (data.allBalls[column - 1, row].tag == tile.tag && data.allBalls[column - 2, row].tag == tile.tag)
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
        if (data.allBalls[column, row].GetComponent<Ball>().isMatched)
        {
            if (findMatches.specialObject.currentMatches.Count == 5 || findMatches.specialObject.currentMatches.Count == 7)
            {
                findMatches.CheckForColorBomb();
            }
            if (findMatches.specialObject.currentMatches.Count == 4)
            {
                findMatches.CheckForMassBomb();
            }
            findMatches.specialObject.score += (data.allBalls[column, row].GetComponent<Ball>().scoreAmount);
            findMatches.specialObject.currentMatches.Remove(data.allBalls[column, row]);

            Destroy(data.allBalls[column, row]);
            data.allBalls[column, row] = null;
        }
    }

    public void DestroyMatches()
    {
        for (int x = 0; x < data.width; x++)
        {
            for (int y = 0; y < data.height; y++)
            {
                if (data.allBalls[x, y] != null)
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
        for (int x = 0; x < data.width; x++)
        {
            for (int y = 0; y < data.height; y++)
            {
                if (data.allBalls[x, y] == null)
                {
                    nullCount++;
                }
                else if (nullCount > 0)
                {
                    data.allBalls[x, y].GetComponent<Ball>().row -= nullCount;
                    data.allBalls[x, y] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(.2f);
        StartCoroutine(FillBalls());
    }


    void AddNewBalls()
    {
        for (int x = 0; x < data.width; x++)
        {
            for (int y = 0; y < data.height; y++)
            {
                if (data.allBalls[x, y] == null)
                {
                    Vector2 tempPos = new Vector2(x, y + data.offset);
                    int ballToUse = Random.Range(0, data.balls.Length);
                    GameObject ball = Instantiate(data.balls[ballToUse], tempPos, Quaternion.identity);
                    data.allBalls[x, y] = ball;
                    ball.GetComponent<Ball>().row = y;
                    ball.GetComponent<Ball>().column = x;
                }
            }
        }
    }

    private bool MatchesOnBoard()
    {
        for (int x = 0; x < data.width; x++)
        {
            for (int y = 0; y < data.height; y++)
            {
                if (data.allBalls[x, y] != null)
                {
                    if (data.allBalls[x, y].GetComponent<Ball>().isMatched)
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
        yield return new WaitForSeconds(.3f);

        while (MatchesOnBoard())
        {
            yield return new WaitForSeconds(.3f);
            DestroyMatches();
        }
        yield return new WaitForSeconds(.3f);
        currentState = GameState.move;
    }

    public void ClearBackground()
    {

        foreach (GameObject ball in data.allBalls)
        {
            if (ball != null)
            {
                Destroy(ball);
            }
        }
    }
}
