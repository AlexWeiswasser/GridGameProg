using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
	private int currentPoints = 0;
	private bool _animating = false;

	[Header("Text")]
    [SerializeField] private TextMeshProUGUI _pointsText;

	[Header("Tweens")]
	private Tween _moveTween;

    private void Start()
    {
        // set player position to middle tile
    }

    void Update()
    {
		if (currentPoints != points)
		{
            _pointsText.text = "Points: " + points;
            currentPoints = points;
		}

		// Moving the player while checking if the space they wish to move to exists. 
        if(Input.GetKeyDown("w"))// Prevent input if tween be happeneing)
        {
            if (_moveTween != null && _moveTween.IsActive())
                return;

            if (!_playerStarted) _playerStarted = true;
			if (_gridManager.GetTile(_gridPos.x, _gridPos.y + 1) == null) return;
            _gridPos.y++;

            GameObject tile = _gridManager.GetTile(_gridPos.x, _gridPos.y);

            _moveTween = transform.DOMove(tile.transform.position, .1f).SetEase(Ease.Linear);
        }
        if (Input.GetKeyDown("a"))// Prevent input if tween be happeneing)
        {
            if (_moveTween != null && _moveTween.IsActive())
                return;

            if (!_playerStarted) _playerStarted = true;
			if (_gridManager.GetTile(_gridPos.x - 1, _gridPos.y) == null) return;
			_gridPos.x--;

            GameObject tile = _gridManager.GetTile(_gridPos.x, _gridPos.y);

            _moveTween = transform.DOMove(tile.transform.position, .1f).SetEase(Ease.Linear);
        }
        if (Input.GetKeyDown("s"))// Prevent input if tween be happeneing)
        {
            if (_moveTween != null && _moveTween.IsActive())
                return;

            if (!_playerStarted) _playerStarted = true;
			if (_gridManager.GetTile(_gridPos.x, _gridPos.y - 1) == null) return;
			_gridPos.y--;

            GameObject tile = _gridManager.GetTile(_gridPos.x, _gridPos.y);

            _moveTween = transform.DOMove(tile.transform.position, .1f).SetEase(Ease.Linear);
        }
        if (Input.GetKeyDown("d")) // Prevent input if tween be happeneing)
		{
            if (_moveTween != null && _moveTween.IsActive())
                return;

            if (!_playerStarted) _playerStarted = true;
			if (_gridManager.GetTile(_gridPos.x + 1, _gridPos.y) == null) return;
			_gridPos.x++;

            GameObject tile = _gridManager.GetTile(_gridPos.x, _gridPos.y);

            _moveTween = transform.DOMove(tile.transform.position, .1f).SetEase(Ease.Linear);
        }

		// Current tile. 
		GameObject tile2 = _gridManager.GetTile(_gridPos.x, _gridPos.y);

        // Starting the game.
        if (_playerStarted && !_gameStarted)
		{
			StartCoroutine(_attackManager.StartGame());
			_gameStarted = true;
		}

		// Checking collision.
		if (tile2.GetComponent<SpriteRenderer>().color == Color.red)
			_dead = true;

		// When player dies
		if (_dead && !_animating)
		{ 
			_animating = true;
			
			//Destroy(gameObject, 1f);
        }
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Points"))
		{
			GreenOrbScript thisOrbScript = collision.gameObject.GetComponent<GreenOrbScript>();
			thisOrbScript.OnPlayerCollection();
            points += 3;
        }
		else 
			_dead = true;
	}
}
