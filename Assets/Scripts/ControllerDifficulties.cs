using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDifficulties : MonoBehaviour
{
    public float GroundedMovementSpeed = 20f;
    public float jumpForce = 40f;
    public float wallJumpForce = 100f;
    public float WallGravityMultiplier;
    public float groundCheckDistance = 1f;
    public float wallCheckDistance = 1f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    private Rigidbody2D _rigidbody2D;
    private bool isGrounded;
    private bool facingRight = true;
    private bool isNextToWall;

    [Header("Animacion")]
    private Animator animator;
    bool isMoving;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {        
        // Check if the player is grounded using raycasting
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

        // Check if the player is next to a wall
        RaycastHit2D WallCollision = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance, wallLayer);
        if(WallCollision.collider == null)
        {
            WallCollision = Physics2D.Raycast(transform.position, Vector2.left, wallCheckDistance, wallLayer);
        }

        Debug.DrawRay(transform.position,  Vector3.right * wallCheckDistance, WallCollision.collider != null ? Color.green : Color.magenta, 0.1f);
        Debug.DrawRay(transform.position,  Vector3.left * wallCheckDistance, WallCollision.collider != null ? Color.green : Color.magenta, 0.1f);

        // Horizontal movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        _rigidbody2D.position += ((Vector2.right * moveInput * GroundedMovementSpeed) + Vector2.up * _rigidbody2D.velocity.y) * Time.deltaTime;

        // Flip character if needed
        if ((moveInput > 0 && !facingRight) || (moveInput < 0 && facingRight))
        {
            Flip();
        }

        // Jumping & Wall mechanics
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || WallCollision.collider != null))
        {
            Vector2 L_jumpDirection = Vector2.up;
            float CurrentJumpForce = jumpForce;

            if(WallCollision.collider != null)
            { 
                L_jumpDirection += WallCollision.normal;
                L_jumpDirection.Normalize();
                jumpForce = wallJumpForce;
            }            
            Debug.DrawRay(transform.position,  L_jumpDirection * 10.0f, Color.red, 0.1f);
             _rigidbody2D.AddForce(L_jumpDirection * CurrentJumpForce, ForceMode2D.Impulse);
        }
        
        //Check if the character is moving to change animations
        if (moveInput > 0 || moveInput < 0)
        {
            animator.SetBool("IsMoving", true);

        }else animator.SetBool("IsMoving", false);

        animator.SetBool("IsGrounded", isGrounded);

    }

    private void StatsForWall()
    {
        _rigidbody2D.gravityScale = 5;
        GroundedMovementSpeed = 2f;
        jumpForce = 100f;
    }
    private void StatsForGround()
    {
        _rigidbody2D.gravityScale = 15;
        GroundedMovementSpeed = 20f;
        jumpForce = 40f;
    }
    private IEnumerator WaiterForGravity()
    {
        yield return new WaitForSeconds(1);
        _rigidbody2D.gravityScale = 20;
        yield return new WaitForEndOfFrame();
        StatsForWall();
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
            //isAlive = false;
            Debug.Log("Died");
            Destroy(gameObject);
        }
        if (GameObject.FindGameObjectWithTag("CheckPoint"))
        {
            GameObject checkPoint;
            Debug.Log("Got a checkpoint");
            checkPoint = GameObject.FindGameObjectWithTag("CheckPoint");
//            spawn = checkPoint.transform;
        }
    }

}
