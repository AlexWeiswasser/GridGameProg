using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [Header ("Grid Managers")]
    [SerializeField] private GridManager _gridManager;

    [Header("Tile Choice")]
    [SerializeField] private Vector2Int _gridPos; 

    void Update()
    {
        GameObject tile = _gridManager.GetTile(_gridPos.x, _gridPos.y);

        // Moving player to match the current tile.
        transform.position = tile.transform.position;
    }
}
