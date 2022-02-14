using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Ball : MonoBehaviour
{
    public BallData ballData;
    public BoardData boardData;

    private MatchFinder findMatches;

    public int column;
    public int row;

    public int previousColumn;
    public int previousRow;

    public int targetX;
    public int targetY;

    private BoardManager board;
    public GameObject otherBall;

    private Vector2 firstTouchPos;
    private Vector2 finalTouchPos;
    private Vector2 tempPos;

    public bool isMatched = false;
    public float swipeAngle = 0;
    public float swipeResist = 1f;

    public Sprite colorBombMaterial;
    public Sprite originalMaterial;
    public Sprite massBombMaterial;


    public bool isColorBomb;
    public bool isMassBomb;


    private void Start()
    {

        GetComponent<SpriteRenderer>().sprite = originalMaterial;
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
        SwitchBallPosition();
        BombColorCheck();

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
            MoveBalls();
            board.currentState = GameState.wait;
            boardData.selectedBall = this;
        }
        else
        {
            board.currentState = GameState.move;
        }
    }

    void MoveBalls()
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

    IEnumerator CheckMove()
    {
        if (isColorBomb)
        {
            findMatches.MatchBallsOfColor(otherBall.tag);
            isMatched = true;
        }
        else if (otherBall.GetComponent<Ball>().isColorBomb)
        {
            findMatches.MatchBallsOfColor(gameObject.tag);
            otherBall.GetComponent<Ball>().isMatched = true;
        }

        yield return new WaitForSeconds(.5f);
        if (otherBall != null)
        {
            if (!isMatched && !otherBall.GetComponent<Ball>().isMatched && !isColorBomb)
            {
                otherBall.GetComponent<Ball>().column = column;
                otherBall.GetComponent<Ball>().row = row;
                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds(.5f);
                boardData.selectedBall = null;
                board.currentState = GameState.move;
            }
            else
            {
                board.DestroyMatches();

            }
        }
        // otherBall = null;
    }

    void SwitchBallPosition()
    {
        if (isMatched)
        {
            transform.DOScale(.05f, 1f);
        }
        targetX = column;
        targetY = row;

        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {

            tempPos = new Vector2(targetX, transform.position.y);
            transform.DOMove(tempPos, .4f);
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
            transform.DOMove(tempPos, .4f);
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




    public void CreateColorBomb()
    {
        isColorBomb = true;
    }

    public void CreateMassBomb()
    {
        isMassBomb = true;
    }

    void BombColorCheck()
    {
        if (isColorBomb)
        {
            transform.DOScale(.7f, .5f);
            GetComponent<SpriteRenderer>().sprite = colorBombMaterial;
        }
        else if (isMassBomb)
        {
            transform.DOScale(.7f, .5f);
            GetComponent<SpriteRenderer>().sprite = massBombMaterial;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = originalMaterial;
        }

    }

}
