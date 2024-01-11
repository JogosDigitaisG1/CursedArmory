using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovementEnemyScript : MonoBehaviour
{
    private CharacterStatsScript characterStatsScript;

    [SerializeField]
    private Collider2D playerCol;
    [SerializeField]
    private Vector3 playerPos;
    private int moveSpeed = 0;
    private Rigidbody2D rb2d;

    //roaming options
    public Vector2 decisionTime = new Vector2(1, 4);
    private float decisionTimeCount = 0;

    private Vector3[] moveDirectionsX = new Vector3[] { Vector3.right, Vector3.left, Vector3.zero };
    private Vector3[] moveDirectionsY = new Vector3[] { Vector3.up, Vector3.down, Vector3.zero };

    private int currentMoveDirectionX;
    private int currentMoveDirectionY;

    public float playerDistanceOffset = 5f;
    [SerializeField]
    private float distanceToPlayer = 0f;
    public ContactFilter2D moveFilter;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float offsetCollision;

    [SerializeField]
    private bool closeToPlayer;

    public enum EnemyLookDirection
    {
        Left, Right
    }
    [SerializeField]
    private EnemyLookDirection lookDirection;

    public float dirX;

    public Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        characterStatsScript = GetComponent<CharacterStatsScript>();
        //detectScript = GetComponent<DetectScript>();
        rb2d = GetComponent<Rigidbody2D>();
        closeToPlayer = false;

        decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

        ChooseRoamDirection();
    }



    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void RoamAround()
    {
        moveSpeed = characterStatsScript.GetCurrentMoveSpeed();
        Vector3 direction = moveDirectionsX[currentMoveDirectionX] + moveDirectionsY[currentMoveDirectionY];
        float xDir = direction.x;
        float yDir = direction.y;

        bool success = TryMove(direction);

        if (!success)
        {
            success = TryMove(new Vector2(direction.x, 0));

            if (!success)
            {
                success = TryMove(new Vector2(0, direction.y));
            }
        }

        if (decisionTimeCount > 0) decisionTimeCount -= Time.deltaTime;
        else
        {
            // Choose a random time delay for taking a decision ( changing direction, or standing in place for a while )
            decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

            // Choose a movement direction, or stay in place
            ChooseRoamDirection();
        }
    }

    public void FollowPlayer()
    {

        if (playerCol != null)
        {
            playerPos = playerCol.transform.position;

            distanceToPlayer = (playerPos - transform.position).magnitude;

            Vector3 direction = (playerPos - transform.position);

            if (distanceToPlayer > playerDistanceOffset)
            {
                closeToPlayer = false;
                bool success = TryMove(direction.normalized);

                if (!success)
                {
                    success = TryMove(new Vector2(direction.x, 0));

                    if (!success)
                    {
                        success = TryMove(new Vector2(0, direction.y));
                    }
                }
            }
            else
            {
                closeToPlayer = true;
            }
        }
        else
        {
            closeToPlayer = false;
        }





    }


    private bool TryMove(Vector3 direction)
    {
        MapAngleToFourDirections();
        int count = rb2d.Cast(direction, moveFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + offsetCollision);

        if (count == 0)
        {
            moveDir = direction;
            rb2d.MovePosition(transform.position + (direction * moveSpeed * Time.fixedDeltaTime));
            return true;
        }
        else
        {
            return false;
        }
    }

    public void FollowPlayerToggle(Collider2D playerPos)
    {
        playerCol = playerPos;
        moveSpeed = characterStatsScript.GetCurrentMoveSpeed();
    }

    public void StopFollowPlayer()
    {
        closeToPlayer = false;
        playerCol = null;
    }

    public bool IsCloseToPlayer()
    {
        return closeToPlayer;
    }

    public Vector3 getPlayerPos()
    {
        return playerPos;
    }

    private void ChooseRoamDirection()
    {
        currentMoveDirectionX = Mathf.FloorToInt(Random.Range(0, moveDirectionsX.Length));
        currentMoveDirectionY = Mathf.FloorToInt(Random.Range(0, moveDirectionsY.Length));
    }

    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        if (playerCol != null)
        {
            //Gizmos.DrawLine(transform.position, playerCol.transform.position);
            Gizmos.DrawLine(transform.position, playerCol.transform.position);

        }



    }



    private void MapAngleToFourDirections()
    {
        dirX = moveDir.x;
        if (dirX >= 0)
        {
            lookDirection = EnemyLookDirection.Right;

        }
        else
        {
            lookDirection = EnemyLookDirection.Left;
        }
        
    }

    public EnemyLookDirection GetEnemyLookDirection()
    {
        return lookDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("colli");
    }
}
