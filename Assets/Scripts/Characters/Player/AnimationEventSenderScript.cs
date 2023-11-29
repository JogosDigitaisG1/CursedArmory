using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventSenderScript : MonoBehaviour
{

    private PlayerControllerScript _playerControllerScript;
    public AttackScript _attackScript;
    private HealthScript healthScript; 

    private void Awake()
    {
        _playerControllerScript = GetComponentInParent<PlayerControllerScript>();
        healthScript = GetComponentInParent<HealthScript>();
    }

    void OnAttackEnd()
    {
        _playerControllerScript.canMove = true;
        _attackScript.StopAttack();
    }

    public void OnDeadEnd()
    {
        healthScript.Dead();
    }
}
