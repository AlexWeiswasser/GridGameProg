using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject _tilePrefab;
    private List<GameObject> _tiles = new List<GameObject>();

    [Header("Grid Data")]
    [SerializeField] private int numRows = 8;
    [SerializeField] private int numColums = 8;
    [SerializeField] private Vector2 tileSize = Vector2.one;
    [SerializeField] private Vector2 padding = new Vector2(.1f, .1f);

    void Start()
    {
        // Determining how large the grid storing array should be. 
        _tiles.Capacity = numRows * numColums;

        // Making the grid, and adding every new tile to the list of tiles. 
        for (int row = 0; row < numRows; row++)
        {
            for(int col = 0; col < numColums; col++)
            {
                Vector2 tilePos = new Vector2(col * (tileSize.x + padding.x), row * (tileSize.x + padding.x));
                GameObject tile = Instantiate(_tilePrefab, tilePos, Quaternion.identity, transform);
                _tiles.Add(tile);
            }
        }

        // Centering the grid. 
        transform.position = new Vector2(transform.position.x - 4, transform.position.y - 4);
    }

    // A function that returns the tile at the inputed grid coordinate.
    public GameObject GetTile(int column, int row)
    {
        if (row >= numRows || row < 0 || column >= numColums || column < 0) return null;
        return _tiles[(row * numColums) + column];
    }
}
