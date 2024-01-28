using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingScript : MonoBehaviour, IEnemyAttack
{
    [SerializeField]
    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject proyectile;

    private bool canShoot = false;

    private Vector2 playerPos;
    void Start()
    {
        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            Shoot();
        }
        

    }

    public void PerformAttack(MovementEnemyScript.EnemyLookDirection lookDirection, Vector3 playerPos)
    {
        this.playerPos = playerPos;
        canShoot = true;
    }

    private void Shoot()
    {
        if (timeBtwShots <= 0)
        {
            SoundManager.Instance.PlaySpellSound();
            GameObject projectile = Instantiate(proyectile, transform.position, Quaternion.identity);
            projectile.GetComponent<EnemyProjectileScript>().targetPos = playerPos;
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void StopAttack()
    {
        playerPos = Vector3.zero;
        canShoot = false;
    }

    public void CheckTrigger(Collider2D collision)
    {
       
    }
}
