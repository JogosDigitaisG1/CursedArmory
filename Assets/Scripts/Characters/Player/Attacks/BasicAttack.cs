using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour, IPlayerAttack
{
    public Collider2D basicAttackColliderX;
    public Collider2D basicAttackColliderY;
    private Vector2 XAttackOffset;
    private Vector2 YAttackOffset;

    private void Start()
    {
        XAttackOffset = basicAttackColliderX.transform.localPosition;
        YAttackOffset = basicAttackColliderY.transform.localPosition;
        basicAttackColliderX.enabled = false;
        basicAttackColliderY.enabled = false;
    }

    public PlayerAttackType GetAttackType()
    {
        return PlayerAttackType.Basic;
    }

    public void PerformAttackDown()
    {
        basicAttackColliderY.enabled = true;
        basicAttackColliderY.transform.localPosition = new Vector3(YAttackOffset.x, YAttackOffset.y * -1);
    }

    public void PerformAttackLeft()
    {
        basicAttackColliderX.enabled = true;
        basicAttackColliderX.transform.localPosition = new Vector3(XAttackOffset.x * -1, XAttackOffset.y);
    }

    public void PerformAttackRight()
    {
        basicAttackColliderX.enabled = true;
        basicAttackColliderX.transform.localPosition = XAttackOffset;
    }

    public void PerformAttackUp()
    {
        basicAttackColliderY.enabled = true;
        basicAttackColliderY.transform.localPosition = new Vector3(YAttackOffset.x, YAttackOffset.y);
    }

    public void OnChildTriggerEnter2D(Collider2D collision, int damage)
    {
        if ((basicAttackColliderX.IsTouching(collision) || basicAttackColliderY.IsTouching(collision)) && collision.gameObject.tag == TagsCons.enemyTag)
        {
            collision.gameObject.GetComponentInParent<HealthScript>().TakeDamage(damage);
            Debug.Log("collider hit enemy on childe trigger");
        }
    }

    public void StopAttack()
    {
        basicAttackColliderX.enabled = false;
        basicAttackColliderY.enabled = false;
    }
}
