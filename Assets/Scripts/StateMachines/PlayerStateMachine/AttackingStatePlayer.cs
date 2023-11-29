using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateManager;

public class AttackingStatePlayer : BaseState<PlayerStateManager.PlayerStates>
{
    private PlayerControllerScript playerController;
    private Animator animator;
    private CharacterStatsScript characterStatsScript;

    public AttackingStatePlayer(CharacterStatsScript characterStatsScript, PlayerControllerScript playerController, Animator animator) 
        : base(PlayerStateManager.PlayerStates.Attack) {
 
        this.characterStatsScript = characterStatsScript;
        this.playerController = playerController;
        this.animator = animator;   
    }

    public override void EnterState()
    {
        animator.Play(PlayerCons.attackAnim);
        playerController.canMove = false;
        playerController.Attack();
        

    }

    public override void ExitState()
    {
        playerController.canMove = true;
    }

    public override PlayerStateManager.PlayerStates GetNextState()
    {
        if (!characterStatsScript.IsAlive())
        {
            return PlayerStates.Dead;
        }

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
