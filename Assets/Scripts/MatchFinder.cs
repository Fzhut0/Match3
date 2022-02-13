using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchFinder : MonoBehaviour
{
    public BoardData boardData;

    private BoardManager board;
    public List<GameObject> currentMatches = new List<GameObject>();

    private void Start()
    {
        board = FindObjectOfType<BoardManager>();
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
}
