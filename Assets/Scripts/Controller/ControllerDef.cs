using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDef : MonoBehaviour
{
    //Movement variables
    public float groundedMovementSpeed = 20f;
    public float jumpForce = 25f;
    public float wallJumpForce = 20f;

    //Checks of distance
    public float groundCheckDistance = 10f;
    public float wallCheckDistance = 1f;

    
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    private Rigidbody2D _rigidbody2D;

    private bool isGrounded;
    private bool isFacingRight = true;
    private bool isInTelaraña = false;

    [Header("Animacion")]
    private Animator animator;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Check if ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

        animator.SetBool("IsGrounded", isGrounded);

        //Check if wall
        RaycastHit2D WallCollision = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance, wallLayer);
        if(WallCollision.collider == null)
        {
            WallCollision = Physics2D.Raycast(transform.position, Vector2.left, wallCheckDistance, wallLayer);
        }
        
        float moveInput = Input.GetAxisRaw("Horizontal");
        _rigidbody2D.position += ((Vector2.right * moveInput * groundedMovementSpeed) + Vector2.up * _rigidbody2D.velocity.y) * Time.deltaTime;
        
        if ((moveInput > 0 && !isFacingRight) || (moveInput < 0 && isFacingRight))
        {
            Flip();
        }
#if UNITY_EDITOR

        //Collision detection debug
        Vector2 MoveDirectionL = Vector2.left;
        Vector2 MoveDirectionR = Vector2.right;
        if(WallCollision.collider != null)
        {
            Debug.DrawRay(transform.position,  MoveDirectionL * 2.0f, Color.green, 0.1f);
            Debug.DrawRay(transform.position,  MoveDirectionR * 2.0f, Color.green, 0.1f);
            
        }
        else
        {
            Debug.DrawRay(transform.position,  MoveDirectionL * 2.0f, Color.red, 0.1f);
            Debug.DrawRay(transform.position,  MoveDirectionR * 2.0f, Color.red, 0.1f);
        }
#endif

        //Jumping and seeing if is near a wall too
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && (isGrounded || WallCollision.collider != null))
        {
            Vector2 L_jumpDirection = Vector2.up;
            float CurrentJumpForce = wallJumpForce;

            if(WallCollision.collider != null)
            {
                int movingAgainstWall = 1;
                _rigidbody2D.gravityScale = 30;
                StatsForWall();                
                L_jumpDirection += WallCollision.normal;
                if (moveInput > 0 || moveInput < 0)
                {
                    movingAgainstWall = 2;
                }
                _rigidbody2D.AddForce(L_jumpDirection * CurrentJumpForce * movingAgainstWall, ForceMode2D.Impulse);
                StartCoroutine(TimerStats());
            }            
            else
            {
                _rigidbody2D.AddForce(L_jumpDirection * CurrentJumpForce, ForceMode2D.Impulse);
            }
#if UNITY_EDITOR
            Debug.DrawRay(transform.position,  L_jumpDirection * 10.0f, Color.red, 0.1f);
#endif
        }


        if (moveInput > 0 || moveInput < 0)
        {
            animator.SetBool("IsMoving", true);
        }else 
        {
            animator.SetBool("IsMoving", false);
        }

        if (isInTelaraña)
        {
            groundedMovementSpeed = 10f;
        }
        else
        {
            groundedMovementSpeed = 20f;
        }
    }
    
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    private void StatsForWall()
    {
        Debug.Log("Wall");
        _rigidbody2D.gravityScale = 1;
        groundedMovementSpeed = 2;
    }
    IEnumerator TimerStats()
    {
        Debug.Log("Timer...");
        yield return new WaitForSecondsRealtime(1); 
        StatsForGround();
    }
    private void StatsForGround()
    {
        Debug.Log("Ground");
        _rigidbody2D.gravityScale = 10;
        groundedMovementSpeed = 20f;
        jumpForce = 25f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Telaraña")
        {
            isInTelaraña = true;
        }
        if (collision.tag == "Hole")
        {
            animator.SetBool("IsFalling", true);            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Telaraña")
        {
            isInTelaraña = false;
        }
        if(collision.tag == "Hole")
        {
            animator.SetBool("IsFalling", false);
        }
    }
}
