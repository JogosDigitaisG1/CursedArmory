using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerControllerScript;

public class AttackScript : MonoBehaviour
{
    public BoxCollider2D attackColliderX;
    public BoxCollider2D attackColliderY;
    private Vector2 XAttackOffset;
    private Vector2 YAttackOffset;

    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {

        XAttackOffset = attackColliderX.transform.localPosition;
        YAttackOffset = attackColliderY.transform.localPosition;
        attackColliderX.enabled = false;
        attackColliderY.enabled = false;

       
    }

    public void Attack(LookDirection lookDirection)
    {
        switch(lookDirection)
        {
            case LookDirection.Left:
                AttackLeft(); break;

                case LookDirection.Right: 
                AttackRight(); break;

            case LookDirection.Up:
                AttackUp(); break;

            case LookDirection.Down:
                AttackDown(); break;

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

    private void AttackUp()
    {
        attackColliderY.enabled = true;
        isAttacking = true;
        attackColliderY.transform.localPosition = new Vector3(YAttackOffset.x, YAttackOffset.y);
    }

    private void AttackDown()
    {
        attackColliderY.enabled = true;
        isAttacking = true;
        attackColliderY.transform.localPosition = new Vector3(YAttackOffset.x, YAttackOffset.y * -1);
    }

    public void StopAttack()
    {
        isAttacking = false;

        if(attackColliderX.enabled)
        attackColliderX.enabled = false;

        if (attackColliderY.enabled)
            attackColliderY.enabled = false;
    }

    public bool IsAttacking() {  return isAttacking; }
}
