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

    public void OnAttackStartFrame()
    {
        attackEnemyScript.Attack(movementEnemyScript.GetEnemyLookDirection());
    }

    public void OnDeadEnd()
    {
        healthScript.Dead();
    }


}
