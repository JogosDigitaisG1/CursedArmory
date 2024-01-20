using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnerScript<T>
{
    [System.Serializable]
    public struct Spawnable
    {
        public T item;
        public float weight;
    }

    public List<Spawnable> spawnables = new List<Spawnable>();

    private float totalWeight;

    public void Initialize()
    {
        totalWeight = 0f;
        foreach (var spawnable in spawnables)
        {
            totalWeight += spawnable.weight;
        }
    }

    public T Spawn()
    {
        float pick = UnityEngine.Random.value * totalWeight;
        int chosenIndex = 0;
        float cumulativeWeight = spawnables[0].weight;

        while (pick > cumulativeWeight && chosenIndex < spawnables.Count - 1)       
        {
            chosenIndex++;
            cumulativeWeight += spawnables[chosenIndex].weight;
        }

        return spawnables[chosenIndex].item;
    }
}
