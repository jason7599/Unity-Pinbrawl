using UnityEngine;
using System.Collections.Generic;

public class Tiles : MonoBehaviour
{
    [SerializeField] private TileConfiguration _tileConfig;

    public int rowCount { get; private set; }
    public int columnCount { get; private set; }
    private int _entitySpawnRow;

    private Tile[,] _tiles;
    public Tile this[int x, int y]
    {
        get
        { 
            if (x < 0 || x >= rowCount || y < 0 || y >= columnCount)
                return null;

            return _tiles[x, y];
        }
    }

    private void Awake()
    {
        GenerateTiles();
    }

    public void GenerateTiles()
    {
        Transform tileHolder = transform.Find("TileHolder");

        if (tileHolder != null)
        {  
            // print("Detected previous tileholder. Destroying and making new");
            DestroyImmediate(tileHolder.gameObject);
        } 

        tileHolder = new GameObject("TileHolder").transform;
        tileHolder.parent = transform;

        rowCount = _tileConfig.rowCount;
        columnCount = _tileConfig.columnCount;

        _entitySpawnRow = _tileConfig.entitySpawningRow;

        Vector3 startingPos = _tileConfig.startingPoint;
        float offset = _tileConfig.tileOffset;
        Tile tilePrefab = _tileConfig.tilePrefab;
        Material materialA = _tileConfig.materialA;
        Material materialB = _tileConfig.materialB;

        _tiles = new Tile[rowCount, columnCount];

        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < columnCount; col++)
            {
                Vector3 position = startingPos + (Vector3.right * col + Vector3.forward * row) * offset;

                Tile tile = Object.Instantiate<Tile>(tilePrefab, position, Quaternion.identity, tileHolder);

                tile.Configure(row, col, (row + col) % 2 == 0 ? materialA : materialB);
                
                _tiles[row, col] = tile;
            }
        }
    }

    public Tile TileBelow(Tile tile)
    {
        int row = tile.rowIndex;
        if (row == 0) return null;
        return _tiles[row - 1, tile.columnIndex];
    }

    public Tile GetRandomUnoccupiedTile()
    {
        int iterationLimit = 50;
        int iteration = 0;
        Tile randomTile;
        
        do
        {
            if (iteration++ >= iterationLimit)
                return null;

            int x = Random.Range(0, rowCount);
            int y = Random.Range(0, columnCount);
            randomTile = _tiles[x, y];

        } while (randomTile.isOccupied);

        return randomTile;
    }

    public List<Tile> GetUnoccupiedTilesInSpawnRow()
    {
        List<Tile> spawnableTiles = new List<Tile>();
        for (int col = 0; col < columnCount; col++)
        {
            Tile tile = _tiles[_entitySpawnRow, col];
            if (!tile.isOccupied)
                spawnableTiles.Add(tile);
        }
        return spawnableTiles;
    }

    // public List<Tile> GetTiles(int maxCount, bool unoccupiedOnly = true, bool onlyOnSpawnableRow = true)
    // {
    //     List<Tile> tiles = new List<Tile>();

    //     if (onlyOnSpawnableRow)
    //     {
    //         for (int col = 0; col < columnCount; col++)
    //         {
    //             Tile tile = _tiles[_entitySpawnRow, col];
    //             if (!unoccupiedOnly || !tile.isOccupied)
    //             {
    //                 tiles.Add(tile);
    //             }
    //         }
    //     }
    //     else // probably not the best way of doing this
    //     {
    //         for (int row = 0; row < rowCount; row++)
    //         {
    //             for (int col = 0; col < columnCount; col++)
    //             {
    //                 Tile tile = _tiles[row, col];
    //                 if (!unoccupiedOnly || !tile.isOccupied)
    //                 {
    //                     tiles.Add(tile);
    //                 }
    //             }
    //         }
            
    //     }

    //     return Utils.Shuffle(tiles).GetRange(0, Mathf.Min(maxCount, tiles.Count));
    // }
}

[System.Serializable]
public class TileConfiguration
{
    [Space(10f)]

    public Vector3 startingPoint;
    public float tileOffset;

    [Space(10f)]

    public int rowCount;
    public int columnCount;

    [Space(10f)]

    public int entitySpawningRow;

    [Space(10f)]

    public Tile tilePrefab;
    public Material materialA;
    public Material materialB;
}
