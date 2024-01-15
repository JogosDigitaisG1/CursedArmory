using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MovementEnemyScript;

public interface IEnemyAttack
{
    void PerformAttack(EnemyLookDirection lookDirection, Vector3 playerPos);

    void StopAttack();

    void CheckTrigger(Collider2D collision);
}
