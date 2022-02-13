using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchFinder : MonoBehaviour
{
    public BoardData boardData;
    public SpecialObjectHandler specialObject;
    public TextMeshProUGUI scoreDisplay;

    public List<GameObject> currentMatches = new List<GameObject>();


    private void Start()
    {
        currentMatches.Clear();
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
                                if (!currentMatches.Contains(leftBall))
                                { currentMatches.Add(leftBall); }
                                leftBall.GetComponent<Ball>().isMatched = true;
                                if (!currentMatches.Contains(rightBall))
                                { currentMatches.Add(rightBall); }
                                rightBall.GetComponent<Ball>().isMatched = true;
                                if (!currentMatches.Contains(currentBall))
                                { currentMatches.Add(currentBall); }
                                currentBall.GetComponent<Ball>().isMatched = true;
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
                                if (!currentMatches.Contains(downBall))
                                { currentMatches.Add(downBall); }
                                downBall.GetComponent<Ball>().isMatched = true;
                                if (!currentMatches.Contains(upBall))
                                { currentMatches.Add(upBall); }
                                upBall.GetComponent<Ball>().isMatched = true;
                                if (!currentMatches.Contains(currentBall))
                                { currentMatches.Add(currentBall); }
                                currentBall.GetComponent<Ball>().isMatched = true;
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
}
