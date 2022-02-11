using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGrid : MonoBehaviour
{
    [SerializeField] GameObject[] balls;
    [SerializeField] BoardPosList data;


    private void Start()
    {
        int xPos = Mathf.RoundToInt(transform.position.x);
        int yPos = Mathf.RoundToInt(transform.position.y);

        SpawnBalls();

    }
    void SpawnBalls()
    {
        int ballToUse = Random.Range(0, balls.Length);
        GameObject ball = Instantiate(balls[ballToUse], transform.position, Quaternion.identity);
        ball.transform.parent = gameObject.transform;
        data.tilesList.Add(transform.position);
    }


}



