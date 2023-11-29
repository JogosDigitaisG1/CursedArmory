using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class IdleEnemyState : BaseState<EnemyStateManager.EnemyStates>
{

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private DetectScript detectScript;
    private MovementEnemyScript movementEnemyScript;
    private CharacterStatsScript characterStatsScript;

    [SerializeField]
    private float idleTimerMax;
    [SerializeField]
    private float idleTimer;

    public IdleEnemyState(CharacterStatsScript characterStatsScript, Animator animator, SpriteRenderer spriteRenderer, DetectScript detectScript, MovementEnemyScript movementEnemyScript, float idleTimerMax) : base(EnemyStateManager.EnemyStates.Idle)
    {
        this.characterStatsScript = characterStatsScript;
        this.animator = animator;
        this.spriteRenderer = spriteRenderer;
        this.detectScript = detectScript;
        this.movementEnemyScript = movementEnemyScript;
        this.idleTimerMax = idleTimerMax;
        idleTimer = idleTimerMax;

    }

    public override void EnterState()
    {
       
        animator.Play(EnemyCons.IdleEnemy);

    }

    public override void ExitState()
    {
        idleTimer = idleTimerMax;
    }

    public override EnemyStateManager.EnemyStates GetNextState()
    {
        if (!characterStatsScript.IsAlive())
        {
            return EnemyStateManager.EnemyStates.Dead;
        }


        if (idleTimer <= 0)
        {
            return EnemyStateManager.EnemyStates.Roam;
        }
        if (detectScript.DetectedPlayer())
        {
            return EnemyStateManager.EnemyStates.Follow;
        }
        else
        {
            return EnemyStateManager.EnemyStates.Idle;
        }

    }

    public override void UpdateState()
    {

         idleTimer -= Time.deltaTime;
    
    }


}
