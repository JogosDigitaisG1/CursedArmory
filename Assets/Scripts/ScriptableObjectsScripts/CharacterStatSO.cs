using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseCharacterStats", menuName = "Character Stats/Base")]
public class CharacterStatSO : ScriptableObject
{

    public int health;
    public int attackPower;
    public int attackSpeed;
    public int defense;
    public int moveSpeed;

}
