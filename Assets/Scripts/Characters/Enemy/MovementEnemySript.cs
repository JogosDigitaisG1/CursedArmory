using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovementEnemyScript : MonoBehaviour
{
    private bool followPlayer;
    private CharacterStatsScript characterStatsScript;

    [SerializeField]
    private Collider2D playerCol;
    private int speed = 0;
    private Rigidbody2D rb2d;

    Vector3 dir2;

    // Start is called before the first frame update
    void Start()
    {
        characterStatsScript = GetComponent<CharacterStatsScript>();
        //detectScript = GetComponent<DetectScript>();
        rb2d = GetComponent<Rigidbody2D>();
        followPlayer = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (followPlayer)
        {
            FollowPlayer();

        }
        else
        {
            RoamAround();
        }
    }

    private void RoamAround()
    {
        //Vector2 thisVector2 = transform.position;
        //Vector3 randomDir  = ((UnityEngine.Random.insideUnitCircle * 5) - thisVector2).normalized;
         dir2 = Random.insideUnitCircle;

  


       // rb2d.MovePosition(transform.position + ((dir2 * 5) * speed * Time.fixedDeltaTime));
    }

    private void FollowPlayer()
    {
        Vector3 playerPos = playerCol.transform.position;
        Vector3 dir = (playerPos - transform.position).normalized;
        rb2d.MovePosition(transform.position + (dir * speed * Time.fixedDeltaTime));
    }

    public void FollowPlayerToggle(Collider2D playerPos)
    {
        this.playerCol = playerPos;
        speed = characterStatsScript.GetCurrentMoveSpeed();
        followPlayer = true;
    }

    public void StopFollowPlayer()
    {
        followPlayer = false;
        playerCol = null;
    }

    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        if (playerCol != null)
        {
            Gizmos.DrawLine(transform.position, playerCol.transform.position);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, dir2 * 5);

    }
}
