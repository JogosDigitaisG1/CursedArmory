using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerShootingScript : MonoBehaviour, IProjectile
{
    public ParticleSystem hitExplosion;
    public ProjectileSO projectileSO;
    public void Shoot(Vector2 targetPos)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, projectileSO.speed * Time.fixedDeltaTime);
         

        if (transform.position.x == targetPos.x && transform.position.y == targetPos.y )
        {
            var particle = Instantiate(hitExplosion, transform.position, transform.rotation);
            
            DestroyProjectile(particle);
        }
    }

    private void DestroyProjectile(ParticleSystem particle)
    {
        Destroy(particle.gameObject, 1.0f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TagsCons.enemyTag)
        {
            print("porjectile hit enemy");
            var particle = Instantiate(hitExplosion, transform.position, transform.rotation);

            DestroyProjectile(particle);

            collision.gameObject.GetComponentInParent<HealthScript>().TakeHit(projectileSO.damage, new List<AttackEffectType>
            { AttackEffectType.Damage}, Vector2.zero, 0f, 0f);
        }
    }



}
