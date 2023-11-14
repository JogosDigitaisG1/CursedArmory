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


    private Rigidbody2D rb;
    private Vector2 moveInput;
    public float moveSpeed = 1f;

    public bool canMove = true;

    

    public ContactFilter2D moveFilter;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float offsetCollision;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackScript = GetComponentInChildren<AttackScript>();
        print(rb);
    }

    void FixedUpdate()
    {

        if (canMove && moveInput != Vector2.zero)
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

        Direction();

    }

    private void Direction()
    {
        if (canMove && moveInput != Vector2.zero)
        {


            if (moveInput.x > 0)
            {
                lookDirection = LookDirection.Right;
            }
            else if (moveInput.x < 0)
            {
                lookDirection = LookDirection.Left;
            }
        }
    }

    public LookDirection GetDirection()
    {
        return lookDirection;
    }

    private bool TryMove(Vector2 direction)
    {
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

}
