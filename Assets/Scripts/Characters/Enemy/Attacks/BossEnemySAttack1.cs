using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MovementEnemyScript;

public class BossEnemySAttack1 : MonoBehaviour, IEnemyAttack
{

    public Collider2D attackCollider;
    public Collider2D mainCollider;
    private Vector2 attackOffset;
    public Transform parentTransform;
    public float speed = 5f;

    public bool isAttacking = false;
    public bool canAttack = true;
    public bool charging = false;

    [SerializeField]
    private CharacterStatsScript characterStatsScript;

    [SerializeField]
    private float timeCharge;
    public float startTimeCharge;

    private Transform playerPosition;

    [SerializeField]
    private Vector3 lastPlayerPosition;

    public float raycastDistance = 0.1f; 
    public LayerMask wallLayer;


    private void Start()
    {
        timeCharge = startTimeCharge;
        //attackOffset = attackCollider.transform.localPosition;
        attackCollider.enabled = false;
        characterStatsScript = GetComponentInParent<CharacterStatsScript>();
        playerPosition = FindObjectOfType<PlayerControllerScript>().transform;
    }

    public void SetAttack(Vector3 playerPos)
    {

        if (canAttack && !isAttacking)
        {

            canAttack = false;
            isAttacking = true;
            
        }

    }

    private void Update()
    {
        if (isAttacking)
        {
            Attack();
        }
    }

    public void Attack()
    {

        if (!charging)
        {
            if (timeCharge <= 0)
            {
                charging = true;
                attackCollider.enabled = true;
                mainCollider.enabled = false;
                if (playerPosition != null)
                {
                    lastPlayerPosition = playerPosition.transform.position;
                }
                
                timeCharge = startTimeCharge;
            }
            else
            {
                
                timeCharge -= Time.deltaTime;
            }
        }
        else
        {

            PerformAttack(EnemyLookDirection.Right, lastPlayerPosition);
        }


        
    }


    public void PerformAttack(EnemyLookDirection lookDirection, Vector3 playerPos)
    {
        //  Attack(lookDirection);

        Vector2 direction = (playerPos - parentTransform.position).normalized;
        parentTransform.Translate(direction * speed * Time.deltaTime);

        float distanceToPlayer = (parentTransform.position - playerPos).magnitude;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, wallLayer);
        Debug.Log("hit " + hit);
        

        Vector2 rayStart = transform.position;

        Vector2 rayEnd = rayStart + direction * raycastDistance;

        Debug.DrawLine(rayStart, rayEnd, Color.green);

        if (distanceToPlayer < 0.05f || hit.collider != null)
        {
            
            StopAttack();
        }
    }

    //public void Attack(EnemyLookDirection lookDirection)
    //{
    //    //print(lookDirection.ToString());
    //    switch (lookDirection)
    //    {
    //        case EnemyLookDirection.Left:
    //            AttackLeft(); break;

    //        case EnemyLookDirection.Right:
    //            AttackRight(); break;

    //    }
    //}
    //private void AttackRight()
    //{


    //    attackCollider.enabled = true;
    //    attackCollider.transform.localPosition = attackOffset;
    //}

    //private void AttackLeft()
    //{
    //    attackCollider.enabled = true;
    //    attackCollider.transform.localPosition = new Vector3(attackOffset.x * -1, attackOffset.y);
    //}

    public void StopAttack()
    {
        Debug.Log("stop attack ");
        attackCollider.enabled = false;
        mainCollider.enabled = true;
        isAttacking = false;
        charging = false;

    }

    public void CheckTrigger(Collider2D collision)
    {
        if ((attackCollider.IsTouching(collision) && collision.gameObject.tag == TagsCons.playerTag))
        {
            int damage = characterStatsScript.GetAttackPower();

            Vector2 knockbackDirection = ((Vector2)transform.position - (Vector2)collision.transform.position).normalized;
            collision.gameObject.GetComponentInParent<HealthScript>().TakeHit(damage, new List<AttackEffectType> { AttackEffectType.Damage, 
                AttackEffectType.Invincibility, AttackEffectType.KnockBack}, knockbackDirection, 5f, .8f);
            print("hit " + collision.gameObject.name);
        }
    }

    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(lastPlayerPosition, 2);
        Gizmos.color = Color.blue;

        if (playerPosition)
        {
            Gizmos.DrawWireSphere(playerPosition.transform.position, 2);
        }
        

    }
}
