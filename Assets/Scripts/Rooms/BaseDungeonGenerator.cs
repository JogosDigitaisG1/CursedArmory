using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDungeonGenerator : MonoBehaviour
{
    public abstract void InitializeDungeon();

    public abstract void DeleteDungeon();
}
