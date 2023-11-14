using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsScript : MonoBehaviour
{

    public ScriptableObject defaultStats; 

    private Dictionary<StatSO, float> currentStats = new Dictionary<StatSO, float>();

    private void Start()
    {
        InitializeStats();
    }

    private void InitializeStats()
    {
        currentStats.Clear();

        
        if (defaultStats is CharacterStatSO baseStats)
        {
            AddStat(baseStats.maxHealth);
            AddStat(baseStats.currentHealth, baseStats.maxHealth.baseValue);
            AddStat(baseStats.attackPower);
            AddStat(baseStats.defense);
            AddStat(baseStats.speed);

            
        }

        
        if (defaultStats is PlayerStatsSO playerStats)
        {
            AddStat(playerStats.swordSpirits);
            AddStat(playerStats.shieldSpirits);
            AddStat(playerStats.bowSpirits);
            AddStat(playerStats.staffSpirits);

            
        }
        else if (defaultStats is EnemyStatsSO enemyStats)
        {
            //AddStat(enemyStats.type);
        
        }
    }

    // Add a stat to the currentStats dictionary
    private void AddStat(StatSO stat)
    {
        currentStats.Add(stat, stat.baseValue);
    }

    private void AddStat(StatSO stat, float value)
    {
        currentStats.Add(stat, value);
    }


    public void TakeDamage(float damage)
    {
        if (defaultStats is CharacterStatSO baseStats)
        {
            currentStats[baseStats.currentHealth] = Mathf.Max(0, currentStats[baseStats.currentHealth] - damage);
        }          

    }

    public void Heal(float healAmount)
    {
        if (defaultStats is CharacterStatSO baseStats)
        {
            currentStats[baseStats.currentHealth] = Mathf.Max(0, currentStats[baseStats.currentHealth] + healAmount);
        }
    }

    public float GetHp()
    {

        return GetStatValue(((CharacterStatSO)defaultStats).currentHealth);
        
    }

    // Method to access stats
    
    public float GetStatValue(StatSO stat)
    {
        if (currentStats.ContainsKey(stat))
        {
            return currentStats[stat];
        }
        else
        {
            Debug.LogError("Stat not found: " + stat.statName);
            return 0f;
        }
    }



}
