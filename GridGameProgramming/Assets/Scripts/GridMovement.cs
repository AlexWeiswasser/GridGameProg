using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private GridManager _gridManager;
	[SerializeField] private AttackManager _attackManager;

	[Header("Tile Choice")]
    private Vector2Int _gridPos = new Vector2Int(4, 4);

	[Header("Variables")]
	private bool dead = false; 

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
		transform.position = Vector3.Lerp(transform.position, tile.transform.position, Time.deltaTime * 45);

		// Starting the next round.
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StartCoroutine(_attackManager.StartRound());
		}

		// Checking collision.
		if (tile.GetComponent<SpriteRenderer>().color == Color.red)
			dead = true;
    }
}
