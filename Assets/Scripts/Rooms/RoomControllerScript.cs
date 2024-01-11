using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class  RoomInfo
{
    public string name;
    public int x;
    public int y;
}

public class RoomControllerScript : MonoBehaviour
{

    public static RoomControllerScript instance;
    string currentWorldName = "Basement";

    RoomInfo currentRoomData;

    public RoomScript currentRoom;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<RoomScript> loadedRooms { get; } = new List<RoomScript>();

    bool isLoadingRoom = false;
    bool spawnedBossRoom = false;
    bool updatedRooms = false;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //LoadRoom("Start", 0, 0);
        //LoadRoom("Start", 1, 0);
        //LoadRoom("Start", -1, 0);
        //LoadRoom("Start", 0, 1);
        //LoadRoom("Start", 0, -1);


        
    }


    private void Update()
    {
        UpdateRoomQueue();

        //foreach (RoomScript room in loadedRooms)
        //{
        //    if (room.activeRoom)
        //    {
        //        currentRoom = room;
        //    }
        //}

        UpdateRooms();
    }

    private void UpdateRooms()
    {
        foreach (RoomScript room in loadedRooms)
        {

            EnemyScript[] enemies = room.GetComponentsInChildren<EnemyScript>();
            room.enemies = enemies;
            room.numOfEnemies = enemies.Length;
            if (currentRoom != room)
            {               
                if (enemies != null)
                {
                    foreach (EnemyScript enemy in enemies)
                    {
                        enemy.notInRoom = true;
                    }
                }
            }
            else
            {
                if (enemies != null)
                {
                    foreach (EnemyScript enemy in enemies)
                    {
                        enemy.notInRoom = false;
                    }
                }
            }
        }
    }

    private void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }

        if (loadRoomQueue.Count == 0)
        {
            if (!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }else if (spawnedBossRoom && !updatedRooms)
            {
                foreach (RoomScript room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
                updatedRooms = true;
            }
            return;
        }

        currentRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentRoomData));
    }

    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {
            RoomScript bossRoom = loadedRooms[loadedRooms.Count - 1 ];
            RoomScript tempRoom = new RoomScript(bossRoom.X, bossRoom.Y);

            Destroy(bossRoom.gameObject);

            var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            loadedRooms.Remove(roomToRemove);

            LoadRoom("End", tempRoom.X, tempRoom.Y);
        }
    }

    public void LoadRoom(string name, int x, int y)
    {
        if(DoesRoomExist(x, y))
        {
            return;
        }

        RoomInfo newRoomData = new RoomInfo();

        newRoomData.name = name;
        newRoomData.x = x;  
        newRoomData.y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName,
            LoadSceneMode.Additive);

        while(loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(RoomScript room)
    {
        if (!DoesRoomExist(currentRoomData.x, currentRoomData.y))
        {
            room.transform.position = new Vector3(currentRoomData.x * room.width,
    currentRoomData.y * room.height, 0);

            room.X = currentRoomData.x;
            room.Y = currentRoomData.y;
            room.name = currentWorldName + "-" + currentRoomData.name + " " +
                room.X + ", " + room.Y;
            room.transform.parent = transform;

            //print(room.name);

            isLoadingRoom = false;

            loadedRooms.Add(room);

        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }

    }
    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }

    public RoomScript FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }

    public string GetRandomRoomName()
    {
        string[] possibleRooms = new string[]
        {
            "Empty",
            "Basic1"
        };

        return possibleRooms[UnityEngine.Random.Range(0, possibleRooms.Length)];
    }
}
