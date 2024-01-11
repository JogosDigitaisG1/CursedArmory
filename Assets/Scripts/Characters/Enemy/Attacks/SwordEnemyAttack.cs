using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MovementEnemyScript;

public class SwordEnemyAttack : MonoBehaviour, IEnemyAttack
{

    public Collider2D attackCollider;
    private Vector2 attackOffset;

    [SerializeField]
    private CharacterStatsScript characterStatsScript;

    private void Start()
    {
        attackOffset = attackCollider.transform.localPosition;
        attackCollider.enabled = false;
        characterStatsScript = GetComponentInParent<CharacterStatsScript>();
    }

    public void PerformAttack(MovementEnemyScript.EnemyLookDirection lookDirection, Vector3 playerPos)
    {
        Attack(lookDirection);
    }

    public void Attack(EnemyLookDirection lookDirection)
    {
        //print(lookDirection.ToString());
        switch (lookDirection)
        {
            case EnemyLookDirection.Left:
                AttackLeft(); break;

            case EnemyLookDirection.Right:
                AttackRight(); break;

        }
    }
    private void AttackRight()
    {


        attackCollider.enabled = true;
        attackCollider.transform.localPosition = attackOffset;
    }

    private void AttackLeft()
    {
        attackCollider.enabled = true;
        attackCollider.transform.localPosition = new Vector3(attackOffset.x * -1, attackOffset.y);
    }

    public void StopAttack()
    {
        attackCollider.enabled = false;
    }

    public void CheckTrigger(Collider2D collision)
    {
        if ((attackCollider.IsTouching(collision) && collision.gameObject.tag == TagsCons.playerTag))
        {
            int damage = characterStatsScript.GetAttackPower();
            collision.gameObject.GetComponentInParent<HealthScript>().TakeDamage(damage);
            print("hit " + collision.gameObject.name);
        }
    }
}
