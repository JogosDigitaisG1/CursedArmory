using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override PlayerStateManager.PlayerStates GetNextState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}
