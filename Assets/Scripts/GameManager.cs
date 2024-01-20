using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public bool newGame = true;
    [SerializeField]
    private PlayerStats maxPlayerData;
    [SerializeField]
    private PlayerStats playerInGameMaxData;
    [SerializeField]
    private PlayerStats currentGameData;

    [SerializeField]
    private bool activeDungeon = false;

    [SerializeField]
    private bool startedDungeon = false;

    [SerializeField]
    private string activeDungeonName = "";

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;


        }

        
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        //Debug.Log("Scene " + scene.name);
        if (IsDungeonScene(scene))
        {
            Debug.Log("Scene is main" + scene.name);
            if (activeDungeon && startedDungeon)
            {
                //Debug.Log("Delete dungeon");
                //Debug.Log("Scene " + scene.name);
                //Debug.Log("activeDungeonName " + activeDungeonName);
                //Debug.Log("activeDungeon " + activeDungeon);
                DeleteDungeon();
            }

                StartDungeon(scene.name);


            
        }

        if (activeDungeon && !scene.name.Replace("Main", "").Contains(activeDungeonName.Replace("Main", "")))
        {
            Debug.Log("scene.name que no es " + scene.name);
            activeDungeon = false;
            activeDungeonName = "";
        }
 
    }

    private bool IsDungeonScene(Scene scene)
    {
        return scene.name == ScenesCons.BASEMENTMAIN; 
    }


    public bool isNewGame()
    {
        return newGame;
    }

    public bool isActiveDungeon()
    {
        return activeDungeon;
    }

    public void InitializeNewPlayerData(PlayerStats data)
    {
        // Initialize new player data for a new game
        currentGameData = data;
        // Set default values or load from a default scriptable object
    }

    private void StartDungeon(string scene)
    {
        Debug.Log("Start dungeon");

        startedDungeon = true;
        activeDungeonName = scene;
        activeDungeon = true;
        BaseDungeonGenerator currentDungeonGenerator = FindObjectOfType<BaseDungeonGenerator>();
        if (currentDungeonGenerator != null)
        {
            currentDungeonGenerator.InitializeDungeon();
        }
    }

    private void DeleteDungeon()
    {
        startedDungeon = false;
        BaseDungeonGenerator currentDungeonGenerator = FindObjectOfType<BaseDungeonGenerator>();
        if (currentDungeonGenerator != null)
        {
            currentDungeonGenerator.DeleteDungeon();
        }
    }

    public void LoadPlayerData()
    {
        // Load existing player data
        // This can be from a file, PlayerPrefs, database, etc.
    }

    public PlayerStats GetCurrentPlayerData()
    {
        return currentGameData;
    }
    public PlayerStats GetMaxPlayerData()
    {
        return maxPlayerData;
    }
    public PlayerStats GetIngameMaxPlayerData()
    {
        return playerInGameMaxData;
    }

    public void SavePlayerData(PlayerStats maxData, PlayerStats inGameMaxData, PlayerStats currentData)
    {
        currentGameData = currentData;
        playerInGameMaxData = inGameMaxData;
        maxPlayerData = maxData;
    }


}
