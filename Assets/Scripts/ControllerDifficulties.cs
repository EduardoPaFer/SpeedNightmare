using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDifficulties : CharacterSpawner
{
    public float moveSpeed = 20f;
    public float jumpForce = 40f;
    public float wallJumpForce = 100f;
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
        animator = GetComponent<Animator>();
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

        // Jumping & Wall mechanics
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);                
        }
        
        if(WallCollision.collider != null)
        {
            Debug.Log("isWall");
            StatsForWall();
            if(Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                rb.AddForce(WallCollision.normal * wallJumpForce, ForceMode2D.Impulse);
            }
            
        }
        else
        {
            StatsForGround();
        }

        //Check if the character is moving to change animations
        if (moveInput > 0 || moveInput < 0)
        {
            animator.SetBool("IsMoving", true);

        }else animator.SetBool("IsMoving", false);


    }

    private void StatsForWall()
    {
        rb.gravityScale = 5;
        moveSpeed = 2f;
        jumpForce = 20f;
    }
    private void StatsForGround()
    {
        rb.gravityScale = 15;
        moveSpeed = 20f;
        jumpForce = 60f;
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    private IEnumerator WaiterForGravity()
    {
        yield return new WaitForSeconds(1);
        rb.gravityScale = 20;
        yield return new WaitForEndOfFrame();
        StatsForWall();
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
