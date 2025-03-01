using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [Header("Round Controls")]
	[SerializeField] public float attackDelay = 1.5f;
	[SerializeField] private int attacksInRound = 1;
	private bool roundInSession = false;
    private int attackType;

	[Header("Managers")]
	[SerializeField] private GridManager _gridManager;

	// Starts the next round upon player discrection, if one isn't already going on.  
	public IEnumerator StartRound()
	{
		if (roundInSession) yield break;
		roundInSession = true;

		Debug.Log("Round start!");

		int attackNumber = 0;

		while (attackNumber < attacksInRound)
		{
			attackNumber++;

			attackType = Random.Range(0, 3);

			switch (attackType)
			{
				case 0:
					LineAttack();
					break;
				case 1:
					BombAttack();
					break;
				case 2:
					int numberOfPoints = Random.Range(30, 40);
					for(int i = 0; i < numberOfPoints; i++)
						PointAttack();
					break;
			}

			yield return new WaitForSeconds(attackDelay);
		}

		Debug.Log("Round end!");
		attackDelay -= .075f;
		attackDelay = Mathf.Clamp(attackDelay, .4f, Mathf.Infinity);
		roundInSession = false;
		attacksInRound++;
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
	void BombAttack()
	{
		int tileX = Random.Range(2, 7);
		int tileY = Random.Range(2, 7);

		int[] offsets = { -2, -1, 0, 1, 2 };

		foreach (int offsetX in offsets)
		{
			foreach (int offsetY in offsets)
			{
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
}
