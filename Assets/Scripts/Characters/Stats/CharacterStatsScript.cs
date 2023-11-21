using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsScript : MonoBehaviour
{

    public CharacterStatSO defaultStats;

    [SerializeField]
    private CharacterBaseStats baseStats;
    [SerializeField]
    private CharacterBaseStats currentStats;
    public enum Stat
    {
        Hp,
        AttackPower,
        AttackSpeed,
        Defense,
        MoveSpeed,
        SwordSpirits,
        ShieldSpirits,
        BowSpirits,
        StaffSpirits
    }

    private void Start()
    {
        InitializeStats();
    }


    private void InitializeStats()
    {
        

        if (defaultStats is CharacterStatSO baseStats)
        {

            if (defaultStats is PlayerStatsSO playerStats)
            {

                this.baseStats = new PlayerStats(baseStats.health, baseStats.attackPower, baseStats.attackSpeed, baseStats.moveSpeed,
                     playerStats.swordSpirits, playerStats.shieldSpirits, playerStats.bowSpirits, playerStats.staffSpirits);

                

            }
            else if (defaultStats is EnemyStatsSO enemyStats)
            {
                //AddStat(enemyStats.type);

                this.baseStats = new EnemyStats(baseStats.health, baseStats.attackPower, baseStats.attackSpeed, baseStats.moveSpeed);

            }
        }
        currentStats = this.baseStats;

    }


    public int GetCurrentHp()
    {

        return currentStats.Hp;
        
    }

    public int GetMaxHp()
    {
        //base stats acts as max hp.
        return baseStats.Hp;

    }

    public int GetAttackPower()
    {
        return currentStats.AttackPower;
    }

    public void SetCurrentHp(int hp)
    {
        currentStats.Hp = hp;
    }

    public void SetMaxtHp(int hp)
    {
        baseStats.Hp = hp;
    }

    public int GetCurrentMoveSpeed()
    {

        return currentStats.MoveSpeed;

    }


}
