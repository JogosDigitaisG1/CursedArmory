using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentEnemyColissionScript : MonoBehaviour
{
    public AttackEnemyScript attackEnemyScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackEnemyScript.OnChildTriggerEnter2D(collision);
    }
}
