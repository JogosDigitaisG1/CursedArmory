using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerControllerScript;

public class AttackScript : MonoBehaviour
{

    private bool isAttacking = false;

    //public LayerMask attackColliderLayer;

    [SerializeField]
    private CharacterStatsScript characterStatsScript;

    public List<AttackTypeEnumPair> attackTypeList = new List<AttackTypeEnumPair>();

    private IPlayerAttack currentAttack;

    public bool slash = false;

    // Start is called before the first frame update
    void Start()
    {
        characterStatsScript= GetComponentInParent<CharacterStatsScript>();

        //int index = list.FindIndex(f => f.Bar == 17);

        int index = attackTypeList.FindIndex(f => f.attackType == PlayerAttackType.Basic);
        currentAttack = attackTypeList[index].attackTypeGameObject.GetComponent<IPlayerAttack>();  



    }

    private void Update()
    {
        if(slash)
        {
            ChangeAttackType(PlayerAttackType.Slash);
        }
        else
        {
            ChangeAttackType(PlayerAttackType.Basic);
        }
    }

    public void ChangeAttackType(PlayerAttackType changeAttackType)
    {
        switch (changeAttackType)
        {
            case PlayerAttackType.Basic:
                currentAttack = attackTypeList[attackTypeList.FindIndex(f => f.attackType == PlayerAttackType.Basic)].
                    attackTypeGameObject.GetComponent<IPlayerAttack>();
                break;
            case PlayerAttackType.Slash:
                currentAttack = attackTypeList[attackTypeList.FindIndex(f => f.attackType == PlayerAttackType.Slash)].
                    attackTypeGameObject.GetComponent<IPlayerAttack>();
                break;
                default: break;
        }
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
        isAttacking = true;
        currentAttack.PerformAttackRight();
    }

    private void AttackLeft()
    {

        currentAttack.PerformAttackLeft();
        isAttacking = true;
    }

    private void AttackUp()
    {
        currentAttack.PerformAttackUp();
        isAttacking = true;
    }

    private void AttackDown()
    {
        currentAttack.PerformAttackDown();
        isAttacking = true;
    }

    public void StopAttack()
    {
        currentAttack.StopAttack();
        isAttacking = false;

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
        currentAttack.OnChildTriggerEnter2D(collision, characterStatsScript.GetAttackPower());
    }

    //private bool IsAttackCollider(Collider2D collider)
    //{
    //    // Check if the collider's layer matches the attack collider layer
    //    return (attackColliderLayer.value & (1 << collider.gameObject.layer)) != 0;
    //}
}
