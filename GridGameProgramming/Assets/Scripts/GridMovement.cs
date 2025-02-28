using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [Header("Grid Managers")]
    [SerializeField] private GridManager _gridManager;
	[SerializeField] private AttackManager _attackManager;

	[Header("Tile Choice")]
    [SerializeField] private Vector2Int _gridPos; 

    void Update()
    {
		// Moving the player while checking if the space they wish to move to exists. 
        if(Input.GetKeyDown("w"))
        {
			if (_gridManager.GetTile(_gridPos.x, _gridPos.y + 1) == null) return;
            _gridPos.y++;
        }
		if (Input.GetKeyDown("a"))
		{
			if (_gridManager.GetTile(_gridPos.x - 1, _gridPos.y) == null) return;
			_gridPos.x--;
		}
		if (Input.GetKeyDown("s"))
		{
			if (_gridManager.GetTile(_gridPos.x, _gridPos.y - 1) == null) return;
			_gridPos.y--;
		}
		if (Input.GetKeyDown("d"))
		{
			if (_gridManager.GetTile(_gridPos.x + 1, _gridPos.y) == null) return;
			_gridPos.x++;
		}

		// Current tile. 
		GameObject tile = _gridManager.GetTile(_gridPos.x, _gridPos.y);

        // Moving player to match the current tile.
        transform.position = tile.transform.position;

		// Starting the next round.
		if (Input.GetKeyDown("space"))
		{
			_attackManager.StartRound();
		}
    }
}
