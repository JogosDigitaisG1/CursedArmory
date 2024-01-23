    using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGenerator :  BaseDungeonGenerator
{
    public DungeonGeneratorDataSO dungeonGeneratorDataSO;
    [SerializeField]
    private List<Vector2Int> dungeonRooms;

    private void Awake()
    {

    }


    public override void InitializeDungeon()
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

    public override void DeleteDungeon()
    {
        Debug.Log("Before " + dungeonRooms.Count);
        dungeonRooms.Clear();
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        RoomControllerScript.instance.loadedRooms.Clear();

        Debug.Log("After " + dungeonRooms.Count);
    }
}
