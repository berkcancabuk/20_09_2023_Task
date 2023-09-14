using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{

    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cam;
    [SerializeField] private GameObject _parentObj;
    private void Start()
    {
        GenerateGrid();
    }
    void GenerateGrid()
    {
        for (var x = 1; x < _width; x++)
        {
            for (var y = 1; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity, _parentObj.transform);
                spawnedTile.name = "Tile-" + x +","+ y;
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
            }
        }
        _cam.transform.position = new Vector3((float)_width / 2, (float)_height / 2 , -10);
    }
}
