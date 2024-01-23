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
    private EnemyScript enemyScript;
    private BossEnemySAttack1 bossEnemySAttack1;
    private BossEnemySAttack2 bossEnemySAttack2;

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
        Dead,
        Special1,
        Special2
    }

    private void Start()
    {
        detectScript = GetComponent<DetectScript>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        movementEnemyScript = GetComponent<MovementEnemyScript>();
        characterStatsScript = GetComponent<CharacterStatsScript>();
        attackEnemyScript = GetComponentInChildren<AttackEnemyScript>();
        bossEnemySAttack1 = GetComponentInChildren<BossEnemySAttack1>();
        bossEnemySAttack2 = GetComponentInChildren<BossEnemySAttack2>();
        enemyScript = GetComponentInChildren<EnemyScript>();

        idleTimerMax = characterStatsScript.GetEnemyIdleTimer();
        roamTimerMax = characterStatsScript.GetEnemyRoamTimer();

        // Initialize states
        States.Add(EnemyStates.Idle, new IdleEnemyState(enemyScript, characterStatsScript, animator, spriteRenderer, detectScript, movementEnemyScript, idleTimerMax));
        States.Add(EnemyStates.Roam, new RoamEnemyState(enemyScript, characterStatsScript, animator, spriteRenderer, detectScript, movementEnemyScript, roamTimerMax));
        States.Add(EnemyStates.Follow, new FollowEnemyState(enemyScript, characterStatsScript, animator, spriteRenderer, detectScript, movementEnemyScript));
        States.Add(EnemyStates.Attack, new AttackEnemyState(enemyScript, characterStatsScript, animator, spriteRenderer, detectScript, movementEnemyScript, attackEnemyScript));
        States.Add(EnemyStates.Dead, new DeadEnemyState(animator, spriteRenderer, attackEnemyScript));
        States.Add(EnemyStates.Special1, new BossSpecialAttack1State(enemyScript, characterStatsScript, animator, spriteRenderer, detectScript, movementEnemyScript, attackEnemyScript, bossEnemySAttack1));
        States.Add(EnemyStates.Special2, new BossSpecialAttack2State(enemyScript, characterStatsScript, animator, spriteRenderer, detectScript, movementEnemyScript, attackEnemyScript, bossEnemySAttack2));

        // Set initial state
        CurrentState = States[(EnemyStates.Idle)];
        CurrentState.EnterState();
    }

    private void Update()
    {
       // Debug.Log(CurrentState);
    }


}
