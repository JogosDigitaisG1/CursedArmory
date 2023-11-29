using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthScript : MonoBehaviour
{
    [SerializeField]
    private CharacterStatsScript characterStatsScript;




    private void Awake()
    {
       
        characterStatsScript = GetComponent<CharacterStatsScript>();


    }

    public void TakeDamage(int amount)
    {
        
        int currentHP = 0;
        if (characterStatsScript != null)
        {
           // PlayHurtAnimation();
            currentHP = characterStatsScript.GetCurrentHp();
            currentHP -= amount;


            currentHP = Mathf.Max(currentHP, 0);

            characterStatsScript.SetCurrentHp(currentHP);

            if(currentHP <= 0)
            {
                characterStatsScript.Dead();
            }
          //  ReturnAnimation();
        }
    }



    public void Dead()
    {

        Destroy(gameObject);
    }

    public void Heal(int amount)
    {
        if (characterStatsScript != null)
        {
            
            int currentHP = characterStatsScript.GetCurrentHp();
            currentHP += amount;

            
            int maxHP = characterStatsScript.GetMaxHp();
            currentHP = Mathf.Min(currentHP, maxHP);

            
            characterStatsScript.SetCurrentHp(currentHP);

            
        }
    }

    public void RaiseMaxHp(int amount)
    {
        if (characterStatsScript != null)
        {
            int currentMaxHP = characterStatsScript.GetMaxHp();
            currentMaxHP += amount;

            characterStatsScript.SetMaxtHp(currentMaxHP);
        }
    }
}
