using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    BallData data;


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

        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;

        row = targetY;
        column = targetX;

        previousColumn = column;
        previousRow = row;
    }

    private void Update()
    {
        FindMatches();
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
            if (board.allBalls[column, row] != gameObject)
            {
                board.allBalls[column, row] = gameObject;
            }
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
            if (board.allBalls[column, row] != gameObject)
            {
                board.allBalls[column, row] = gameObject;
            }
        }
        else
        {
            tempPos = new Vector2(transform.position.x, targetY);
            transform.position = tempPos;
        }


    }

    private void OnMouseDown()
    {
        firstTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        finalTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    void CalculateAngle()
    {
        if (Mathf.Abs(finalTouchPos.y - firstTouchPos.y) > swipeResist || Mathf.Abs(finalTouchPos.x - firstTouchPos.x) > swipeResist)
        {
            swipeAngle = Mathf.Atan2(finalTouchPos.y - firstTouchPos.y, finalTouchPos.x - firstTouchPos.x) * 180 / Mathf.PI;
            MovePieces();
        }
    }

    void MovePieces()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1)
        {
            // right
            otherBall = board.allBalls[column + 1, row];
            otherBall.GetComponent<Ball>().column -= 1;
            column += 1;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)
        {
            // up
            otherBall = board.allBalls[column, row + 1];
            otherBall.GetComponent<Ball>().row -= 1;
            row += 1;
        }
        else if (swipeAngle > 135 && swipeAngle <= -135 && column > 0)
        {
            // left
            otherBall = board.allBalls[column - 1, row];
            otherBall.GetComponent<Ball>().column += 1;
            column -= 1;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            //down
            otherBall = board.allBalls[column, row - 1];
            otherBall.GetComponent<Ball>().row += 1;
            row -= 1;
        }
        StartCoroutine(CheckMove());
    }

    void FindMatches()
    {
        if (column > 0 && column < board.width - 1)
        {
            GameObject leftDot1 = board.allBalls[column - 1, row];
            GameObject rightDot1 = board.allBalls[column + 1, row];
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
        if (row > 0 && row < board.height - 1)
        {
            GameObject upDot1 = board.allBalls[column, row + 1];
            GameObject downDot1 = board.allBalls[column, row - 1];
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
            }
            else
            {
                board.CheckDestroyMatches();
            }
        }
        otherBall = null;
    }

}
