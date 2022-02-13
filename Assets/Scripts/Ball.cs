using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    BallData data;
    public BoardData boardData;

    private MatchFinder findMatches;
    public int column;
    public int row;

    public int previousColumn;
    public int previousRow;

    public int targetX;
    public int targetY;

    private BoardManager board;
    private GameObject otherBall;

    private Vector2 firstTouchPos;
    private Vector2 finalTouchPos;
    private Vector2 tempPos;

    public bool isMatched = false;
    public float swipeAngle = 0;
    public float swipeResist = 1f;

    private void Awake()
    {

    }

    private void Start()
    {
        board = FindObjectOfType<BoardManager>();
        findMatches = FindObjectOfType<MatchFinder>();
        /*
                targetX = (int)transform.position.x;
                targetY = (int)transform.position.y;

                row = targetY;
                column = targetX;

                previousColumn = column;
                previousRow = row;
        */
    }

    private void Update()
    {
        // FindMatches();
        if (isMatched)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        targetX = column;
        targetY = row;

        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {
            tempPos = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPos, .6f);
            if (boardData.allBalls[column, row] != gameObject)
            {
                boardData.allBalls[column, row] = gameObject;
            }
            findMatches.FindMatches();
        }
        else
        {
            tempPos = new Vector2(targetX, transform.position.y);
            transform.position = tempPos;
        }
        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            tempPos = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPos, .6f);
            if (boardData.allBalls[column, row] != gameObject)
            {
                boardData.allBalls[column, row] = gameObject;
            }
            findMatches.FindMatches();
        }
        else
        {
            tempPos = new Vector2(transform.position.x, targetY);
            transform.position = tempPos;
        }


    }

    private void OnMouseDown()
    {
        if (board.currentState == GameState.move)
        {
            firstTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseUp()
    {
        if (board.currentState == GameState.move)
        {
            finalTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }
    }

    void CalculateAngle()
    {
        if (Mathf.Abs(finalTouchPos.y - firstTouchPos.y) > swipeResist || Mathf.Abs(finalTouchPos.x - firstTouchPos.x) > swipeResist)
        {
            swipeAngle = Mathf.Atan2(finalTouchPos.y - firstTouchPos.y, finalTouchPos.x - firstTouchPos.x) * 180 / Mathf.PI;
            MovePieces();
            board.currentState = GameState.wait;
        }
        else
        {
            board.currentState = GameState.move;
        }
    }

    void MovePieces()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < boardData.width - 1)
        {
            // right
            otherBall = boardData.allBalls[column + 1, row];
            previousColumn = column;
            previousRow = row;
            otherBall.GetComponent<Ball>().column -= 1;
            column += 1;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < boardData.height - 1)
        {
            // up
            otherBall = boardData.allBalls[column, row + 1];
            previousColumn = column;
            previousRow = row;
            otherBall.GetComponent<Ball>().row -= 1;
            row += 1;
        }
        else if (swipeAngle > 135 || swipeAngle <= -135 && column > 0)
        {
            // left
            otherBall = boardData.allBalls[column - 1, row];
            previousColumn = column;
            previousRow = row;
            otherBall.GetComponent<Ball>().column += 1;
            column -= 1;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            //down
            otherBall = boardData.allBalls[column, row - 1];
            previousColumn = column;
            previousRow = row;
            otherBall.GetComponent<Ball>().row += 1;
            row -= 1;
        }
        StartCoroutine(CheckMove());
    }

    void FindMatches()
    {
        if (column > 0 && column < boardData.width - 1)
        {
            GameObject leftDot1 = boardData.allBalls[column - 1, row];
            GameObject rightDot1 = boardData.allBalls[column + 1, row];
            if (leftDot1 != null && rightDot1 != null)
            {
                if (leftDot1.tag == gameObject.tag && rightDot1.tag == gameObject.tag)
                {
                    leftDot1.GetComponent<Ball>().isMatched = true;
                    rightDot1.GetComponent<Ball>().isMatched = true;
                    isMatched = true;
                }
            }
        }
        if (row > 0 && row < boardData.height - 1)
        {
            GameObject upDot1 = boardData.allBalls[column, row + 1];
            GameObject downDot1 = boardData.allBalls[column, row - 1];
            if (upDot1 != null && downDot1 != null)
            {
                if (upDot1.tag == gameObject.tag && downDot1.tag == gameObject.tag)
                {
                    upDot1.GetComponent<Ball>().isMatched = true;
                    downDot1.GetComponent<Ball>().isMatched = true;
                    isMatched = true;
                }
            }
        }
    }
    IEnumerator CheckMove()
    {
        yield return new WaitForSeconds(.5f);
        if (otherBall != null)
        {
            if (!isMatched && !otherBall.GetComponent<Ball>().isMatched)
            {
                otherBall.GetComponent<Ball>().column = column;
                otherBall.GetComponent<Ball>().row = row;
                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds(.5f);
                board.currentState = GameState.move;
            }
            else
            {
                board.DestroyMatches();

            }
        }
        otherBall = null;
    }

}
