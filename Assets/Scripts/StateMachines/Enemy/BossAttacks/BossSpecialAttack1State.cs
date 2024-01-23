using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpecialAttack1State : BaseState<EnemyStateManager.EnemyStates>
{

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private DetectScript detectScript;
    private MovementEnemyScript movementEnemyScript;
    private AttackEnemyScript attackEnemyScript;
    private CharacterStatsScript characterStatsScript;
    private EnemyScript enemyScript;
    private BossEnemySAttack1 bossEnemySAttack1;

    public BossSpecialAttack1State(EnemyScript enemyScript, CharacterStatsScript characterStatsScript, Animator animator, SpriteRenderer spriteRenderer, DetectScript detectScript, MovementEnemyScript movementEnemyScript,
        AttackEnemyScript attackEnemyScript, BossEnemySAttack1 bossEnemySAttack1) : base(EnemyStateManager.EnemyStates.Special1)
    {
        this.enemyScript = enemyScript;
        this.characterStatsScript = characterStatsScript;
        this.animator = animator;
        this.spriteRenderer = spriteRenderer;
        this.detectScript = detectScript;
        this.movementEnemyScript = movementEnemyScript;
        this.attackEnemyScript = attackEnemyScript;
        this.bossEnemySAttack1 = bossEnemySAttack1;
    }

    public override void EnterState()
    {
        animator.Play(EnemyCons.IdleEnemy);

            bossEnemySAttack1.SetAttack(movementEnemyScript.getPlayerPos());

        
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

            if (bossEnemySAttack1.isAttacking)
            {
                return EnemyStateManager.EnemyStates.Special1;
            }
            else
            {
                attackEnemyScript.attacks = 0;
                bossEnemySAttack1.canAttack = true;
                return EnemyStateManager.EnemyStates.Idle;
            }
           
        }
        else
        {
            attackEnemyScript.attacks = 0;
            bossEnemySAttack1.StopAttack();
            attackEnemyScript.StopAttack();
            movementEnemyScript.StopFollowPlayer();
            return EnemyStateManager.EnemyStates.Idle;
        }
        
    }

    public override void UpdateState()
    {

        //bossEnemySAttack1.Attack();
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
