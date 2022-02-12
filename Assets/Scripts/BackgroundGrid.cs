using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGrid : MonoBehaviour
{
    [SerializeField] BoardData data;
    public List<GameObject> nearTiles = new List<GameObject>();
    public bool isMovable = false;

    public bool isSelecting = false;
    public bool hasMoved = false;


    private void Awake()
    {
        SpawnBalls();
    }

    private void Start()
    {
        CheckNearTiles();
    }

    private void Update()
    {



    }

    void SpawnBalls()
    {
        int ballToUse = Random.Range(0, data.ballPrefabs.Length);
        GameObject ball = Instantiate(data.ballPrefabs[ballToUse], transform.position, Quaternion.identity);
        ball.transform.parent = gameObject.transform;
        data.tilesList.Add(gameObject);
    }

    void CheckNearTiles()
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

    void MovePlace()
    {

        if (isSelecting)
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D clickedTile = Physics2D.Raycast(new Vector2(clickPosition.x, clickPosition.y), Vector2.zero, 0);

            foreach (GameObject tile in nearTiles)
            {
                if (clickedTile.transform != gameObject.transform)
                {
                    Debug.Log(tile.transform.position);
                    clickedTile.transform.position = gameObject.transform.position;
                    gameObject.transform.position = clickedTile.transform.position;
                }
            }

        }
    }

}



