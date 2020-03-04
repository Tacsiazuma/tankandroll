using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TileMapGenerator : MonoBehaviour
{

    public Tilemap ground;
    public Tile groundTile;
    public Tile concreteTile;
    public Tilemap concretes;

    [SerializeField]
    private GameObject brickPrefab;

    [SerializeField]
    private GameObject concretePrefab;

    [SerializeField]
    private GameObject bushPrefab;

    [SerializeField]
    private GameObject waterPrefab;

    [SerializeField]
    private GameObject basePrefab;

    private int[,] map;

    public int mapWidth;
    public int mapHeight;

    private void Start()
    {
        ClearMap();
        map = new int[mapWidth, mapHeight];
        CreateMap();
        CreateGround();
        CreateBricks();
        CreateWall();
    }

    private void CreateMap()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                int v = Random.Range(1, 100);
                if (v > 70)
                {
                    map[x, y] = 1; // brick
                }
                else if (v > 60 && y < mapHeight - 2 && y > 2)
                {
                    map[x, y] = 2; // solid
                }
                else if (v > 50)
                {
                    map[x, y] = 3; // bush
                }
                else if (v > 40 && y < mapHeight - 2 && y > 2)
                {
                    map[x, y] = 4; // water
                }
                else
                {
                    map[x, y] = 0;
                }
            }
        }
        ClearEdges();
        CreateBase();
    }

    private void CreateBase()
    {
        map[mapWidth / 2 + 1, 0] = 1;
        map[mapWidth / 2 + 1, 1] = 1;
        map[mapWidth / 2 , 0] = 5;
        map[mapWidth / 2 , 1] = 1;
        map[mapWidth / 2 - 1, 1] = 1;
        map[mapWidth / 2 -1 , 0] = 1;
    }

    private void ClearEdges()
    {
        map[0, 0] = 0;
        map[mapWidth - 1, 0] = 0;
        map[0, mapHeight - 1] = 0;
        map[mapWidth - 1, mapHeight - 1] = 0;
    }

    private void CreateWall()
    {
        for (int x = -1; x <= mapWidth; x++)
        {
            for (int y = -1; y <= mapHeight; y++)
            {
                if (y == -1 ||x == -1 || y == mapHeight || x == mapWidth)
                {
                    concretes.SetTile(new Vector3Int(x, y, 0), concreteTile);
                }
            }
        }
    }

    private void CreateBricks()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                switch (map[x, y])
                {
                    case 1:
                        Instantiate(brickPrefab, new Vector3(x, y), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(concretePrefab, new Vector3(x, y), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(bushPrefab, new Vector3(x, y), Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(waterPrefab, new Vector3(x, y), Quaternion.identity);
                        break;
                    case 5:
                        Instantiate(basePrefab, new Vector3(x, y), Quaternion.identity);
                        break;
                }
            }
        }
    }

    private void CreateGround()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                ground.SetTile(new Vector3Int(x, y, 0), groundTile);
            }
        }
    }

    void ClearMap()
    {
        ground.ClearAllTiles();
    }

}
