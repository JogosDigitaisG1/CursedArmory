using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CharacterStatsScript : MonoBehaviour
{

    public CharacterStatSO defaultStats;

    [SerializeField]
    private CharacterBaseStats maxStats;

    [SerializeField]
    private CharacterBaseStats inGameMaxStats;

    [SerializeField]
    private CharacterBaseStats currentStats;

    private bool isAlive;

    public bool isPlayer;

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

            if (defaultStats is PlayerStatsSO playerStats && isPlayer)
            {

                if (GameManager.Instance.isNewGame())
                {
                    
                    maxStats = new PlayerStats(baseStats.health, baseStats.attackPower, baseStats.attackSpeed, baseStats.moveSpeed,
                 playerStats.swordSpirits, playerStats.shieldSpirits, playerStats.bowSpirits, playerStats.staffSpirits, playerStats.gold,
                     playerStats.bagCapacity, 0);

                    inGameMaxStats = new PlayerStats((PlayerStats)maxStats);
                    currentStats = new PlayerStats((PlayerStats)maxStats);

                    UpdateSavedStats();

                    GameManager.Instance.newGame = false;
                }
                else
                {
                    GetStats();
                }


            }
            else if (defaultStats is EnemyStatsSO enemyStats)
            {

                inGameMaxStats = new EnemyStats(baseStats.health, baseStats.attackPower, baseStats.attackSpeed, baseStats.moveSpeed, enemyStats.roamTimer, enemyStats.idleTimer);

                currentStats = new EnemyStats((EnemyStats)inGameMaxStats);
            }
        }
                
        isAlive = true;



    }

    public void GetStats()
    {
        Debug.Log("get stats");
        maxStats = GameManager.Instance.GetMaxPlayerData();
        inGameMaxStats = GameManager.Instance.GetIngameMaxPlayerData();
        currentStats = GameManager.Instance.GetCurrentPlayerData();
    }

    public void StatsAfterDeathRestart()
    {
        DecreaseHalfShield((PlayerStats)inGameMaxStats);
        DecreaseHalfStaff((PlayerStats)inGameMaxStats);
        DecreaseHalfSword((PlayerStats)inGameMaxStats);
        DecreaseHalfGold();

        currentStats = new PlayerStats((PlayerStats)inGameMaxStats); 

        UpdateSavedStats();
    }

    public void StatsAfterChangeScene()
    {
        inGameMaxStats = new PlayerStats((PlayerStats)maxStats);
        currentStats = new PlayerStats((PlayerStats)maxStats);

        UpdateSavedStats();
    }



    public void ApplySavedStats(PlayerStats savedData)
    {
        if (savedData != null)
        {
            inGameMaxStats = savedData;
            currentStats = savedData;
        }
    }

    public void UpdateSavedStats()
    {

        GameManager.Instance.SavePlayerData((PlayerStats)maxStats, (PlayerStats)inGameMaxStats, (PlayerStats)currentStats);

    }


    public int GetCurrentHp()
    {
        return currentStats.Hp; 
    }

    public int GetInGameMaxHp()
    {
        return inGameMaxStats.Hp;

    }

    public int GetMaxHp()
    {
        return maxStats.Hp;

    }

    public int GetAttackPower()
    {
        return currentStats.AttackPower;
    }

    public int GetMaxAttackPower()
    {
        return maxStats.AttackPower;
    }

    public int GetInGameMaxAttackPower()
    {
        return inGameMaxStats.AttackPower;
    }

    public void SetCurrentHp(int hp)
    {
        currentStats.Hp = hp;
    }

    public void SetMaxHp(int hp)
    {
        maxStats.Hp = hp;
    }

    public void RaiseInGameMaxHp(int amount)
    {
            int currentInGameMaxHP = GetInGameMaxHp();
            currentInGameMaxHP += amount;

            SetInGameMaxHp(currentInGameMaxHP);

        RaiseCurrentHp(amount);


    }

    public void DecreaseInGameMaxHp(int amount)
    {
        int currentInGameMaxHP = GetInGameMaxHp();
        currentInGameMaxHP -= amount;

        SetInGameMaxHp(currentInGameMaxHP);

        SetCurrentHp(Mathf.Clamp(GetCurrentHp(), 0, currentInGameMaxHP));


    }

    public void RaiseCurrentHp(int amount)
    {
        int currentHP = GetCurrentHp();
        currentHP += amount;

        SetCurrentHp(currentHP);

    }

    public void DecreaseCurrentHp(int amount)
    {
        int currentHP = GetCurrentHp();
        currentHP -= amount;

        SetCurrentHp(currentHP);

    }

    public void RaiseMaxHp(int amount)
    {

            int maxHP = GetMaxHp();
            maxHP += amount;

            SetMaxHp(maxHP);
        
    }

    public void SetInGameMaxHp(int hp)
    {
        inGameMaxStats.Hp = hp;
    }

    public int GetCurrentMoveSpeed()
    {

        return currentStats.MoveSpeed;

    }

    public int GetMaxMoveSpeed()
    {

        return maxStats.MoveSpeed;

    }

    public int GetInGameMaxMoveSpeed()
    {

        return inGameMaxStats.MoveSpeed;

    }

    public float GetEnemyIdleTimer()
    {

        if (!isPlayer)
        {
            EnemyStats enemyStats = (EnemyStats)inGameMaxStats;

            return enemyStats.IdleTime;
        }
        else
        {
            return 0;
        }

    }

    public float GetEnemyRoamTimer()
    {

        if (!isPlayer)
        {
            EnemyStats enemyStats = (EnemyStats)inGameMaxStats;

            return enemyStats.RoamTime;
        }
        else
        {
            return 0;
        }
    }
    public int GetGold()
    {

        if (maxStats is PlayerStats playerStats && isPlayer)
        {
            return playerStats.Gold;
        }
        else return 0;
            
    }

    public void SetGold(int num)
    {
        if (maxStats is PlayerStats playerStats && isPlayer)
        {
            playerStats.Gold = num;
            SyncCurrentGold();
        }
    }

    public void DecreaseGold(int num)
    {

        if (maxStats is PlayerStats playerStats && isPlayer)
        {
            int currentGold = GetGold();

            currentGold -= num;
            SetGold(currentGold);
            SyncCurrentGold();
        }
        
    }

    public void IncreaseOneGold()
    {
        if (maxStats is PlayerStats playerStats && isPlayer)
        {
            playerStats.AddGold();
            SyncCurrentGold();
        }
    }

    public void SyncCurrentGold()
    {
        if (currentStats is PlayerStats playerStats && isPlayer && inGameMaxStats is PlayerStats playerStatsInGame)
        {
            playerStats.Gold = GetGold();
            playerStatsInGame.Gold = GetGold();
        }
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public void Dead()
    {
        if (defaultStats is EnemyStatsSO)
        {
            Instantiate(pickupPrefab, transform.position, Quaternion.identity);
        }

        isAlive = false;
    }

    public void GetPickup(PickupSO pickup)
    {
        string pickupName = pickup.name;

        if (currentStats is PlayerStats playerStats && inGameMaxStats is PlayerStats playerStatsIngameMax)
        {

            switch (pickupName)
            {
                case var _ when pickupName == PickupCons.sword:

                    SoundManager.Instance.PlayPickupSound();
                    playerStats.AddSword();
                    playerStatsIngameMax.AddSword();
                    IncreseOnePowerPickup(5);
                    
                    break;
                case var _ when pickupName == PickupCons.bow:
                    playerStats.AddBow();
                    
                    break;
                case var _ when pickupName == PickupCons.staff:
                    SoundManager.Instance.PlayPickupSound();
                    playerStats.AddStaff();
                    IncreseOnePowerPickup(2);
                    RaiseInGameMaxHp(5);
                    playerStatsIngameMax.AddStaff();
                    
                    break;
                case var _ when pickupName == PickupCons.shield:
                    SoundManager.Instance.PlayPickupSound();
                    playerStats.AddShield();
                    playerStatsIngameMax.AddShield();
                    RaiseInGameMaxHp(10);
                    
                    break;
                case var _ when pickupName == PickupCons.gold:
                    SoundManager.Instance.PlayCoinSound();
                    IncreaseOneGold();
                    
                    break;
                default:
                    Debug.Log("Pickup not found");

                    break;
            }

        }

    }

    public PlayerStats GetCurrentPlayerStats()
    {
        if (currentStats is PlayerStats playerStats)
        {
            return (PlayerStats)currentStats;
        }
        return null;
    }


    private void IncreseOnePowerPickup(int value)
    {
        currentStats.AttackPower = currentStats.AttackPower + value;
        inGameMaxStats.AttackPower = inGameMaxStats.AttackPower + value;
    }


    private void DecreaseHalfSword(PlayerStats currentStats)
    {
        int swordsNum = currentStats.SwordSpirits;
        int halfSwords = 0;
        if (swordsNum > 1) {
            halfSwords = swordsNum / 2;
            currentStats.AttackPower = currentStats.AttackPower - halfSwords * 5;
            currentStats.DecreaseFromBag(halfSwords);
        }
        else if (swordsNum == 1)
        {
            halfSwords = 0;
            currentStats.DecreaseFromBag(1);
            currentStats.AttackPower = currentStats.AttackPower - 5;
        }

        currentStats.SwordSpirits = halfSwords;
    }

    private void DecreaseHalfStaff(PlayerStats currentStats)
    {
        int staffNum = currentStats.StaffSpirits;
        int halfStaff = staffNum / 2;
        if (staffNum > 1)
        {
            halfStaff = staffNum / 2;
            SetInGameMaxHp(currentStats.Hp - halfStaff * 5);
            currentStats.AttackPower = currentStats.AttackPower - halfStaff * 2;
            currentStats.DecreaseFromBag(halfStaff);
            // currentStats.AttackPower = currentStats.AttackPower - halfStaff * 2;
        }
        else if (staffNum == 1)
        {
            halfStaff = 0;
            currentStats.AttackPower = currentStats.AttackPower - 2;
            SetInGameMaxHp(currentStats.Hp - 5);
            currentStats.DecreaseFromBag(1);
            // currentStats.AttackPower = currentStats.AttackPower - 2;
        }

        currentStats.StaffSpirits = halfStaff;

    }

    private void DecreaseHalfShield(PlayerStats currentStats)
    {
        int shieldNum = currentStats.ShieldSpirits;
        int halfShield = shieldNum / 2;
        if (shieldNum > 1)
        {
            halfShield = shieldNum / 2;
            SetInGameMaxHp(currentStats.Hp - halfShield * 10);
            currentStats.DecreaseFromBag(halfShield);
        }
        else if (shieldNum == 1)
        {
            halfShield = 0;
            currentStats.DecreaseFromBag(1);
            SetInGameMaxHp(currentStats.Hp  - 10);
        }

        currentStats.ShieldSpirits = halfShield;

    }

    public void DecreaseHalfGold()
    {

        DecreaseGold(GetGold() / 2);
    }

    public bool BagNotFull()
    {

        if (currentStats is PlayerStats playerStats && isPlayer)
        {
            return playerStats.BagNotFull();
        }else
            return false;

    }

    public int GetNumberOfSwords()
    {
        if (currentStats is PlayerStats playerStats && isPlayer)
        {
            return playerStats.SwordSpirits;
        }
        else
            return 0;
    }

    public int GetNumberOfStaffs()
    {
        if (currentStats is PlayerStats playerStats && isPlayer)
        {
            return playerStats.StaffSpirits;
        }
        else
            return 0;
    }




}
