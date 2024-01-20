using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemyState : BaseState<EnemyStateManager.EnemyStates>
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private DetectScript detectScript;
    private MovementEnemyScript movementEnemyScript;
    private CharacterStatsScript characterStatsScript;
    private EnemyScript enemyScript;

    public FollowEnemyState(EnemyScript enemyScript, CharacterStatsScript characterStatsScript, Animator animator, SpriteRenderer spriteRenderer, DetectScript detectScript, MovementEnemyScript movementEnemyScript) 
        : base(EnemyStateManager.EnemyStates.Follow)
    {
        this.enemyScript = enemyScript;
        this.characterStatsScript = characterStatsScript;
        this.animator = animator;
        this.spriteRenderer = spriteRenderer;
        this.detectScript = detectScript;
        this.movementEnemyScript = movementEnemyScript;
    }

    public override void EnterState()
    {
        animator.Play(EnemyCons.WalkEnemy);
        movementEnemyScript.FollowPlayerToggle(detectScript.GetPlayerCollider());

        
    }

    public override void ExitState()
    {
    }

    public override EnemyStateManager.EnemyStates GetNextState()
    {
        if (!characterStatsScript.IsAlive())
        {
            return EnemyStateManager.EnemyStates.Dead;
        }

        if (enemyScript.activeRoom)
        {
            if (movementEnemyScript.IsCloseToPlayer())
            {
                return EnemyStateManager.EnemyStates.Attack;
            }
            if (detectScript.DetectedPlayer())
            {
                return EnemyStateManager.EnemyStates.Follow;
            }
            else
            {
                movementEnemyScript.StopFollowPlayer();
                return EnemyStateManager.EnemyStates.Idle;
            }
        }
        else
        {
            movementEnemyScript.StopFollowPlayer();
            return EnemyStateManager.EnemyStates.Idle;
        }

       
    }

    public override void UpdateState()
    {
        movementEnemyScript.FollowPlayer();

        if (movementEnemyScript.GetEnemyLookDirection() == MovementEnemyScript.EnemyLookDirection.Left)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

    }
}
