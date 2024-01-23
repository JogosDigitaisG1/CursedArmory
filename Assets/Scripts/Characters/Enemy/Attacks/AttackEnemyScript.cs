using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MovementEnemyScript;

public class AttackEnemyScript : MonoBehaviour
{

    [SerializeField]
    private bool isAttacking = false;

    public LayerMask attackColliderLayer;


    public int attacks = 0;
    public int specialAttacks1 = 0;
    public int specialAttacks2 = 0;


    [SerializeField]
    private IEnemyAttack enemyTypeAttack;

    // Start is called before the first frame update
    void Start()
    {

        
        enemyTypeAttack = GetComponent<IEnemyAttack>();
    }

    public void Attack(EnemyLookDirection lookDirection, Vector3 playerPos)
    {
        enemyTypeAttack.PerformAttack(lookDirection, playerPos);
    }

    public void StartAttackAnim()
    {
        isAttacking = true;
    }


    public void StopAttack()
    {
        attacks++;
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
