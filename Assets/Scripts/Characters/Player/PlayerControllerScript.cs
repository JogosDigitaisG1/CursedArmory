using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerScript : MonoBehaviour
{

    public enum LookDirection
    {
        Left, Right, Up, Down
    }

    private AttackScript attackScript;

    private LookDirection lookDirection;

    public CharacterStatsScript characterStats;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private float moveSpeed = 1f;

    public bool canMove = true;

    public bool enteringNewRoom = false;
    private Vector2 newRoomcenter = Vector2.zero;

    private Vector3 mouseVector = Vector3.zero;

    float anglePos = 0f;

    public ContactFilter2D moveFilter;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float offsetCollision;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        characterStats = GetComponent<CharacterStatsScript>();
        attackScript = GetComponentInChildren<AttackScript>();
        
    }

    void FixedUpdate()
    {

        if (characterStats.IsAlive() && canMove && moveInput != Vector2.zero)
        {
            bool success = TryMove(moveInput);

            if (!success)
            {
                success = TryMove(new Vector2(moveInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, moveInput.y));
                }
            }

        }

        if (enteringNewRoom)
        {
            MoveToCenterOfRoom();
        }


        anglePos = getMappedAngle();


    }

    public float getMappedAngle()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Map the angle to four directions (up, down, left, right)
        return MapAngleToFourDirections(angle);
    }

    private float MapAngleToFourDirections(float angle)
    {
        if (angle < 45f && angle >= -45f)
        {
            lookDirection = LookDirection.Right;
            return 0f; // Right
        }
        else if (angle >= 45f && angle < 135f)
        {
            lookDirection = LookDirection.Up;
            return 1f; // Up
        }
        else if (angle >= 135f || angle < -135f)
        {
            lookDirection = LookDirection.Left;
            return 2f; // Left
        }
        else
        {
            lookDirection = LookDirection.Down;
            return 3f; // Down
        }
    }

    //public LookDirection AngleToVectorDirection(Transform transform)
    //{
    //    float angle = GetMousePosition(transform);

    //    if (angle >= 67.5 && angle < 157.5)
    //    {
    //        anglePos = 0f;
    //        return LookDirection.Up;
    //    }
    //    else if (angle >= 157.5 && angle < 247.5)
    //    {
    //        anglePos = .25f;
    //        return LookDirection.Left;
    //    }
    //    else if (angle >= 247.5 && angle < 337.5)
    //    {
    //        anglePos = .75f;
    //        return LookDirection.Down;
    //    }
    //    else if (angle >= 337.5 || angle < 67.5)
    //    {
    //        anglePos = 1f;
    //        return LookDirection.Right;
    //    }
    //    else return LookDirection.Down;

    //}

    public float GetAngle()
    {
        return anglePos;
    }


    //private void Direction()
    //{
    //    if (canMove && moveInput != Vector2.zero)
    //    {


    //        if (moveInput.x > 0)
    //        {
    //            lookDirection = LookDirection.Right;
    //        }
    //        else if (moveInput.x < 0)
    //        {
    //            lookDirection = LookDirection.Left;
    //        }
    //    }
    //}

    public LookDirection GetDirection()
    {
        return lookDirection;
    }

    private bool TryMove(Vector2 direction)
    {
        moveSpeed = characterStats.GetCurrentMoveSpeed();
        int count = rb.Cast(moveInput, moveFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + offsetCollision);

        if (count == 0)
        {
            rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Attack()
    {
        attackScript.Attack(lookDirection);
    }

    public Vector2 GetInput()
    {
        return moveInput;
    }

    void OnMove(InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (!attackScript.IsAttacking())
        {
            Attack();
        }
        


    }

    void OnSpecial()
    {
        print("Special");
    }

    public bool IsAttacking()
    {
        return attackScript.IsAttacking();
    }



    //public static float GetMousePosition(Transform transform)
    //{
    //    Vector3 mousePosition = GetMouseVector(transform);
    //    float angleRadius = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x);
    //    float angle = (180 / Mathf.PI) * angleRadius;
    //    angle = (angle < 0) ? angle + 360 : angle;
    //    return angle;
    //}

    //public static Vector3 GetMouseVector(Transform transform)
    //{
    //    float cameraDistance = Camera.main.transform.position.y - transform.position.y;
    //    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
    //    return mousePosition;
    //}

    public Vector3 GetVectorDirection()
    {
        return mouseVector;
    }

    public Vector3 GetVectorDirectionNormalized()
    {
        return mouseVector.normalized;
    }


    public void OnAttackTriggerEntered(Collider other)
    {
        print("");
    }

    public void SetCenterOfRoom(Vector2 roomCenter)
    {

        print("room center mag" + roomCenter.magnitude);
        print("player pos mag" + rb.position.magnitude);
        newRoomcenter = roomCenter;
        canMove = false;
        enteringNewRoom = true;
    }

    public void MoveToCenterOfRoom()
    {

        if (rb.position.magnitude != newRoomcenter.magnitude)
        {
            rb.transform.position = Vector2.MoveTowards(transform.position, newRoomcenter, moveSpeed * Time.fixedDeltaTime);
        }
        else{ 
            canMove = true;
            enteringNewRoom = false;
        }

    }



}
