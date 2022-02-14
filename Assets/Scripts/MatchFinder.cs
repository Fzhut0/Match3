using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class MatchFinder : MonoBehaviour
{
    public BoardData boardData;
    public SpecialObjectHandler specialObject;
    public TextMeshProUGUI scoreDisplay;

    public List<GameObject> currentMatches = new List<GameObject>();


    private void Start()
    {
        specialObject.currentMatches.Clear();
    }
    private void Update()
    {
        scoreDisplay.text = currentMatches.Count.ToString();
    }


    IEnumerator FindAllMatches()
    {
        yield return new WaitForSeconds(.2f);
        for (int x = 0; x < boardData.width; x++)
        {
            for (int y = 0; y < boardData.height; y++)
            {
                GameObject currentBall = boardData.allBalls[x, y];
                if (currentBall != null)
                {
                    if (x > 0 && x < boardData.width - 1)
                    {
                        GameObject leftBall = boardData.allBalls[x - 1, y];
                        GameObject rightBall = boardData.allBalls[x + 1, y];
                        if (leftBall != null && rightBall != null)
                        {
                            if (leftBall.tag == currentBall.tag && rightBall.tag == currentBall.tag)
                            {
                                if (!specialObject.currentMatches.Contains(leftBall))
                                { specialObject.currentMatches.Add(leftBall); }
                                leftBall.GetComponent<Ball>().isMatched = true;
                                if (!specialObject.currentMatches.Contains(rightBall))
                                { specialObject.currentMatches.Add(rightBall); }
                                rightBall.GetComponent<Ball>().isMatched = true;
                                if (!specialObject.currentMatches.Contains(currentBall))
                                { specialObject.currentMatches.Add(currentBall); }
                                currentBall.GetComponent<Ball>().isMatched = true;

                                specialObject.currentMatches.Union(IsMassBomb(leftBall.GetComponent<Ball>(), currentBall.GetComponent<Ball>(), rightBall.GetComponent<Ball>()));


                            }
                        }
                    }
                    if (y > 0 && y < boardData.height - 1)
                    {
                        GameObject downBall = boardData.allBalls[x, y - 1];
                        GameObject upBall = boardData.allBalls[x, y + 1];
                        if (upBall != null && downBall != null)
                        {
                            if (downBall.tag == currentBall.tag && upBall.tag == currentBall.tag)
                            {
                                if (!specialObject.currentMatches.Contains(downBall))
                                { specialObject.currentMatches.Add(downBall); }
                                downBall.GetComponent<Ball>().isMatched = true;
                                if (!specialObject.currentMatches.Contains(upBall))
                                { specialObject.currentMatches.Add(upBall); }
                                upBall.GetComponent<Ball>().isMatched = true;
                                if (!specialObject.currentMatches.Contains(currentBall))
                                { specialObject.currentMatches.Add(currentBall); }
                                currentBall.GetComponent<Ball>().isMatched = true;


                                specialObject.currentMatches.Union(IsMassBomb(downBall.GetComponent<Ball>(), currentBall.GetComponent<Ball>(), upBall.GetComponent<Ball>()));


                            }
                        }
                    }
                }
            }
        }
    }

    public void FindMatches()
    {
        StartCoroutine(FindAllMatches());
    }

    public void MatchBallsOfColor(string color)
    {
        for (int x = 0; x < boardData.width; x++)
        {
            for (int y = 0; y < boardData.height; y++)
            {
                if (boardData.allBalls[x, y] != null)
                {
                    if (boardData.allBalls[x, y].tag == color)
                    {
                        boardData.allBalls[x, y].GetComponent<Ball>().isMatched = true;
                        boardData.selectedBall.isMatched = false;

                    }
                }
            }
        }
    }

    public void CheckForBomb()
    {
        if (boardData.selectedBall != null)
        {
            if (boardData.selectedBall.isMatched)
            {
                boardData.selectedBall.isMatched = false;
                boardData.selectedBall.isColorBomb = true;
                boardData.selectedBall.CreateColorBomb();
            }
        }
    }


    List<GameObject> GetNearbyBallMatches(int column, int row)
    {
        List<GameObject> balls = new List<GameObject>();
        for (int x = column - 1; x <= column + 1; x++)
        {
            for (int y = row - 1; y <= row + 1; y++)
            {
                if (x >= 0 && x < boardData.width && y >= 0 && y < boardData.height)
                {
                    balls.Add(boardData.allBalls[x, y]);
                    boardData.allBalls[x, y].GetComponent<Ball>().isMatched = true;
                }
            }
        }
        return balls;
    }
    private List<GameObject> IsMassBomb(Ball ball1, Ball ball2, Ball ball3)
    {
        List<GameObject> currentBalls = new List<GameObject>();
        if (ball1.isMassBomb)
        {
            specialObject.currentMatches.Union(GetNearbyBallMatches(ball1.column, ball1.row));
        }
        if (ball2.isMassBomb)
        {
            specialObject.currentMatches.Union(GetNearbyBallMatches(ball2.column, ball2.row));
        }
        if (ball3.isMassBomb)
        {
            specialObject.currentMatches.Union(GetNearbyBallMatches(ball3.column, ball3.row));
        }
        return currentBalls;
    }
}

