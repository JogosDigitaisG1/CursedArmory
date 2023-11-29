using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateManager;

public class DeadStatePlayer : BaseState<PlayerStateManager.PlayerStates>
{
    private PlayerControllerScript playerController;
    private Animator animator;

    public DeadStatePlayer(PlayerControllerScript playerController, Animator animator) : base(PlayerStateManager.PlayerStates.Dead) {

        this.playerController = playerController;
        this.animator = animator;
    }

    public override void EnterState()
    {
        animator.Play(PlayerCons.deadAnim);
    }

    public override void ExitState()
    {
        
    }

    public override PlayerStateManager.PlayerStates GetNextState()
    {
        return PlayerStates.Dead;
    }

    public override void UpdateState()
    {
        
    }
}
