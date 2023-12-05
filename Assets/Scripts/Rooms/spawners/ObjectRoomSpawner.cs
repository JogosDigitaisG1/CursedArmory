using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{

    [System.Serializable]
    public struct RandomSpawner
    {
        public string name;
        public SpawnerDataSO spawnerData;
    }

    public GridControllerScript grid;
    public RandomSpawner[] spawnerData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SpawnObjects(RandomSpawner data)
    {
        int randomIter = Random.Range(data.spawnerData.minSpawn, data.spawnerData.maxSpawn + 1);

        for (int i = 0; i < randomIter; i++)
        {
            int randomPos = Random.Range(0, grid.availablePoints.Count - 1);
            GameObject go = Instantiate(data.spawnerData.itemToSpawn, grid.availablePoints[randomPos], Quaternion.identity, transform)
;           grid.availablePoints.RemoveAt(randomPos);
            Debug.Log("spawned object");




        }
    }

    public void InitializeObjectSpawning()
    {
        foreach (RandomSpawner rs in spawnerData)
        {
            SpawnObjects(rs);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
