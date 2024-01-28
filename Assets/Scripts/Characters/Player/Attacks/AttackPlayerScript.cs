using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerControllerScript;

public class AttackScript : MonoBehaviour
{
    private List<IPlayerAttack> activeAttacks = new List<IPlayerAttack>();

    private bool isAttacking = false;

    //public LayerMask attackColliderLayer;

    [SerializeField]
    private CharacterStatsScript characterStatsScript;

    public List<AttackTypeEnumPair> attackTypeList = new List<AttackTypeEnumPair>();

    public bool slash = false;
    public bool shoot = false;
    public bool basic = false;
    public bool special = false;

    // Start is called before the first frame update
    void Start()
    {
        characterStatsScript= GetComponentInParent<CharacterStatsScript>();

        ActivateAttack(PlayerAttackType.Basic); // Activate basic attack by default
    }

    private void Update()
    {

        ToggleAttack(PlayerAttackType.Slash, characterStatsScript.GetNumberOfSwords() >= 5);


        ToggleAttack(PlayerAttackType.Shoot, characterStatsScript.GetNumberOfStaffs() >= 5);

        ToggleAttack(PlayerAttackType.Special, GameManager.Instance.defeatedBoss);



    }



    private void ActivateAttack(PlayerAttackType attackType)
    {
        int index = attackTypeList.FindIndex(f => f.attackType == attackType);
        if (index != -1)
        {
            IPlayerAttack attack = attackTypeList[index].attackTypeGameObject.GetComponent<IPlayerAttack>();
            if (!activeAttacks.Contains(attack))
            {
                activeAttacks.Add(attack);
                attackTypeList[index].attackTypeGameObject.SetActive(true);
            }
        }
    }

    public void ToggleAttack(PlayerAttackType attackType, bool activate)
    {
        int index = attackTypeList.FindIndex(f => f.attackType == attackType);
        if (index != -1)
        {
            IPlayerAttack attack = attackTypeList[index].attackTypeGameObject.GetComponent<IPlayerAttack>();
            if (activate)
            {
                
                if (!activeAttacks.Contains(attack))
                {
                    SoundManager.Instance.PlayPowerupSound();
                    activeAttacks.Add(attack);
                    attackTypeList[index].attackTypeGameObject.SetActive(true);
                }
            }
            else
            {
                if (activeAttacks.Contains(attack))
                {
                    activeAttacks.Remove(attack);
                    attackTypeList[index].attackTypeGameObject.SetActive(false);
                }
            }
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
        
        foreach (var activeAttack in activeAttacks)
        {
            activeAttack.PerformAttackRight();
        }
        isAttacking = true;

    }

    private void AttackLeft()
    {
        foreach (var activeAttack in activeAttacks)
        {
            activeAttack.PerformAttackLeft();
        }
        isAttacking = true;

    }

    private void AttackUp()
    {
        foreach (var activeAttack in activeAttacks)
        {
            activeAttack.PerformAttackUp();
        }
        isAttacking = true;
    }

    private void AttackDown()
    {
        foreach (var activeAttack in activeAttacks)
        {
            activeAttack.PerformAttackDown();
        }
        isAttacking = true;
    }

    public void StopAttack()
    {
        foreach (var activeAttack in activeAttacks)
        {
            activeAttack.StopAttack();
        }
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

        foreach (var activeAttack in activeAttacks)
        {
            activeAttack.OnChildTriggerEnter2D(collision, characterStatsScript.GetAttackPower());
        }
        
    }

    //private bool IsAttackCollider(Collider2D collider)
    //{
    //    // Check if the collider's layer matches the attack collider layer
    //    return (attackColliderLayer.value & (1 << collider.gameObject.layer)) != 0;
    //}
}
