using UnityEngine;

[CreateAssetMenu(fileName ="Spawner.asset", menuName = "Spawners/Spawner")]
public class SpawnerDataSO : ScriptableObject
{
    public GameObject itemToSpawn;
    public int minSpawn;
    public int maxSpawn;
}
