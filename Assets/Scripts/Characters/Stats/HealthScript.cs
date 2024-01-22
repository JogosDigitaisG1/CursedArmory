using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthScript : MonoBehaviour
{
    [SerializeField]
    private CharacterStatsScript characterStatsScript;

    public SpriteRenderer spriteRenderer;
    private DamageEffects damageEffects;

    private bool canTakeDamage = true;




    private void Awake()
    {
       
        characterStatsScript = GetComponent<CharacterStatsScript>();

        damageEffects = GetComponent<DamageEffects>();
        if (damageEffects == null)
        {
            damageEffects = gameObject.AddComponent<DamageEffects>();
        }

    }
    
    public void TakeHit(int amount, List<AttackEffectType> attackEffects, Vector2 knockbackDirection, float knockbackForce, float knockbackDuration)
    {

        if (characterStatsScript != null && canTakeDamage)
        {
            foreach (var effect in attackEffects)
            {
                switch (effect)
                {
                    case AttackEffectType.Damage:
                        TakeDamage(amount);
                        break;
                    case AttackEffectType.KnockBack:
                      //  PerformKnockBack(knockbackDirection, knockbackForce, knockbackDuration);
                        break;

                    case AttackEffectType.Invincibility:
                        GetTemporalInvincibility(0.5f, 0.1f);
                        break;

                    default:
                        break;


                }
            }
                

        }
    }

    private void PerformKnockBack(Vector2 knockbackDirection, float knockbackForce, float knockbackDuration)
    {
        if (damageEffects != null)
        {
            damageEffects.ApplyKnockback(knockbackDirection, knockbackForce, knockbackDuration);
        }

    }

    private void GetTemporalInvincibility(float time, float blinkingTime)
    {
        canTakeDamage = false;
        damageEffects.BlinkingCompleted += OnBlinkingCompleted;
        damageEffects.StartBlinking(time, 0.1f, spriteRenderer);


    }


    private void OnBlinkingCompleted(bool success)
    {
        canTakeDamage = true;
    }

    private void TakeDamage(int amount)
    {
        int currentHP = 0;


            currentHP = characterStatsScript.GetCurrentHp();
            currentHP -= amount;


            currentHP = Mathf.Max(currentHP, 0);

            characterStatsScript.SetCurrentHp(currentHP);

            if (currentHP <= 0)
            {
                characterStatsScript.Dead();
            }
            //  ReturnAnimation();
        
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
