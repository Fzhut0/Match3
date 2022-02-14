using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGrid : MonoBehaviour
{






}





/*
public BoardData data;

public List<GameObject> ballPrefabs = new List<GameObject>();
public GameObject ballToSpawn;

public GameObject newBallToSpawn;

public Material lastUsedColor;

public List<GameObject> nearTiles = new List<GameObject>();

public GameObject sameColorBall;

public bool isMovable = false;
public bool isSelecting = false;
public bool hasMoved = false;
public bool isEmpty = true;





private void Update()
{
    NameChange();
    BallSpawnedCheck();


}

IEnumerator SpawnBalls()
{
    ballPrefabs.Remove(data.lastSpawnedBall);

    int ballToUse = Random.Range(0, ballPrefabs.Count);
    ballToSpawn = ballPrefabs[ballToUse];
    lastUsedColor = data.lastSpawnedBall.GetComponent<SpriteRenderer>().sharedMaterial;
    RaycastHit2D[] checkBallLeft = Physics2D.RaycastAll(transform.position, Vector2.left, 1f);

    while (isEmpty && checkBallLeft.Length == 0)
    {

        GameObject ball = Instantiate(ballToSpawn, transform.position, Quaternion.identity);
        ball.transform.parent = gameObject.transform;
        data.lastSpawnedBall = ballToSpawn;
        data.tilesList.Add(gameObject);
        lastUsedColor = ballToSpawn.GetComponent<SpriteRenderer>().sharedMaterial;
        isEmpty = false;

    }

    while (isEmpty && checkBallLeft.Length >= 1)
    {

        foreach (RaycastHit2D hit in checkBallLeft)
        {
            if (hit.distance >= .1f && hit.transform.gameObject.GetComponentInChildren<SpriteRenderer>().sharedMaterial == ballToSpawn.GetComponent<SpriteRenderer>().sharedMaterial)
            {
                sameColorBall = hit.transform.gameObject.GetComponentInChildren<SpriteRenderer>().gameObject;
                ballPrefabs.Remove(ballToSpawn);

            }

            int newBallToUse = Random.Range(0, ballPrefabs.Count);
            newBallToSpawn = ballPrefabs[newBallToUse];


            GameObject ball = Instantiate(newBallToSpawn, transform.position, Quaternion.identity);
            ball.transform.parent = gameObject.transform;
            data.lastSpawnedBall = newBallToSpawn;
            data.tilesList.Add(gameObject);
            lastUsedColor = newBallToSpawn.GetComponent<SpriteRenderer>().sharedMaterial;
            isEmpty = false;














            /*else
             *  && hit.collider.GetComponentInChildren<SpriteRenderer>().sharedMaterial == ballToSpawn.GetComponent<SpriteRenderer>().sharedMaterial
            {checkBallLeft.collider.GetComponentInChildren<SpriteRenderer>().sharedMaterial == newBallToSpawn.GetComponent<SpriteRenderer>().sharedMaterial
                if (checkBallLeft.collider.GetComponentInChildren<SpriteRenderer>().sharedMaterial != newBallToSpawn.GetComponent<SpriteRenderer>().sharedMaterial)
                {
                    int newBallToUse = Random.Range(0, ballPrefabs.Count);
                    newBallToSpawn = ballPrefabs[newBallToUse];

                                sameColorBall = hit.transform.gameObject.GetComponentInChildren<SpriteRenderer>().gameObject;
                    ballPrefabs.Remove(sameColorBall);



                    GameObject ball = Instantiate(newBallToSpawn, transform.position, Quaternion.identity);
                    isEmpty = false;
                    ball.transform.parent = gameObject.transform;
                    data.lastSpawnedBall = newBallToSpawn;
                    data.tilesList.Add(gameObject);
                    lastUsedColor = newBallToSpawn.GetComponent<SpriteRenderer>().sharedMaterial;
                }
            }






            /* else if (checkBallLeft)
             {
                 int ballToUse = Random.Range(0, ballPrefabs.Length);
                 ballToSpawn = ballPrefabs[ballToUse];
                 lastUsedColor = data.lastSpawnedBall.GetComponent<SpriteRenderer>().sharedMaterial;

                 if (checkBallLeft.collider.GetComponentInChildren<SpriteRenderer>().sharedMaterial != ballToSpawn.GetComponent<SpriteRenderer>().sharedMaterial)
                 {
                     if (lastUsedColor != ballToSpawn.GetComponent<SpriteRenderer>().sharedMaterial)
                     {
                         GameObject ball = Instantiate(ballToSpawn, transform.position, Quaternion.identity);
                         ball.transform.parent = gameObject.transform;
                         data.lastSpawnedBall = ballToSpawn;
                         data.tilesList.Add(gameObject);
                         lastUsedColor = ballToSpawn.GetComponent<SpriteRenderer>().sharedMaterial;
                         isEmpty = false;
                     }
                 }
             }


             // if () { yield return new WaitForSeconds(Mathf.NegativeInfinity); }

             /*
             else if ()
             {
                 GameObject ball = Instantiate(ballToSpawn, transform.position, Quaternion.identity);
                 ball.transform.parent = gameObject.transform;
                 data.lastSpawnedBall = ballToSpawn;
                 data.tilesList.Add(gameObject);
                 isEmpty = false;
             }
             // data.tilesList.Remove(gameObject);



        }
        CheckNearTiles();
        yield return new WaitForEndOfFrame();
    }
}

public void CheckNearTiles()
{
    foreach (GameObject pos in data.tilesList)
    {
        if (Vector2.Distance(transform.position, pos.transform.position) <= 1)
        {
            nearTiles.Add(pos);
        }
    }
}

private void OnMouseDown()
{
    MoveBall();
}

void MarkNearTiles()
{
    Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    RaycastHit2D clickedTile = Physics2D.Raycast(new Vector2(clickPosition.x, clickPosition.y), Vector2.zero, 0);


    foreach (GameObject tile in data.tilesList)
    {
        tile.GetComponent<BackgroundGrid>().isMovable = false;
        tile.GetComponent<BackgroundGrid>().isSelecting = false;
        tile.GetComponent<BackgroundGrid>().data.selectedTile = null;
        if (clickedTile)
        {
            isSelecting = true;
            data.selectedTilePos = clickedTile.transform.position;
            foreach (GameObject neartile in nearTiles)
            {
                Debug.Log(transform.position);
                neartile.GetComponent<BackgroundGrid>().isMovable = true;

            }
        }
    }
}

void MoveBall()
{
    Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    RaycastHit2D clickedTile = Physics2D.Raycast(new Vector2(clickPosition.x, clickPosition.y), Vector2.zero, 0);
    if (!isSelecting && !isMovable)
    {
        MarkNearTiles();
        data.selectedTile = gameObject;
    }
    if (isMovable && clickedTile.transform.position != data.selectedTilePos)
    {
        data.selectedTile.transform.position = clickedTile.transform.position;
        clickedTile.transform.position = data.selectedTilePos;
        foreach (GameObject tile in data.tilesList)
        {
            tile.GetComponent<BackgroundGrid>().isMovable = false;
            tile.GetComponent<BackgroundGrid>().isSelecting = false;
            tile.GetComponent<BackgroundGrid>().nearTiles.Clear();
            tile.GetComponent<BackgroundGrid>().CheckNearTiles();
        }
    }
}


void NameChange()
{
    if (name != "(" + transform.position.x + "," + transform.position.y + ")")
    {
        name = "(" + transform.position.x + "," + transform.position.y + ")";
    }


}

void BallSpawnedCheck()
{

    if (isEmpty)
    {
        StartCoroutine(SpawnBalls());
    }
    if (!GetComponentInChildren<Ball>())
    {
        isEmpty = true;
    }

}


public void CheckBallToLeft()
{
    RaycastHit2D checkBallLeft = Physics2D.Raycast(transform.position, Vector2.left, 1);
    if (checkBallLeft.collider.GetComponentInChildren<SpriteRenderer>().sharedMaterial == GetComponentInChildren<SpriteRenderer>().sharedMaterial)
    {
        Debug.Log("wrong color" + checkBallLeft.collider.transform.position);
    }

}
*/


