using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerControllerScript;

public class AttackScript : MonoBehaviour
{
    private BoxCollider2D attackCollider;
    Vector2 rightAttackOffset;

    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        attackCollider = GetComponent<BoxCollider2D>();
        rightAttackOffset = transform.localPosition;
        attackCollider.enabled = false;

        print(rightAttackOffset);
    }

    public void Attack(LookDirection lookDirection)
    {
        switch(lookDirection)
        {
            case LookDirection.Left:
                AttackLeft(); break;

                case LookDirection.Right: 
                AttackRight(); break;

        }
    }


    private void AttackRight()
    {
        attackCollider.enabled = true;
        isAttacking = true;
        transform.localPosition = rightAttackOffset;
    }

    private void AttackLeft()
    {
        attackCollider.enabled = true;
        isAttacking = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack()
    {
        isAttacking = false;
        attackCollider.enabled = false;
    }

    public bool IsAttacking() {  return isAttacking; }
}
