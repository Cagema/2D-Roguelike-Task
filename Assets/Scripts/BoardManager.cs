using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int Columns = 8;
    public int Rows = 8;
    public Count WallCount = new Count(5, 9);
    public Count FoodCount = new Count(1, 5);
    public GameObject Exit;
    public GameObject[] FloorTiles;
    public GameObject[] WallTiles;
    public GameObject[] FoodTiles;
    public GameObject[] EnemyTiles;
    public GameObject[] OuterWallTiles;

    Transform _boardHolder;
    List<Vector3> _gridPositions = new();

    void InitialiseList()
    {
        _gridPositions.Clear();
        for (int x = 1; x < Columns - 1; x++)
        {
            for (int y = 1; y < Rows - 1; y++)
            {
                _gridPositions.Add(new Vector3(x, y, 0));
            }
        }
    }

    void BoardSetup()
    {
        _boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < Columns + 1; x++)
        {
            for (int y = -1; y < Rows + 1; y++)
            {
                GameObject toInstantiane = FloorTiles[Random.Range(0, FloorTiles.Length)];
                if (x == -1 || x == Columns || y == -1 || y == Rows)
                    toInstantiane = OuterWallTiles[Random.Range(0, OuterWallTiles.Length)];

                Instantiate(toInstantiane, new Vector3(x, y, 0), Quaternion.identity, _boardHolder);
            }
        }
    }

    Vector3 RandomPosition()
    {
        int ranomdIndex = Random.Range(0, _gridPositions.Count);
        Vector3 randomPosition = _gridPositions[ranomdIndex];
        _gridPositions.RemoveAt(ranomdIndex);
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupScene(int level)
    {
        BoardSetup();
        InitialiseList();
        LayoutObjectAtRandom(WallTiles, WallCount.minimum, WallCount.maximum);
        LayoutObjectAtRandom(FoodTiles, FoodCount.minimum, FoodCount.maximum);
        int enemyCount = (int)Mathf.Log(level, 2);
        LayoutObjectAtRandom(EnemyTiles, enemyCount, enemyCount);
        Instantiate(Exit, new Vector3(Columns - 1, Rows - 1, 0), Quaternion.identity);
    }
}
