using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile.asset", menuName = "Projectiles/Projectile")]
public class ProjectileSO : ScriptableObject
{
    public float speed;
    public int damage;

}
