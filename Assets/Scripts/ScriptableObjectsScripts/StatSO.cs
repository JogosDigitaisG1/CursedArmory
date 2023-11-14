using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Character Stats/Stat")]
public class StatSO : ScriptableObject
{
    public string statName;
    public float baseValue;
    

    public float GetValue()
    {
        
        return baseValue;
    }

    public string getStatName()
    {
        return statName;
    }
}
