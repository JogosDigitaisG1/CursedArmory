using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationEventSenderScript : MonoBehaviour
{

    private PlayerControllerScript _playerControllerScript;
    public AttackScript _attackScript;
    private HealthScript healthScript; 
    public Canvas _canvas;

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
        
        _canvas.gameObject.SetActive(true);
        healthScript.Dead();
    }
}
