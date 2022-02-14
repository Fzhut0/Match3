using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{ }
/*
public List<GameObject> tilesPositions = new List<GameObject>();
[SerializeField] Material mouseOverColor;
[SerializeField] Material originalColor;
[SerializeField] BoardData data;
[SerializeField] Transform selectedTile;

public bool isSelecting = false;



private void Update()
{

    /*
            if (GetComponentInParent<BackgroundGrid>().isMovable)
            {
                GetComponent<SpriteRenderer>().material = mouseOverColor;
            }
            else
            {
                GetComponent<SpriteRenderer>().material = originalColor;
            }

    BallMover();
}
private void OnMouseDown()
{
    if (isSelecting)
    {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D clickedTile = Physics2D.Raycast(new Vector2(clickPosition.x, clickPosition.y), Vector2.zero, 0);

        foreach (GameObject tile in tilesPositions)
        {
            tile.GetComponentInChildren<MoveBall>().selectedTile = transform.parent;
            if (tile.GetComponentInChildren<MoveBall>().isSelecting && tile.GetComponentInParent<BackgroundGrid>().isMovable)
            {
                clickedTile.transform.position = selectedTile.transform.position;
                isSelecting = false;

            }
        }
    }
    else if (!isSelecting)
    {
        MarkNearTiles();
        selectedTile = gameObject.transform.parent;
    }
}




void BallMover()
{

    /* foreach (GameObject tile in tilesPositions)
     {
         if (clickedTile != gameObject.transform.parent && tile.GetComponent<BackgroundGrid>().isMovable)
         {
             selectedTile2 = tile.transform.parent;
         }
         else if (clickedTile == gameObject && tile.GetComponent<BackgroundGrid>().isMovable)
         {
             selectedTile2 = tile.transform.parent;
         }
     }


}


void MarkNearTiles()
{
    Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    RaycastHit2D clickedTile = Physics2D.Raycast(new Vector2(clickPosition.x, clickPosition.y), Vector2.zero, 0);
    tilesPositions = GetComponentInParent<BackgroundGrid>().nearTiles;


    foreach (GameObject tile in data.tilesList)
    {
        tile.GetComponent<BackgroundGrid>().isMovable = false;
        if (clickedTile)
        {
            foreach (GameObject neartile in tilesPositions)
            {
                Debug.Log(transform.parent.position);
                neartile.GetComponentInChildren<MoveBall>().isSelecting = true;
                neartile.GetComponent<BackgroundGrid>().isMovable = true;
            }
        }
    }
}

*/

