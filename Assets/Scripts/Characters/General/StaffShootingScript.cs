using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffShootingScript : MonoBehaviour, IProjectile
{
    public ParticleSystem hitExplosion;
    public void Shoot(Vector2 targetPos, float s)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, s * Time.fixedDeltaTime);

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


}
