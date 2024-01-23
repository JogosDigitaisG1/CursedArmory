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
    private RoomPercentageSpawnerScript roomPercentageSpawnerScript;


    RoomInfo currentRoomData;

    public RoomScript currentRoom;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<RoomScript> loadedRooms { get; } = new List<RoomScript>();

    bool isLoadingRoom = false;
    bool spawnedBossRoom = false;
    bool updatedRooms = false;
    public bool gameStarted = false;

    public Canvas canvas;
    public GameObject gameOver;
    public GameObject win;
    public PlayerControllerScript playerControl;
    public CharacterStatsScript playerStats;

    public Canvas ui;


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

       // roomPercentageSpawnerScript = GetComponent<RoomPercentageSpawnerScript>();

        

    }


    private void Update()
    {
        GameManager.Instance.bossSpawned = spawnedBossRoom;
        ui.gameObject.SetActive(spawnedBossRoom && GameManager.Instance.startedDungeon);


        UpdateRoomQueue();

        //foreach (RoomScript room in loadedRooms)
        //{
        //    if (room.activeRoom)
        //    {
        //        currentRoom = room;
        //    }
        //}

        UpdateRooms();

        if (spawnedBossRoom && !gameStarted)
        {
            foreach (RoomScript room in loadedRooms)
            {
                gameStarted = true;
                room.StartGameMain();
            }

            playerControl.canMove = true;
        }

        if (currentRoom != null && currentRoom.name.Contains("End") && !(currentRoom.numOfEnemies > 0))
        {
            GameManager.Instance.defeatedBoss = true;
            canvas.gameObject.SetActive(true);
            playerControl.canMove = false;
            win.SetActive(true);
            Debug.Log("Boxx dead");
        }

        if (!playerStats.IsAlive())
        {
            gameOver.SetActive(true);
        }
    }

    private void UpdateRooms()
    {
        foreach (RoomScript room in loadedRooms)
        {

            EnemyScript[] enemies = room.GetComponentsInChildren<EnemyScript>();
            room.enemies = enemies;
            room.numOfEnemies = enemies.Length;

            if (enemies != null)
            {
                foreach (EnemyScript enemy in enemies)
                {
                    enemy.activeRoom = room.activeRoom;
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

            tempRoom.isBossRoom = true;
            LoadRoom("End", tempRoom.X, tempRoom.Y);
        }
    }

    //public void LoadRoom(string name, int x, int y)
    //{
    //    if(DoesRoomExist(x, y))
    //    {
    //        return;
    //    }

    //    RoomInfo newRoomData = new RoomInfo();

    //    newRoomData.name = name;
    //    newRoomData.x = x;  
    //    newRoomData.y = y;

    //    loadRoomQueue.Enqueue(newRoomData);
    //}

    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(x, y))
        {
            return;
        }

        string activeSceneName = SceneManager.GetActiveScene().name;
        string roomName = currentWorldName + name;

        if (activeSceneName != roomName)
        {
            RoomInfo newRoomData = new RoomInfo();
            newRoomData.name = name;
            newRoomData.x = x;
            newRoomData.y = y;

            loadRoomQueue.Enqueue(newRoomData);
        }
    }



    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        // Unload the previous scene
        if (SceneManager.GetSceneByName(roomName).isLoaded)
            yield return SceneManager.UnloadSceneAsync(roomName);

        // Load the new scene
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (!loadRoom.isDone)
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
        ;

        return GetComponent<RoomPercentageSpawnerScript>().GetRandomScene();
    }
}
