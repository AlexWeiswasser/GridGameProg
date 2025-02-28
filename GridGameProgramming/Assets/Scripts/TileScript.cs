using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TileScript : MonoBehaviour
{
	[Header("Tile Data")]
	public float dangerTime;

	[Header("Components")]
	[SerializeField] private SpriteRenderer tileRend;

	private void Update()
	{
		if (dangerTime <= 0)
		{
			tileRend.color = Color.white;
		}
		else
		{
			dangerTime -= Time.deltaTime;
		}

		if(dangerTime < 0)
			dangerTime = 0;
	}
}