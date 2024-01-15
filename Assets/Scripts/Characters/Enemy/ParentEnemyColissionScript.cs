using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentEnemyColissionScript : MonoBehaviour
{
    public AttackEnemyScript attackEnemyScript;

    private void Start()
    {
        attackEnemyScript.GetComponent<AttackEnemyScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackEnemyScript.OnChildTriggerEnter2D(collision);
    }
}
