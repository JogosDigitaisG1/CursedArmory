using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerScript : MonoBehaviour
{


    private Rigidbody2D rb;
    private Vector2 moveInput;
    public float moveSpeed = 1f;
    public bool canMove = true;

    private bool attacking = false;

    public ContactFilter2D moveFilter;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float offsetCollision;

    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        print(rb);
    }

    void FixedUpdate()
    {
        
        if(moveInput != Vector2.zero && canMove)
        {
           bool success =  TryMove(moveInput);

            if (!success)
            {
                success = TryMove(new Vector2(moveInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, moveInput.y));
                }
            }

        }

        


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
        attacking = value.isPressed;
    }

    void OnSpecial()
    {
        print("Special");
    }

    public bool IsAttacking()
    {
        return attacking;
    }
}
