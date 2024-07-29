using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f, runSpeed = 7f, jumpImpulse = 8f, airWalkSpeed = 4f, timePassed;

    private float coyoteTime = 0.2f, coyoteTimeCounter;
    public bool canMove;
    public bool _isFacingRight = true;
    private bool _isGliding = false;
    private bool _isRunning = false;
    private bool _isMoving = false;
    Rigidbody2D rb;
    Animator animator;
    Vector2 moveInput;
    TouchingDirections touchingDirections;

    public float CurrentMoveSpeed
    { get
        {
            if (IsMoving && !touchingDirections.IsOnWall && canMove)
            {
                if (touchingDirections.IsGrounded)
                {
                    if (IsRunning)
                    {
                        return runSpeed;
                    }
                    else
                    {
                        return walkSpeed;
                    }
                }
                if (IsGliding)
                {
                    return airWalkSpeed + 1;
                }
                else 
                {
                    return airWalkSpeed;
                }

            }
            
            else
            {
                return 0;
                
            }

        } 
    }

    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(Animations.isMoving, value);

        }
    }

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool(Animations.isRunning, value);
        }
    }

    public bool IsGliding 
    {
        get
        {
            return _isGliding;
        }
        set
        {
            _isGliding = value;
            animator.SetBool(Animations.isGliding, value);      
        }
    }

    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            {
                _isFacingRight = value;
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        canMove = true;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        animator.SetFloat(Animations.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            moveInput = context.ReadValue<Vector2>();
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
    }

    private void SetFacingDirection(Vector2 moveInput) 
    {
        if(moveInput.x > 0 && !IsFacingRight) 
        {
            IsFacingRight = true;
        } 
        else if (moveInput.x < 0 && IsFacingRight) 
        {
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            if (context.started)
            {
                IsRunning = true;
            }
            else if (context.canceled)
            {
                IsRunning = false;
            }
        }
    }

/*    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(Animations.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }*/

    public void OnGlide(InputAction.CallbackContext context)
    {
        if (context.started && !touchingDirections.IsGrounded)
        {
            IsGliding = true;
            rb.drag = 2f;
        }
        else if (context.canceled)
        {
            IsGliding = false;
            rb.drag = 0f;
        }
    }

    void Update()
    {
        if (timePassed > 2f )
        {
            StartCoroutine(autoJump());
            timePassed = 0.0f;
        }
        if (touchingDirections.IsGrounded && canMove)
        {
            coyoteTimeCounter = coyoteTime;
            IsGliding = false;
            rb.drag = 0f;
            timePassed += Time.deltaTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private IEnumerator autoJump()
    {
        yield return new WaitForSeconds(0);
        if (coyoteTimeCounter > 0f && canMove)
        {
            animator.SetTrigger(Animations.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            coyoteTimeCounter = 0.0f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.CompareTag("End"))
        {
            animator.SetTrigger(Animations.isDancing);
            canMove = false;

        }
    }
}
