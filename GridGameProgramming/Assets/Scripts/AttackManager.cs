using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [Header("Round Controls")]
	[SerializeField] public float attackDelay = 1.5f;
	[SerializeField] private int attacksInRound = 5;
    private int attackType;

	[Header("Scripts")]
	[SerializeField] private GridManager _gridManager;
	[SerializeField] private GridMovement _gridMovement;

	[Header("Prefabs")]
	[SerializeField] private GameObject _bullet;
	[SerializeField] private GameObject _pointOrb;

	// Starts the game upon player discrection, and continues it, while slowly increasing the speed.  
	public IEnumerator StartGame()
	{
		int attackNumber = 0;

		while (attackNumber < attacksInRound)
		{
			// Timing related things.
			attackNumber++;

			int firstBullet = Random.Range(0, attacksInRound);
			int secondBullet = Random.Range(0, attacksInRound);

			int pointSpawn = Random.Range(0, attacksInRound);

			// Attack Related things.
			attackType = Random.Range(0, 3);

			switch (attackType)
			{
				case 0:
					LineAttack();
					LineAttack();
					break;
				case 1:
					CircleAttack();
					break;
				case 2:
					int numberOfPoints = Random.Range(40, 50);
					for(int i = 0; i < numberOfPoints; i++)
						PointAttack();
					break;
			}

			if (attackNumber == firstBullet || attackNumber == secondBullet)
			{
				GenerateBullets();
				GeneratePointBall();
			}

			yield return new WaitForSeconds(attackDelay);
		}

		attackDelay -= .075f;
		attackDelay = Mathf.Clamp(attackDelay, .4f, Mathf.Infinity);
		_gridMovement.points++;
		
		StartCoroutine(StartGame());
	}

	// Makes a few lines of tiles dangerous. 
	void LineAttack()
    {
		int tempNumb = Random.Range(0, 9);

		for (int i = 0; i < 9; i++)
		{
			StartCoroutine(_gridManager.TileAttack(i, tempNumb));
			StartCoroutine(_gridManager.TileAttack(tempNumb, i));
		}
	}

	// Makes a blurb of tiles dangerous. 
	void CircleAttack()
	{
		int tileX = Random.Range(2, 7);
		int tileY = Random.Range(2, 7);

		int[] offsets = { -2, -1, 0, 1, 2 };

		foreach (int offsetX in offsets)
		{
			foreach (int offsetY in offsets)
			{
				if ((offsetX != 0 || offsetY != 0) && !(Mathf.Abs(offsetX) == 2 && Mathf.Abs(offsetY) == 2))
					StartCoroutine(_gridManager.TileAttack(tileY + offsetY, tileX + offsetX)); 
			}
		}
	}

	// Quickly makes a random grouping of tiles dangerous. 
	void PointAttack()
    {
		int tileX, tileY;
        tileX = Random.Range(0, _gridManager.numRows);
        tileY = Random.Range(0, _gridManager.numColumns);

		StartCoroutine(_gridManager.TileAttack(tileY, tileX));
	}

	// Generates some bullets that are thrown towards the grid randomly.
	public void GenerateBullets()
	{
		int numBullets = Random.Range(2, 4);

		for (int i = 0; i < numBullets; i++)
		{
			int colOrRow = Random.Range(0, 2);
			int whichSide = Random.Range(0, 2);
			float bulletSpeed = Random.Range(4, 9);

			if (colOrRow == 0)
			{
				int rowNumb = Random.Range(0, 9);

				if (whichSide == 0)
				{
					Vector2 spawnPos = new Vector2(_gridManager.GetTile(0, rowNumb).transform.position.x - _gridManager.tileSize.x * 3, _gridManager.GetTile(0, rowNumb).transform.position.y);
					GameObject bullet = Instantiate(_bullet, spawnPos, Quaternion.Euler(0, 0, -90));
					bullet.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;
					Destroy(bullet, 10f);
				}
				else
				{
					Vector2 spawnPos = new Vector2(_gridManager.GetTile(8, rowNumb).transform.position.x + _gridManager.tileSize.x * 3, _gridManager.GetTile(0, rowNumb).transform.position.y);
					GameObject bullet = Instantiate(_bullet, spawnPos, Quaternion.Euler(0, 0, 90));
					bullet.GetComponent<Rigidbody2D>().velocity = transform.right * -bulletSpeed;
					Destroy(bullet, 10f);
				}
			}
			else
			{
				int colNumb = Random.Range(0, 9);

				if (whichSide == 0)
				{
					Vector2 spawnPos = new Vector2(_gridManager.GetTile(colNumb, 0).transform.position.x, _gridManager.GetTile(colNumb, 0).transform.position.y -_gridManager.tileSize.y * 3);
					GameObject bullet = Instantiate(_bullet, spawnPos, Quaternion.identity);
					bullet.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
					Destroy(bullet, 10f);
				}
				else
				{
					Vector2 spawnPos = new Vector2(_gridManager.GetTile(colNumb, 8).transform.position.x, _gridManager.GetTile(colNumb, 8).transform.position.y + _gridManager.tileSize.y * 3);
					GameObject bullet = Instantiate(_bullet, spawnPos, Quaternion.Euler(0, 0, 180));
					bullet.GetComponent<Rigidbody2D>().velocity = transform.up * -bulletSpeed;
					Destroy(bullet, 10f);
				}
			}
		}
	}

	// Makes an object the player can grab to get points. 
	void GeneratePointBall()
	{
		int tileX, tileY;
		tileX = Random.Range(0, _gridManager.numRows);
		tileY = Random.Range(0, _gridManager.numColumns);

		Vector2 pointPos = new Vector2(_gridManager.GetTile(tileY, tileX).transform.position.x, _gridManager.GetTile(tileY, tileX).transform.position.y);

		GameObject PointOrb = Instantiate(_pointOrb, pointPos, Quaternion.identity);
		Destroy(PointOrb, 3f);
	}
}
