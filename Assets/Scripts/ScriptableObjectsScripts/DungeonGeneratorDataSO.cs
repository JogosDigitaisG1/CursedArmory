using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonGenerationData.asset",
    menuName ="dungeonGenerationData/ Dungeon Data")]
public class DungeonGeneratorDataSO : ScriptableObject
{

    public int numberOfCrawlers;
    public int iterationMin;
    public int iterationMax;
}
