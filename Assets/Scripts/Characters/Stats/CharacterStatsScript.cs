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

    private bool isAlive;

    [SerializeField]
    private GameObject pickupPrefab;

    private void Awake()
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
                     playerStats.swordSpirits, playerStats.shieldSpirits, playerStats.bowSpirits, playerStats.staffSpirits, playerStats.gold, playerStats.bagCapacity);

                currentStats = new PlayerStats((PlayerStats) this.baseStats);


            }
            else if (defaultStats is EnemyStatsSO enemyStats)
            {
                Debug.Log("idle timer = " + enemyStats.idleTimer + " roamTime: " + enemyStats.roamTimer);
                //AddStat(enemyStats.type);

                this.baseStats = new EnemyStats(baseStats.health, baseStats.attackPower, baseStats.attackSpeed, baseStats.moveSpeed, enemyStats.roamTimer, enemyStats.idleTimer);

                currentStats = new EnemyStats((EnemyStats)this.baseStats);
            }
        }

        isAlive = true;



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

    public float GetEnemyIdleTimer()
    {

        if (baseStats is EnemyStats enemyStats)
        {
            Debug.Log("idle time: " + enemyStats.IdleTime);
            return enemyStats.IdleTime;
        }
        else
        {
            Debug.Log("idle time: " + 0);
            return 0;
        }        

    }

    public float GetEnemyRoamTimer()
    {

        if (baseStats is EnemyStats enemyStats)
        {
            return enemyStats.RoamTime;
        }
        else
        {
            return 0;
        }

    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public void Dead()
    {

        if (defaultStats is EnemyStatsSO enemyStats)
        {
            Instantiate(pickupPrefab, transform.position, Quaternion.identity);
        }

        isAlive = false;


    }

    public void GetPickup(PickupSO pickup)
    {
        string pickupName = pickup.name;

        if (currentStats is PlayerStats playerStats)
        {

            switch (pickupName)
            {
                case var _ when pickupName == PickupCons.sword:

                    playerStats.AddSword();
                    Debug.Log("Swords " + playerStats.SwordSpirits);

                    break;
                case var _ when pickupName == PickupCons.bow:
                    playerStats.AddBow();
                    Debug.Log("bows " + playerStats.BowSpirits);
                    break;
                case var _ when pickupName == PickupCons.staff:
                    playerStats.AddStaff();
                    Debug.Log("Staff " + playerStats.StaffSpirits);
                    break;
                case var _ when pickupName == PickupCons.shield:
                    playerStats.AddShield();
                    Debug.Log("Shield " + playerStats.ShieldSpirits);
                    break;
                case var _ when pickupName == PickupCons.gold:
                    playerStats.AddGold();
                    Debug.Log("Gold " + playerStats.Gold);
                    break;
                default:
                    Debug.Log("Pickup not found");
                    
                    break;
            }

            if (pickup.name == PickupCons.sword)
            {

            }else if (pickup.name == PickupCons.bow)
            {

            }
        }

    }



}
