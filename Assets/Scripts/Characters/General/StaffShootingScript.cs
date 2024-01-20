using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffShootingScript : MonoBehaviour, IProjectile
{
    public ParticleSystem hitExplosion;
    public ProjectileSO projectileSO;
    public void Shoot(Vector2 targetPos)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, projectileSO.speed * Time.fixedDeltaTime);

        if (transform.position.x == targetPos.x && transform.position.y == targetPos.y)
        {
            Instantiate(hitExplosion, transform.position, transform.rotation);
            DestroyProjectile();
        }
    }
     
    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TagsCons.playerTag)
        {
            print("porjectile hit");
            Instantiate(hitExplosion, transform.position, transform.rotation);
            DestroyProjectile();
            collision.gameObject.GetComponentInParent<HealthScript>().TakeHit(projectileSO.damage, new List<AttackEffectType> { AttackEffectType.Damage,
                AttackEffectType.Invincibility }, Vector2.zero, 0f, 0f);
        }
    }

}
