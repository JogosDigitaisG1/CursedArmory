using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : StateManager<PlayerStateManager.PlayerStates>
{
    private PlayerControllerScript playerController;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public enum PlayerStates
    {
        Idle, 
        Walk,
        Attack,
        Dead
    }

    //private PlayerController controls;


    private void Awake()
    {
        playerController = GetComponent<PlayerControllerScript>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Initialize states
        States.Add(PlayerStates.Idle, new IdleStatePlayer(playerController, animator, spriteRenderer));
        States.Add(PlayerStates.Walk, new WalkingStatePlayer(playerController, animator, spriteRenderer));
        States.Add(PlayerStates.Attack, new AttackingStatePlayer(playerController, animator));
        States.Add(PlayerStates.Dead, new DeadStatePlayer(playerController, animator));

        // Set initial state
        CurrentState = States[PlayerStates.Idle];
        CurrentState.EnterState();
    }
}
