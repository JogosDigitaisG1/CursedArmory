using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseCharacterStats", menuName = "Character Stats/Base")]
public class CharacterStatSO : ScriptableObject
{

    public StatSO maxHealth;
    public StatSO currentHealth;
    public StatSO attackPower;
    public StatSO defense;
    public StatSO speed;

}
