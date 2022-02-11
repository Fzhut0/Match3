using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Tile Positions")]
public class BoardPosList : ScriptableObject
{
    public List<Vector2> tilesList = new List<Vector2>();

}
