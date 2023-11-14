using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventSenderScript : MonoBehaviour
{

    private PlayerControllerScript _playerControllerScript;

    private void Awake()
    {
        _playerControllerScript = GetComponentInParent<PlayerControllerScript>();
    }

    void OnAttackEnd()
    {
        _playerControllerScript.canMove = true;
    }
}
