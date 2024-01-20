using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileScript : MonoBehaviour
{


   // public Transform target;
    public Vector2 targetPos;

    private IProjectile iProjectile;

    // Start is called before the first frame update
    void Start()
    {
        iProjectile = GetComponent<IProjectile>();
       //targetPos = new Vector2(target.position.x, target.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        iProjectile.Shoot(targetPos);
    }



}
