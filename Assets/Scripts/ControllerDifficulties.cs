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
<<<<<<< HEAD
=======
        //mainCam.orthographicSize = 2;

        animator = GetComponent<Animator>();

>>>>>>> ccd6bc9af5d71e43022e0b5c0966f4b547997508
    }

    private void Update()
    {        
        // Check if the player is grounded using raycasting
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

        // Check if the player is next to a wall
        RaycastHit2D WallCollision = Physics2D.Raycast(groundCheck.position, Vector2.right, wallCheckDistance, wallLayer);
        if(WallCollision.collider == null)
        {
            WallCollision = Physics2D.Raycast(groundCheck.position, Vector2.left, wallCheckDistance, wallLayer);
        }

        
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

if (Input.GetKeyDown(KeyCode.Space))
            {
        if(WallCollision.collider != null)
        {
            rb.AddForce(WallCollision.normal * jumpForce, ForceMode2D.Impulse);

        }
            Debug.Log("isWall");
            moveSpeed = 2f;
            rb.gravityScale = 0.2f;
            
                
                // gravity = 10;
                // moveSpeed = 20f;
                // jumpForce = 60f;
                // wallJumpForce = 20f;
            }
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
