using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerStateManager;

public class IdleEnemyState : BaseState<EnemyStateManager.EnemyStates>
{

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private DetectScript detectScript;
    private MovementEnemyScript movementEnemyScript;

    public IdleEnemyState(Animator animator, SpriteRenderer spriteRenderer, DetectScript detectScript, MovementEnemyScript movementEnemyScript) : base(EnemyStateManager.EnemyStates.Idle)
    {
        this.animator = animator;
        this.spriteRenderer = spriteRenderer;
        this.detectScript = detectScript;
        this.movementEnemyScript = movementEnemyScript;
    }

    public override void EnterState()
    {
        animator.Play(EnemyCons.IdleEnemy);

    }

    public override void ExitState()
    {
        
    }

    public override EnemyStateManager.EnemyStates GetNextState()
    {
        if (detectScript.DetectedPlayer())
        {
            return EnemyStateManager.EnemyStates.Walk;
        }
        else
        {
            return EnemyStateManager.EnemyStates.Idle;
        }
    }

    public override void UpdateState()
    {
        
    }


}
