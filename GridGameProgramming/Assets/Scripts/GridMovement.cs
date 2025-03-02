using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private GridManager _gridManager;
	[SerializeField] private AttackManager _attackManager;

	[Header("Tile Choice")]
    private Vector2Int _gridPos = new Vector2Int(4, 4);

	[Header("Variables")]
	private bool _dead = false;
	private bool _gameStarted = false;
	private bool _playerStarted = false;
	public int points = 0; 

	void Update()
    {
		// Moving the player while checking if the space they wish to move to exists. 
        if(Input.GetKeyDown("w"))
        {
			if(!_playerStarted) _playerStarted = true;
			if (_gridManager.GetTile(_gridPos.x, _gridPos.y + 1) == null) return;
            _gridPos.y++;
        }
		if (Input.GetKeyDown("a"))
		{
			if (!_playerStarted) _playerStarted = true;
			if (_gridManager.GetTile(_gridPos.x - 1, _gridPos.y) == null) return;
			_gridPos.x--;
		}
		if (Input.GetKeyDown("s"))
		{
			if (!_playerStarted) _playerStarted = true;
			if (_gridManager.GetTile(_gridPos.x, _gridPos.y - 1) == null) return;
			_gridPos.y--;
		}
		if (Input.GetKeyDown("d"))
		{
			if (!_playerStarted) _playerStarted = true;
			if (_gridManager.GetTile(_gridPos.x + 1, _gridPos.y) == null) return;
			_gridPos.x++;
		}

		// Current tile. 
		GameObject tile = _gridManager.GetTile(_gridPos.x, _gridPos.y);

		// Moving player to match the current tile.
		transform.position = Vector3.Lerp(transform.position, tile.transform.position, Time.deltaTime * 45);

		// Starting the game.
		if (_playerStarted && !_gameStarted)
		{
			StartCoroutine(_attackManager.StartGame());
			_gameStarted = true;
		}

		// Checking collision.
		if (tile.GetComponent<SpriteRenderer>().color == Color.red)
			_dead = true;

		Debug.Log("Current points are " + points + ". Dead status: " + _dead);
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Points"))
			points += 3;
		else 
			_dead = true;
		Destroy(collision.gameObject);
	}
}
