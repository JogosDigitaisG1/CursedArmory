using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemyState : BaseState<EnemyStateManager.EnemyStates>
{

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private DetectScript detectScript;
    private MovementEnemyScript movementEnemyScript;

    public DeadEnemyState(Animator animator, SpriteRenderer spriteRenderer) : base(EnemyStateManager.EnemyStates.Dead)
    {
        this.animator = animator;
        this.spriteRenderer = spriteRenderer;

    }

    public override void EnterState()
    {
        animator.Play(EnemyCons.DeadEnemy);
    }

    public override void ExitState()
    {
        
    }

    public override EnemyStateManager.EnemyStates GetNextState()
    {
        return EnemyStateManager.EnemyStates.Dead;
    }

    public override void UpdateState()
    {
        
    }
}
