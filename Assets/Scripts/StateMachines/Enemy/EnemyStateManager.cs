using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateManager;

[System.Serializable]
public class EnemyStateManager : StateManager<EnemyStateManager.EnemyStates>
{
    private DetectScript detectScript;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private MovementEnemyScript movementEnemyScript;
    private CharacterStatsScript characterStatsScript;
    private AttackEnemyScript attackEnemyScript;

    [SerializeField]
    private float idleTimerMax;
    [SerializeField]
    private float roamTimerMax;

    public enum EnemyStates
    {
        Idle,
        Roam,
        Follow,
        Attack,
        Dead
    }

    private void Start()
    {
        detectScript = GetComponent<DetectScript>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        movementEnemyScript = GetComponent<MovementEnemyScript>();
        characterStatsScript = GetComponent<CharacterStatsScript>();
        attackEnemyScript = GetComponentInChildren<AttackEnemyScript>();

        idleTimerMax = characterStatsScript.GetEnemyIdleTimer();
        roamTimerMax = characterStatsScript.GetEnemyRoamTimer();

        // Initialize states
        States.Add(EnemyStates.Idle, new IdleEnemyState(characterStatsScript, animator, spriteRenderer, detectScript, movementEnemyScript, idleTimerMax));
        States.Add(EnemyStates.Roam, new RoamEnemyState(characterStatsScript, animator, spriteRenderer, detectScript, movementEnemyScript, roamTimerMax));
        States.Add(EnemyStates.Follow, new FollowEnemyState(characterStatsScript, animator, spriteRenderer, detectScript, movementEnemyScript));
        States.Add(EnemyStates.Attack, new AttackEnemyState(characterStatsScript, animator, spriteRenderer, detectScript, movementEnemyScript, attackEnemyScript));
        States.Add(EnemyStates.Dead, new DeadEnemyState(animator, spriteRenderer));

        // Set initial state
        CurrentState = States[(EnemyStates.Idle)];
        CurrentState.EnterState();
    }

    private void Update()
    {
       // Debug.Log(CurrentState);
    }


}
