using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGeneratorDataSO dungeonGeneratorDataSO;
    private List<Vector2Int> dungeonRooms;

    private void Start()
    {
        dungeonRooms = DungeonCrawlerControllerScript.GenerateDungeon(dungeonGeneratorDataSO);
        SpawnRooms(dungeonRooms);
            
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomControllerScript.instance.LoadRoom("Start", 0, 0);

        foreach (Vector2Int roomLocation in rooms)
        {

                RoomControllerScript.instance.LoadRoom(RoomControllerScript.instance.GetRandomRoomName(),
    roomLocation.x, roomLocation.y);


        }
    }
}
