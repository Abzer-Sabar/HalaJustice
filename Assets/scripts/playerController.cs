using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float jumpForce = 16f;
    public float groundCheckRadius = 0.3f;
    public float airDrag = 0.95f;
    public float airMovement = 10f;
    public float jumPHeightMultiplier = 0.5f;
    public float dashTime;
    public float dashSpeedAir, dashSpeedGround;
    public float distanceBtwImages;
    public float dashCoolDown;
    public float knockBackDuration;
    public float jumpTimerSet = 0.15f;
    public int amountOfJumps;

    public Vector2 knockbackSpeed;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public afterImagePool afterImage;

    private Rigidbody2D rb;
    private Animator animator;

    private float moveInputDirection;
    private float dashTimeLeft;
    private float lastImageXPos;
    private float lastDash = -100f;
    private float knockBackStartTime;
    private float jumpTimer;
    private int facingDirection = 1;
    private int amountOfJumpsLeft;

    private bool isFacingRight = true;
    private bool isWalking;
    private bool isGrounded;
    private bool canJump;
    private bool isDashing;
    private bool canMove;
    private bool canFlip;
    private bool knockBack;
    private bool isAttemptingToJump;
    private bool checkJumpMultuplier;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
    }

    private void Update()
    {
        checkInput();
        checkMoveDirection();
        updateAnimations();
        checkIfCanJump();
        checkDash();
        checkKnockBack();
        checkJump();
    }

    private void FixedUpdate()
    {
        applyMovement();
        checkSurroudings();
    }
    private void checkInput()
    {
        moveInputDirection = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(moveInputDirection) > 0.1f)
        {
            canMove = true;
            canFlip = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || amountOfJumps > 0)
            {
                normalJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }
        }
        if (checkJumpMultuplier && !Input.GetButton("Jump"))
        {
            checkJumpMultuplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumPHeightMultiplier);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Time.time >= (lastDash + dashCoolDown))
                tryToDash();
        }
    }

    private void tryToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        afterImagePool.Instance.GetFromPool();
        lastImageXPos = transform.position.x;
    }

    private void checkDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0 && isGrounded == true)
            {
                canMove = false;
                canFlip = false;
                rb.velocity = new Vector2(dashSpeedGround * facingDirection, 0.0f);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXPos) > distanceBtwImages)
                {
                    afterImagePool.Instance.GetFromPool();
                    lastImageXPos = transform.position.x;
                }
            }
            else
            {
                if (dashTimeLeft > 0 && isGrounded == false)
                {
                    canMove = false;
                    canFlip = false;
                    rb.velocity = new Vector2(dashSpeedAir * facingDirection, 0.0f);
                    dashTimeLeft -= Time.deltaTime;

                    if (Mathf.Abs(transform.position.x - lastImageXPos) > distanceBtwImages)
                    {
                        afterImagePool.Instance.GetFromPool();
                        lastImageXPos = transform.position.x;
                    }
                }
            }

            if (dashTimeLeft <= 0)
            {
                isDashing = false;
                canMove = true;
                canFlip = true;
            }

            /*if (Input.GetKey(KeyCode.D))
            {
                dashRight();
            }else if (Input.GetKey(KeyCode.A))
            {
                dashLeft();
            }

            if (dashTimeLeft <= 0)
            {
                isDashing = false;
                canMove = true;
                canFlip = true;
            }*/

        }


    }

    private void dashRight()
    {
        if (dashTimeLeft > 0 && isGrounded == true)
        {
            canMove = false;
            canFlip = false;
            rb.velocity = new Vector2(dashSpeedGround * 1, 0.0f);
            dashTimeLeft -= Time.deltaTime;

            if (Mathf.Abs(transform.position.x - lastImageXPos) > distanceBtwImages)
            {
                afterImagePool.Instance.GetFromPool();
                lastImageXPos = transform.position.x;
            }
        }
        else
        {
            if (dashTimeLeft > 0 && isGrounded == false)
            {
                canMove = false;
                canFlip = false;
                rb.velocity = new Vector2(dashSpeedAir * 1, 0.0f);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXPos) > distanceBtwImages)
                {
                    afterImagePool.Instance.GetFromPool();
                    lastImageXPos = transform.position.x;
                }
            }
        }
    }

    private void dashLeft()
    {
        if (dashTimeLeft > 0 && isGrounded == true)
        {
            canMove = false;
            canFlip = false;
            rb.velocity = new Vector2(dashSpeedGround * -1, 0.0f);
            dashTimeLeft -= Time.deltaTime;

            if (Mathf.Abs(transform.position.x - lastImageXPos) > distanceBtwImages)
            {
                afterImagePool.Instance.GetFromPool();
                lastImageXPos = transform.position.x;
            }
        }
        else
        {
            if (dashTimeLeft > 0 && isGrounded == false)
            {
                canMove = false;
                canFlip = false;
                rb.velocity = new Vector2(dashSpeedAir * -1, 0.0f);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXPos) > distanceBtwImages)
                {
                    afterImagePool.Instance.GetFromPool();
                    lastImageXPos = transform.position.x;
                }
            }
        }
    }




    private void applyMovement()
    {
        if (!isGrounded && moveInputDirection == 0 && !knockBack)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDrag, rb.velocity.y);
        }
        else if(!isGrounded && moveInputDirection != 0)
        {
            rb.velocity = new Vector2(moveSpeed * moveInputDirection, rb.velocity.y);
        }
        /* else if(!isGrounded && moveInputDirection != 0) 
         {
             Vector2 forceToAdd = new Vector2(airMovement * moveInputDirection, 0);
             rb.AddForce(forceToAdd);

            if (Mathf.Abs(rb.velocity.x) > moveSpeed)
            {
                rb.velocity = new Vector2(moveSpeed * moveInputDirection, rb.velocity.y);
            }
         }*/

        else if (canMove && !knockBack)
        {
            rb.velocity = new Vector2(moveSpeed * moveInputDirection, rb.velocity.y);
        }

    }

    private void checkMoveDirection()
    {
        if (isFacingRight && moveInputDirection < 0)
        {
            flip();
        }
        else if (!isFacingRight && moveInputDirection > 0)
        {
            flip();
        }
        if (Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    private void checkJump()
    {
        if (jumpTimer > 0)
        {
            if (isGrounded)
            {
                normalJump();
            }
        }
        if (isAttemptingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }
    }

    private void normalJump()
    {
        if (canJump && !knockBack)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultuplier = true;
        }
    }
    private void flip()
    {
        if (canFlip)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);

        }
    }

    public void enableFlip()
    {
        canFlip = true;
    }

    public void disableFlip()
    {
        canFlip = false;
    }

    private void updateAnimations()
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    private void checkSurroudings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void checkIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0.01f)
        {
            amountOfJumpsLeft = amountOfJumps;
        }
        if (amountOfJumpsLeft <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
    }

    public void applyKnockBack(int direction)
    {
        knockBack = true;
        knockBackStartTime = Time.time;

        rb.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }

    private void checkKnockBack()
    {
        if (Time.time >= knockBackStartTime + knockBackDuration && knockBack)
        {
            knockBack = false;
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    public bool getDashStatus()
    {
        return isDashing;
    }


    //player movement powerup
    public void applyMovementPowerup(float speed)
    {
        this.moveSpeed = speed;
    }

    public void revertMovement()
    {
        this.moveSpeed = 7f;
    }

}
