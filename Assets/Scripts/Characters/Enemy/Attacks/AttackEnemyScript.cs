using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MovementEnemyScript;

public class AttackEnemyScript : MonoBehaviour
{

    private bool isAttacking = false;

    public LayerMask attackColliderLayer;



    [SerializeField]
    private IEnemyAttack enemyTypeAttack;

    // Start is called before the first frame update
    void Start()
    {

        
        enemyTypeAttack = GetComponent<IEnemyAttack>();
    }

    public void Attack(EnemyLookDirection lookDirection, Vector3 playerPos)
    {
        isAttacking = true;
        enemyTypeAttack.PerformAttack(lookDirection, playerPos);
    }


    public void StopAttack()
    {
        isAttacking = false;
        enemyTypeAttack.StopAttack();


    }

    public bool IsAttacking() { return isAttacking; }


    public void OnChildTriggerEnter2D(Collider2D collision)
    {
        enemyTypeAttack.CheckTrigger(collision);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
