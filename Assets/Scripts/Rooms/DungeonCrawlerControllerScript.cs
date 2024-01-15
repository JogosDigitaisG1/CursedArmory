using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    up = 0, left = 1, down = 2, right = 3,
};
public class DungeonCrawlerControllerScript : MonoBehaviour
{

    public static List<Vector2Int> positionsVisited = new List<Vector2Int>();

    private static readonly Dictionary<Direction, Vector2Int>
        directionMovementMap = new Dictionary<Direction, Vector2Int>
        {
            {Direction.up, Vector2Int.up },
            {Direction.left, Vector2Int.left},
            {Direction.right, Vector2Int.right},
            {Direction.down, Vector2Int.down }
};

    public static List<Vector2Int> GenerateDungeon(DungeonGeneratorDataSO dungeonData)
    {
        List<DungeonCrawlerScript> dungeonCrawlers = new List<DungeonCrawlerScript>();

        for (int i = 0; i < dungeonData.numberOfCrawlers; i++)
        {

            dungeonCrawlers.Add(new DungeonCrawlerScript(Vector2Int.zero));
        }

        int iterations = Random.Range(dungeonData.iterationMin, dungeonData.iterationMax);

        for (int i = 0; i < iterations; i++)
        { 
            foreach (DungeonCrawlerScript dungeonCrawler in dungeonCrawlers)
            {
                Vector2Int newPos = dungeonCrawler.Move(directionMovementMap);
                positionsVisited.Add(newPos);

            }
        }

        return positionsVisited;
    }

        }
