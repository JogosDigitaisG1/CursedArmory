using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MovementEnemyScript;
using static PlayerControllerScript;

public class AttackEnemyScript : MonoBehaviour
{

    public Collider2D attackColliderX;
    private Vector2 XAttackOffset;

    private bool isAttacking = false;

    public LayerMask attackColliderLayer;

    [SerializeField]
    private CharacterStatsScript characterStatsScript;

    // Start is called before the first frame update
    void Start()
    {
        characterStatsScript = GetComponentInParent<CharacterStatsScript>();
        XAttackOffset = attackColliderX.transform.localPosition;
        attackColliderX.enabled = false;
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
        attackColliderX.enabled = true;
        isAttacking = true;
        attackColliderX.transform.localPosition = XAttackOffset;
    }

    private void AttackLeft()
    {
        attackColliderX.enabled = true;
        isAttacking = true;
        attackColliderX.transform.localPosition = new Vector3(XAttackOffset.x * -1, XAttackOffset.y);
    }

    public void StopAttack()
    {
        isAttacking = false;

        attackColliderX.enabled = false;
    }

    public bool IsAttacking() { return isAttacking; }

    public void OnChildTriggerEnter2D(Collider2D collision)
    {
        if ((attackColliderX.IsTouching(collision) && collision.gameObject.tag == TagsCons.playerTag))
        {
            int damage = characterStatsScript.GetAttackPower();
            collision.gameObject.GetComponentInParent<HealthScript>().TakeDamage(damage);
            print("hit " + collision.gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
