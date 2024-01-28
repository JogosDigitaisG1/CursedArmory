using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventEnemyEvent : MonoBehaviour
{

    public AttackEnemyScript attackEnemyScript;
    public MovementEnemyScript movementEnemyScript;
    public HealthScript healthScript;


    public void OnAttackEnd()
    {
        attackEnemyScript.StopAttack();
    }

    public void OnAttackAnim()
    {
        attackEnemyScript.StartAttackAnim();
    }

    public void OnAttackStartFrame(int num)
    {
        if(num == 1)
        {
            SoundManager.Instance.PlaySlashSound();
        }else if(num == 2)
        {
            SoundManager.Instance.PlayStompSound();
        }
        
        attackEnemyScript.Attack(movementEnemyScript.GetEnemyLookDirection(), movementEnemyScript.getPlayerPos());
    }

    public void OnDeadEnd()
    {
        healthScript.Dead();
    }


}
