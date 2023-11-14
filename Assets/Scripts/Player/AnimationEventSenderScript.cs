using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventSenderScript : MonoBehaviour
{

    private PlayerControllerScript _playerControllerScript;
    public AttackScript _attackScript;

    private void Awake()
    {
        _playerControllerScript = GetComponentInParent<PlayerControllerScript>();
    }

    void OnAttackEnd()
    {
        _playerControllerScript.canMove = true;
        _attackScript.StopAttack();
    }
}
