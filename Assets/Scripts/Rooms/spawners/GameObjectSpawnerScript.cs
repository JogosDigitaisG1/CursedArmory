using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSpawnerScript : MonoBehaviour
{
    public SpawnerScript<GameObject> gameObjectSpawner;

    private void Start()
    {
        gameObjectSpawner.Initialize();

        GameObject spawnedObject = Instantiate(gameObjectSpawner.Spawn(), transform.position, Quaternion.identity, transform);
       
    }
}
