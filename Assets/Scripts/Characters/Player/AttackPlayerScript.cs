using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerControllerScript;

public class AttackScript : MonoBehaviour
{
    public Collider2D attackColliderX;
    public Collider2D attackColliderY;
    private Vector2 XAttackOffset;
    private Vector2 YAttackOffset;

    private bool isAttacking = false;

    public LayerMask attackColliderLayer;

    [SerializeField]
    private CharacterStatsScript characterStatsScript;

    // Start is called before the first frame update
    void Start()
    {
        characterStatsScript= GetComponentInParent<CharacterStatsScript>();
        XAttackOffset = attackColliderX.transform.localPosition;
        YAttackOffset = attackColliderY.transform.localPosition;
        attackColliderX.enabled = false;
        attackColliderY.enabled = false;

       
    }

    public void Attack(LookDirection lookDirection)
    {
        //print(lookDirection.ToString());
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


        attackColliderX.enabled = false;
        attackColliderY.enabled = false;
    }

    public bool IsAttacking() {  return isAttacking; }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag == TagsCons.enemyTag)
        {
            Debug.Log("trigger hit enemy playerattakc scriot");
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == TagsCons.enemyTag)
        {
            Debug.Log("collision hit enemy oncollision player scri");
        }
    }   

    public void OnChildTriggerEnter2D(Collider2D collision)
    {
        if ((attackColliderX.IsTouching(collision) || attackColliderY.IsTouching(collision)) && collision.gameObject.tag == TagsCons.enemyTag)
        {
            int damage = characterStatsScript.GetAttackPower();
            collision.gameObject.GetComponentInParent<HealthScript>().TakeDamage(damage);
            Debug.Log("collider hit enemy on childe trigger");
        }
    }

    //private bool IsAttackCollider(Collider2D collider)
    //{
    //    // Check if the collider's layer matches the attack collider layer
    //    return (attackColliderLayer.value & (1 << collider.gameObject.layer)) != 0;
    //}
}
