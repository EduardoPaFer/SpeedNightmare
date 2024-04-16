using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDifficulties : CharacterSpawner
{
    public float moveSpeed = 20f;
    public float jumpForce = 40f;
    public float wallJumpForce = 20f;
    public float gravity;
    public Transform groundCheck;
    public float groundCheckDistance = 1f;
    public float wallCheckDistance = 1f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public Camera mainCam;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isNextToWall;
    private bool facingRight = true;

    [Header("Animacion")]
    private Animator animator;
    bool isMoving;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
        //mainCam.orthographicSize = 2;

        animator = GetComponent<Animator>();

    }

    private void Update()
    {        
        // Check if the player is grounded using raycasting
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

        // Check if the player is next to a wall
        RaycastHit2D wall1 = Physics2D.Raycast(groundCheck.position, Vector2.right, wallCheckDistance, wallLayer);
        RaycastHit2D wall2 = Physics2D.Raycast(groundCheck.position, Vector2.left, wallCheckDistance, wallLayer);
        isNextToWall = wall1.collider != null;
        isNextToWall = wall2.collider != null;
        
        // Horizontal movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip character if needed
        if ((moveInput > 0 && !facingRight) || (moveInput < 0 && facingRight))
        {
            Flip();
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        }
        if (isNextToWall)
        {
            Debug.Log("isWall");
            moveSpeed = 2f;
            rb.gravityScale = 0.2f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //if(facingRight)
                //{

                //}
                gravity = 10;
                rb.velocity = new Vector2(rb.velocity.x, wallJumpForce);
                moveSpeed = 20f;
                jumpForce = 60f;
                wallJumpForce = 20f;
            }

        }
        else
        {
            moveSpeed = 20f;
            rb.gravityScale = 10f;
        }
        jumpForce = 40;

        //Check if the character is moving to change animations
        if (moveInput > 0 || moveInput < 0)
        {
            animator.SetBool("IsMoving", true);

        }else animator.SetBool("IsMoving", false);

        


    }

    private void FixedUpdate()
    {
        //Check if the character is jumping
        animator.SetBool("IsGrounded", isGrounded);
    }



    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObject.FindGameObjectWithTag("KillSwitch"))
        {
            isAlive = false;
            Debug.Log("Died");
            Destroy(gameObject);
        }
        if (GameObject.FindGameObjectWithTag("CheckPoint"))
        {
            GameObject checkPoint;
            Debug.Log("Got a checkpoint");
            checkPoint = GameObject.FindGameObjectWithTag("CheckPoint");
            spawn = checkPoint.transform;
        }
    }

}
