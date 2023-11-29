using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyCharacterStats", menuName = "Character Stats/Enemy")]
public class EnemyStatsSO : CharacterStatSO
{

    //public StatSO type;
    public float idleTimer;
    public float roamTimer;

    public override string ToString()
    {
        return "idle timer = " + idleTimer + " roamTime: " + roamTimer;
    }

}
