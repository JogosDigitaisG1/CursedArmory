using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerControllerScript;

[System.Serializable]
public class RoamEnemyState : BaseState<EnemyStateManager.EnemyStates>
{

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private DetectScript detectScript;
    private MovementEnemyScript movementEnemyScript;
    private CharacterStatsScript characterStatsScript;

    [SerializeField]
    private float roamTimerMax;
    [SerializeField]
    private float roamTimer;

    public RoamEnemyState(CharacterStatsScript characterStatsScript, Animator animator, SpriteRenderer spriteRenderer, DetectScript detectScript, MovementEnemyScript movementEnemyScript, float roamTimerMax) : base(EnemyStateManager.EnemyStates.Roam)
    {
        this.characterStatsScript = characterStatsScript;
        this.animator = animator;
        this.spriteRenderer = spriteRenderer;
        this.detectScript = detectScript;
        this.movementEnemyScript = movementEnemyScript;
        this.roamTimerMax = roamTimerMax;
        roamTimer = roamTimerMax;

    }

    public override void EnterState()
    {

        animator.Play(EnemyCons.WalkEnemy);

    }

    public override void ExitState()
    {

        roamTimer = roamTimerMax;
    }

    public override EnemyStateManager.EnemyStates GetNextState()
    {

        if (!characterStatsScript.IsAlive())
        {
            return EnemyStateManager.EnemyStates.Dead;
        }

        if (roamTimer <= 0)
        {
            return EnemyStateManager.EnemyStates.Idle;
        }
        if (detectScript.DetectedPlayer())
        {
            return EnemyStateManager.EnemyStates.Follow;
        }
        else
        {

            return EnemyStateManager.EnemyStates.Roam;
        }

    }

    public override void UpdateState()
    {

        movementEnemyScript.RoamAround();
        roamTimer -= Time.deltaTime;

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
