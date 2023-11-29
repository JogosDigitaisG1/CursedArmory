using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    private Vector2 screenBounds;

    [SerializeField]
    private float spawnRate = 1f;

    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private int numberToSpawn;

    [SerializeField]
    private bool canSpawn;

    [SerializeField]
    private List<Vector2> spawnPositions;

    private void Start()
    {
        spawnPositions = new List<Vector2>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Debug.Log(screenBounds);
        // StartCoroutine(Spawner());

        for (int i = 0; i < numberToSpawn; i++)
        {
            Vector2 randomVector = new Vector2(Random.Range(-1 * screenBounds.x + 1, screenBounds.x - 1), Random.Range(-1, screenBounds.y - 1));
            spawnPositions.Add(randomVector);
        }

        SpawnEnemies();
    }

    private void SpawnEnemies()
    {

        for (int i = 0; i < numberToSpawn; i++)
        {
            int rand = Random.Range(0, enemyPrefabs.Length);

            GameObject enemyToSpawn = enemyPrefabs[rand];

            Instantiate(enemyToSpawn, spawnPositions[i], Quaternion.identity);
        }

    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn)
        {
            yield return wait;

            int rand = Random.Range(0, enemyPrefabs.Length);
            
            GameObject enemyToSpawn = enemyPrefabs[rand];

            Instantiate(enemyToSpawn, spawnPositions[0], Quaternion.identity);
        }
    }
}
