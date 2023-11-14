using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateManager;

public class WalkingStatePlayer : BaseState<PlayerStateManager.PlayerStates>
{
    private PlayerControllerScript playerController;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public WalkingStatePlayer(PlayerControllerScript playerController, Animator animator, SpriteRenderer spriteRenderer) : base(PlayerStateManager.PlayerStates.Walk) {
    
        this.playerController = playerController;
        this.animator = animator;
        this.spriteRenderer = spriteRenderer;
    }   

    public override void EnterState()
    {
        animator.Play(PlayerCons.walkAnim);
    }

    public override void ExitState()
    {
        //throw new System.NotImplementedException();
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
        //Debug.Log("Walking: " + playerController.GetInput());
        if (playerController.GetInput() != Vector2.zero)
        {
            animator.SetFloat(PlayerCons.xMove, playerController.GetInput().x);
            animator.SetFloat(PlayerCons.yMove, playerController.GetInput().y);

            Debug.Log("set floatx " + PlayerCons.xMove + " " + playerController.GetInput().x);
            Debug.Log("set floaty " + PlayerCons.xMove + " " + playerController.GetInput().x);

            if (playerController.GetInput().x > 0)
            {
                spriteRenderer.flipX = false;
            }else if(playerController.GetInput().x < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
    }
}
