using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerAttack
{
    void PerformAttackLeft();
    void PerformAttackRight();
    void PerformAttackUp();
    void PerformAttackDown();

    void StopAttack();

    void OnChildTriggerEnter2D(Collider2D collision, int damage);


    PlayerAttackType GetAttackType();
}
