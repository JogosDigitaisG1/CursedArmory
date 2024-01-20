using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomPercentageSpawnerScript : MonoBehaviour
{

    public SpawnerScript<string> stringSpawner;

    private void Awake()
    {
        stringSpawner.Initialize();
        Debug.Log("Start RoomPercentageSpawnerScript");
    }


    public string GetRandomScene()
    {
        
        return stringSpawner.Spawn();
    }
}
