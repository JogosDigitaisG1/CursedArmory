using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasicAttack : MonoBehaviour, IPlayerAttack
{
    public Collider2D basicAttackColliderX;
    public Collider2D basicAttackColliderYTop;
    public Collider2D basicAttackColliderYBot;
    private Vector2 XAttackOffset;
    private Vector2 YAttackOffset;

    private void Start()
    {
        XAttackOffset = basicAttackColliderX.transform.localPosition;
        YAttackOffset = basicAttackColliderYTop.transform.localPosition;
        basicAttackColliderX.enabled = false;
        basicAttackColliderYBot.enabled = false;
        basicAttackColliderYTop.enabled = false;
    }

    public PlayerAttackType GetAttackType()
    {
        return PlayerAttackType.Basic;
    }

    public void PerformAttackDown()
    {
        SoundManager.Instance.PlaySlashSound();
        basicAttackColliderYBot.enabled = true;
        //basicAttackColliderYTop.transform.localPosition = new Vector3(YAttackOffset.x, YAttackOffset.y * -1);
    }

    public void PerformAttackLeft()
    {
        SoundManager.Instance.PlaySlashSound();
        basicAttackColliderX.enabled = true;
        basicAttackColliderX.transform.localPosition = new Vector3(XAttackOffset.x * -1, XAttackOffset.y);
    }

    public void PerformAttackRight()
    {
        SoundManager.Instance.PlaySlashSound();
        basicAttackColliderX.enabled = true;
        basicAttackColliderX.transform.localPosition = XAttackOffset;
    }

    public void PerformAttackUp()
    {
        SoundManager.Instance.PlaySlashSound();
        basicAttackColliderYTop.enabled = true;
        //basicAttackColliderYTop.transform.localPosition = new Vector3(YAttackOffset.x, YAttackOffset.y);
    }

    public void OnChildTriggerEnter2D(Collider2D collision, int damage)
    {
        if ((basicAttackColliderX.IsTouching(collision) || basicAttackColliderYTop.IsTouching(collision)) && collision.gameObject.tag == TagsCons.enemyTag)
        {
            Vector2 knockbackDirection = ((Vector2)transform.position - (Vector2)collision.transform.position).normalized;
            collision.gameObject.GetComponentInParent<HealthScript>().TakeHit(damage, new List<AttackEffectType> { AttackEffectType.Damage,
                AttackEffectType.KnockBack}, knockbackDirection, 5f, .8f);
            Debug.Log("collider hit enemy on childe trigger");
        }
    }

    public void StopAttack()
    {
        basicAttackColliderX.enabled = false;
        basicAttackColliderYTop.enabled = false;
        basicAttackColliderYBot.enabled = false;
    }
}
