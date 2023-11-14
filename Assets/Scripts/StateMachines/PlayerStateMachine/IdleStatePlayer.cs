using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.ShaderGraph;
using UnityEngine;
using static PlayerStateManager;

public class IdleStatePlayer : BaseState<PlayerStateManager.PlayerStates>
{
    private PlayerControllerScript playerController;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public IdleStatePlayer(PlayerControllerScript playerControllerScript, Animator animator, SpriteRenderer spriteRenderer) : base(PlayerStateManager.PlayerStates.Idle)
    {

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


    }



}
