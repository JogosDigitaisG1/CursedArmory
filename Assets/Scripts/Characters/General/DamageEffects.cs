using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffects : MonoBehaviour
{

    //knockback effect
    public void ApplyKnockback(Vector2 direction, float force, float duration)
    {
        StartCoroutine(KnockbackRoutine(direction, force, duration));
    }

    private IEnumerator KnockbackRoutine(Vector2 direction, float speed, float duration)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float timer = 0f;

            while (timer < duration)
            {

                print("knicking back");
                // Move the kinematic body in the knockback direction
                rb.MovePosition(rb.position + (-direction.normalized * speed * Time.deltaTime));

                timer += Time.deltaTime;
                yield return null;
            }
        }
    }



    //Blinking effect
    public event Action<bool> BlinkingCompleted;

    public void StartBlinking(float duration, float blinkInterval, SpriteRenderer spriteRenderer)
    {
        StartCoroutine(BlinkRoutine(duration, blinkInterval, spriteRenderer));
    }

    private IEnumerator BlinkRoutine(float duration, float blinkInterval, SpriteRenderer spriteRenderer)
    {
        if (spriteRenderer == null) yield break;

        float elapsedTime = 0f;
        bool visible = true;

        while (elapsedTime < duration)
        {
            yield return new WaitForSeconds(blinkInterval);
            visible = !visible;
            spriteRenderer.enabled = visible;
            elapsedTime += blinkInterval;
        }

        spriteRenderer.enabled = true; // Ensure it's visible after blinking

        // Notify subscribers that blinking has completed
        BlinkingCompleted?.Invoke(true);
    }


}
