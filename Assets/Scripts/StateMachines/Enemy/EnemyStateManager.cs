using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateManager;

public class EnemyStateManager : StateManager<EnemyStateManager.EnemyStates>
{
    private DetectScript detectScript;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private MovementEnemyScript movementEnemyScript;

    public enum EnemyStates
    {
        Idle,
        Walk,
        Attack,
        Dead
    }

    private void Awake()
    {
        detectScript = GetComponent<DetectScript>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        movementEnemyScript = GetComponent<MovementEnemyScript>();

        // Initialize states
        States.Add(EnemyStates.Idle, new IdleEnemyState(animator, spriteRenderer, detectScript, movementEnemyScript));
        States.Add(EnemyStates.Walk, new WalkEnemyState(animator, spriteRenderer, detectScript, movementEnemyScript));

        // Set initial state
        CurrentState = States[(EnemyStates.Idle)];
        CurrentState.EnterState();
    }


}
