using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttack : MonoBehaviour, IPlayerAttack
{
    public Collider2D slashAttackColliderX;
    public Collider2D slashAttackColliderY;
    private Vector2 XAttackOffset;
    private Vector2 YAttackOffset;

    private SpriteRenderer spriteRendererX;
    private SpriteRenderer spriteRendererY;

    private Animator animatorX;
    private Animator animatorY;


    private void Start()
    {
        spriteRendererX = slashAttackColliderX.gameObject.GetComponent<SpriteRenderer>();
        spriteRendererY = slashAttackColliderY.gameObject.GetComponent<SpriteRenderer>();
        animatorX = slashAttackColliderX.gameObject.GetComponent<Animator>();
        animatorY = slashAttackColliderY.gameObject.GetComponent<Animator>();
        XAttackOffset = slashAttackColliderX.transform.localPosition;
        YAttackOffset = slashAttackColliderY.transform.localPosition;
        slashAttackColliderX.enabled = false;
        slashAttackColliderY.enabled = false;
        spriteRendererX.enabled = false;
        spriteRendererY.enabled = false;

        animatorX.enabled = true;
        animatorY.enabled = true;
        animatorX.Play(PlayerCons.noSlash);
        animatorY.Play(PlayerCons.noSlash);
    }

    //animator.SetTrigger(PlayerCons.slashAnim);

    public PlayerAttackType GetAttackType()
    {
        return PlayerAttackType.Slash;
    }

    public void PerformAttackLeft()
    {
        
        slashAttackColliderX.enabled = true;
        spriteRendererX.enabled = true;
        //animatorX.enabled = true;
        animatorX.Play(PlayerCons.slashAnim);
        slashAttackColliderX.transform.localPosition = new Vector3(XAttackOffset.x * -1, XAttackOffset.y);
    }

    public void PerformAttackRight()
    {
        slashAttackColliderX.enabled = true;
        spriteRendererX.enabled = true;
        //animatorX.enabled = true;
        animatorX.Play(PlayerCons.slashAnim);
        slashAttackColliderX.transform.localPosition = XAttackOffset;
    }

    public void PerformAttackUp()
    {
        slashAttackColliderY.enabled = true;
        spriteRendererY.enabled = true;
        //animatorY.enabled = true;
        animatorY.Play(PlayerCons.slashAnim);
        slashAttackColliderY.transform.localPosition = new Vector3(YAttackOffset.x, YAttackOffset.y);
    }

    public void PerformAttackDown()
    {
        slashAttackColliderY.enabled = true;
        spriteRendererY.enabled = true;
        //animatorY.enabled = true;
        animatorY.Play(PlayerCons.slashAnim);
        slashAttackColliderY.transform.localPosition = new Vector3(YAttackOffset.x, YAttackOffset.y * -1);
    }

    public void StopAttack()
    {
        slashAttackColliderX.enabled = false;
        slashAttackColliderY.enabled = false;
        spriteRendererX.enabled = false;
        spriteRendererY.enabled = false;
        animatorX.Play(PlayerCons.noSlash);
        animatorY.Play(PlayerCons.noSlash);
        //animatorX.enabled = false;
        //animatorY.enabled = false;
    }

    public void OnChildTriggerEnter2D(Collider2D collision, int damage)
    {
        if ((slashAttackColliderX.IsTouching(collision) || slashAttackColliderY.IsTouching(collision)) && collision.gameObject.tag == TagsCons.enemyTag)
        {
            Vector2 knockbackDirection = ((Vector2)transform.position - (Vector2)collision.transform.position).normalized;
            collision.gameObject.GetComponentInParent<HealthScript>().TakeHit(damage, 
                new List<AttackEffectType> { AttackEffectType.Damage, AttackEffectType.KnockBack }, knockbackDirection,
                5f, .8f);
            Debug.Log("collider hit enemy on childe trigger");
        }
    }
}
