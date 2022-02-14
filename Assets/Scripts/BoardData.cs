using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    wait,
    move
}
[CreateAssetMenu(fileName = "BoardData", menuName = "BoardManagerData")]
public class BoardData : ScriptableObject
{
    public GameState currentState = GameState.move;

    public int width;
    public int height;
    public int offset;

    public GameObject backgroundPrefab;
    public GameObject[] balls;
    public BackgroundGrid[,] allTiles;
    public GameObject[,] allBalls;
    public Ball selectedBall;


    public enum GameState
    {
        wait,
        move
    }




}

