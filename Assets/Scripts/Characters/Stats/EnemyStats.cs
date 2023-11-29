
using UnityEngine;

[System.Serializable]
public class EnemyStats : CharacterBaseStats
{
    [SerializeField]
    private float roamTime;
    [SerializeField]
    private float idleTime;

    public float RoamTime { get => roamTime; set => roamTime = value; }
    public float IdleTime { get => idleTime; set => idleTime = value; }

    public EnemyStats(int hp, int attackPower, int attackSpeed, int moveSpeed, float roamTime, float idleTime) : base(hp, attackPower, attackSpeed, moveSpeed)
    {

        this.IdleTime = idleTime;
        this.RoamTime = roamTime;
    }

    public EnemyStats(EnemyStats enemy): base(enemy.Hp, enemy.AttackPower, enemy.AttackSpeed, enemy.MoveSpeed)
    {
        this.IdleTime = enemy.IdleTime;
        this.RoamTime = enemy.RoamTime;
    }


}
