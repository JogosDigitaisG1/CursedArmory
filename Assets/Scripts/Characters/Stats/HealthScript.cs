using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class HealthScript : MonoBehaviour
{
    [SerializeField]
    private CharacterStatsScript characterStatsScript;

    public Collider2D mainBodyCollider;

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
            StartBlinking(.5f, .1f, spriteRenderer);
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

        if (mainBodyCollider != null)
        {
            mainBodyCollider.enabled = false;
        }
        canTakeDamage = false;
        Blink(time);

    }

    private void Blink(float time)
    {
        damageEffects.BlinkingCompleted += OnBlinkingCompleted;
        damageEffects.StartBlinking(time, 0.1f, spriteRenderer);
    }

    private void OnBlinkingCompleted(bool success)
    {
        canTakeDamage = true;
        if (mainBodyCollider != null)
        {
            mainBodyCollider.enabled = true;
        }
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

            
            int maxHP = characterStatsScript.GetInGameMaxHp();
            currentHP = Mathf.Min(currentHP, maxHP);

            
            characterStatsScript.SetCurrentHp(currentHP);

            
        }
    }

    public void RaiseInGameMaxHp(int amount)
    {
        if (characterStatsScript != null)
        {
            int currentInGameMaxHP = characterStatsScript.GetInGameMaxHp();
            currentInGameMaxHP += amount;

            characterStatsScript.SetInGameMaxHp(currentInGameMaxHP);
        }
    }

    public void RaiseMaxHp(int amount)
    {
        if (characterStatsScript != null)
        {
            int maxHP = characterStatsScript.GetMaxHp();
            maxHP += amount;

            characterStatsScript.SetMaxHp(maxHP);
        }
    }



    public void StartBlinking(float duration, float blinkInterval, SpriteRenderer spriteRenderer)
    {
        StartCoroutine(BlinkRoutine(duration, blinkInterval, spriteRenderer));
    }

    private IEnumerator BlinkRoutine(float duration, float blinkInterval, SpriteRenderer spriteRenderer)
    {
        if (spriteRenderer == null) yield break;

        float elapsedTime = 0f;
        bool visible = true;

        Quaternion startRotation = transform.localRotation;

        while (elapsedTime < duration)
        {
            yield return new WaitForSeconds(blinkInterval);
            visible = !visible;
            spriteRenderer.enabled = visible;

            float currentAngle = Mathf.Sin(elapsedTime * 20f) * 30f;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));


            elapsedTime += blinkInterval;


        }

        transform.localRotation = startRotation;

        spriteRenderer.enabled = true; 

    }



}
