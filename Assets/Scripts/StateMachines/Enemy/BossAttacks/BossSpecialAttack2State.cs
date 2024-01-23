using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpecialAttack2State : BaseState<EnemyStateManager.EnemyStates>
{

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private DetectScript detectScript;
    private MovementEnemyScript movementEnemyScript;
    private AttackEnemyScript attackEnemyScript;
    private CharacterStatsScript characterStatsScript;
    private EnemyScript enemyScript;
    private BossEnemySAttack2 BossEnemySAttack2;


    public BossSpecialAttack2State(EnemyScript enemyScript, CharacterStatsScript characterStatsScript, Animator animator, SpriteRenderer spriteRenderer, DetectScript detectScript, MovementEnemyScript movementEnemyScript,
        AttackEnemyScript attackEnemyScript, BossEnemySAttack2 BossEnemySAttack2) : base(EnemyStateManager.EnemyStates.Attack)
    {
        this.enemyScript = enemyScript;
        this.characterStatsScript = characterStatsScript;
        this.animator = animator;
        this.spriteRenderer = spriteRenderer;
        this.detectScript = detectScript;
        this.movementEnemyScript = movementEnemyScript;
        this.attackEnemyScript = attackEnemyScript;
        this.BossEnemySAttack2 = BossEnemySAttack2;
    }

    public override void EnterState()
    {
        animator.Play(EnemyCons.AttackEnemy);
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
            if (!attackEnemyScript.IsAttacking())
            {
                if (detectScript.DetectedPlayer())
                {
                    attackEnemyScript.StopAttack();
                    return EnemyStateManager.EnemyStates.Follow;
                }
                else
                {
                    attackEnemyScript.StopAttack();
                    movementEnemyScript.StopFollowPlayer();
                    return EnemyStateManager.EnemyStates.Idle;
                }
            }
            else
            {
                return EnemyStateManager.EnemyStates.Attack;
            }
           
        }
        else
        {
            attackEnemyScript.StopAttack();
            movementEnemyScript.StopFollowPlayer();
            return EnemyStateManager.EnemyStates.Idle;
        }
        
    }

    public override void UpdateState()
    {
        movementEnemyScript.FollowPlayer();

        if (!attackEnemyScript.IsAttacking())
        {
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
}
