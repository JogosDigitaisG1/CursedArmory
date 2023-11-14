using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateManager;

public class AttackingStatePlayer : BaseState<PlayerStateManager.PlayerStates>
{
    private PlayerControllerScript playerController;
    private Animator animator;

    public AttackingStatePlayer(PlayerControllerScript playerController, Animator animator) : base(PlayerStateManager.PlayerStates.Attack) {
 
        this.playerController = playerController;
        this.animator = animator;   
    }

    public override void EnterState()
    {
        animator.Play(PlayerCons.attackAnim);
        playerController.canMove = false;
    }

    public override void ExitState()
    {
        playerController.canMove = true;
    }

    public override PlayerStateManager.PlayerStates GetNextState()
    {
        if (playerController.canMove)
        {
            if (playerController.GetInput().magnitude > 0.1f)
            {
                return PlayerStates.Walk;
            }
            else
            {
                return PlayerStates.Idle;
            }
        }else
            return PlayerStates.Attack;

    }

    public override void UpdateState()
    {
        
    }

    
}
