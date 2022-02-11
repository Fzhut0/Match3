using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGrid : MonoBehaviour
{
    [SerializeField] BoardData data;
    [SerializeField] List<Vector2> nearTiles = new List<Vector2>();


    private void Awake()
    {

        SpawnBalls();

    }

    private void Start()
    {
        CheckNearTiles();
    }
    void SpawnBalls()
    {
        int ballToUse = Random.Range(0, data.ballPrefabs.Length);
        GameObject ball = Instantiate(data.ballPrefabs[ballToUse], transform.position, Quaternion.identity);
        ball.transform.parent = gameObject.transform;
        data.tilesList.Add(transform.position);
    }

    void CheckNearTiles()
    {
        foreach (Vector2 pos in data.tilesList)
        {
            if (Vector2.Distance(transform.position, pos) == 1)
            {
                nearTiles.Add(pos);
            }
        }

    }

}



