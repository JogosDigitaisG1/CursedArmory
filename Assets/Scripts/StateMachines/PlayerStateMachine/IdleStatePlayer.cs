using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.ShaderGraph;
using UnityEngine;
using static PlayerControllerScript;
using static PlayerStateManager;

public class IdleStatePlayer : BaseState<PlayerStateManager.PlayerStates>
{
    private PlayerControllerScript playerController;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private CharacterStatsScript characterStatsScript;

    public IdleStatePlayer(CharacterStatsScript characterStatsScript, PlayerControllerScript playerControllerScript, Animator animator,
        SpriteRenderer spriteRenderer) : base(PlayerStateManager.PlayerStates.Idle)
    {
        this.characterStatsScript = characterStatsScript;
        playerController = playerControllerScript;
        this.animator = animator;
        this.spriteRenderer = spriteRenderer;
    }

    public override void EnterState()
    {
        animator.Play(PlayerCons.idleAnim);
    }

    public override void ExitState()
    {
        //Debug.Log("Exit idle");
    }

    public override PlayerStateManager.PlayerStates GetNextState()
    {
        if (!characterStatsScript.IsAlive())
        {
            return PlayerStates.Dead;
        }


        if (playerController.IsAttacking())
        {
            return PlayerStates.Attack;
        }

        if (playerController.GetInput().magnitude > 0.1f)
        {
            return PlayerStates.Walk;
        }
        else
        {
            return PlayerStates.Idle;
        }
        
    }

    public override void UpdateState()
    {
        LooKDirection();

    }

    private void LooKDirection()
    {

        //animator.SetFloat(PlayerCons.xMove, playerController.GetVectorDirectionNormalized().x);
        //animator.SetFloat(PlayerCons.yMove, playerController.GetVectorDirectionNormalized().y);

        animator.SetFloat(PlayerCons.lookAngle, playerController.GetAngle());

        if (playerController.GetDirection() == LookDirection.Left)
            {
                spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        
    }
}
