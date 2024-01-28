using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttack : MonoBehaviour, IPlayerAttack
{
    [SerializeField]
    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject proyectile;

    private bool canShoot = false;

    private Vector2 targetPos;



    private void Start()
    {
        timeBtwShots = startTimeBtwShots;

    }

    private void Update()
    {

        if (timeBtwShots > 0)
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    //animator.SetTrigger(PlayerCons.slashAnim);

    public PlayerAttackType GetAttackType()
    {
        return PlayerAttackType.Shoot;
    }

    public void PerformAttackLeft()
    {
        Shoot();
    }

    public void PerformAttackRight()
    {
        Shoot();
    }

    public void PerformAttackUp()
    {
        Shoot();
    }

    public void PerformAttackDown()
    {
        Shoot();
    }


    public void PerformAttack()
    {
        Shoot();
    }

    private void Shoot()
    {
        SoundManager.Instance.PlaySpellSound();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (timeBtwShots <= 0)
        {
            GameObject projectile = Instantiate(proyectile, transform.position, Quaternion.identity);
            projectile.GetComponent<PlayerProjectileScript>().targetPos = mousePosition;
            timeBtwShots = startTimeBtwShots;
        }
    }
    public void StopAttack()
    {

    }

    public void OnChildTriggerEnter2D(Collider2D collision, int damage)
    {

    }
}
