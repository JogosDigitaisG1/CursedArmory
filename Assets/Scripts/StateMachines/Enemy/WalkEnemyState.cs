using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemyState : BaseState<EnemyStateManager.EnemyStates>
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private DetectScript detectScript;
    private MovementEnemyScript movementEnemyScript;

    public WalkEnemyState(Animator animator, SpriteRenderer spriteRenderer, DetectScript detectScript, MovementEnemyScript movementEnemyScript) 
        : base(EnemyStateManager.EnemyStates.Walk)
    {
        this.animator = animator;
        this.spriteRenderer = spriteRenderer;
        this.detectScript = detectScript;
        this.movementEnemyScript = movementEnemyScript;
    }

    public override void EnterState()
    {
        animator.Play(EnemyCons.WalkEnemy);
        movementEnemyScript.FollowPlayerToggle(detectScript.GetPlayerCollider());

        Debug.Log("walk state");
        
    }

    public override void ExitState()
    {
        movementEnemyScript.StopFollowPlayer();
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
