using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using DG.Tweening;

public class GridManager : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject _tilePrefab;
    private List<GameObject> _tiles = new List<GameObject>();

    [Header("Grid Data")]
    [SerializeField] public int numRows = 9;
    [SerializeField] public int numColumns = 9;
    [SerializeField] public Vector2 tileSize = Vector2.one;
    [SerializeField] private Vector2 padding = new Vector2(.1f, .1f);
    [SerializeField] private AttackManager _attackManager;

    void Start()
    {
        // Determining how large the grid storing array should be. 
        _tiles.Capacity = numRows * numColumns;

        // Making the grid, and adding every new tile to the list of tiles. 
        for (int row = 0; row < numRows; row++)
        {
            for(int col = 0; col < numColumns; col++)
            {
                Vector2 tilePos = new Vector2(col * (tileSize.x + padding.x), row * (tileSize.x + padding.x));
                GameObject tile = Instantiate(_tilePrefab, tilePos, Quaternion.identity, transform);
                _tiles.Add(tile);
            }
        }

        // Centering the grid. 
        transform.position = new Vector2(transform.position.x - (float)numRows/2, transform.position.y - (float)numColumns /2);
    }

    // A function that returns the tile at the inputed grid coordinate.
    public GameObject GetTile(int column, int row)
    {
        if (row >= numRows || row < 0 || column >= numColumns || column < 0) return null;
        return _tiles[(row * numColumns) + column];
    }

    // Obtains a tile and delay, and makes that tile dangerous after that delay. 
    public IEnumerator TileAttack(int column, int row)
    {
		if (row >= numRows || row < 0 || column >= numColumns || column < 0) yield break;

		GameObject tile = _tiles[(row * numColumns) + column];
        SpriteRenderer tileRend = tile.GetComponent<SpriteRenderer>();

        if (tileRend.color == Color.white)
        {
			tileRend.color = Color.gray;
            
            yield return new WaitForSeconds(_attackManager.attackDelay/2);

            tileRend.color = Color.red;

            yield return tile.transform.DOPunchScale(new Vector3(.15f, .15f, .15f), (_attackManager.attackDelay / 2) - .01f, 1, 1).WaitForCompletion();

            //yield return tileRend.DOColor()

            tileRend.color = Color.white;
		}
	}
}
