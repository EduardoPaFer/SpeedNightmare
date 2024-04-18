using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDef : MonoBehaviour
{
    public float GroundedMovementSpeed = 20f;
    public float jumpForce = 25f;
    public float wallJumpForce = 100f;
    public float WallGravityMultiplier;
    public float groundCheckDistance = 1f;
    public float wallCheckDistance = 1f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    private Rigidbody2D _rigidbody2D;
    private bool isGrounded;
    private bool facingRight = true;


    [Header("Animacion")]
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if ground or wall
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

        RaycastHit2D WallCollision = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance, wallLayer);
        if(WallCollision.collider == null)
        {
            WallCollision = Physics2D.Raycast(transform.position, Vector2.left, wallCheckDistance, wallLayer);
        }
        
        float moveInput = Input.GetAxisRaw("Horizontal");
        _rigidbody2D.position += ((Vector2.right * moveInput * GroundedMovementSpeed) + Vector2.up * _rigidbody2D.velocity.y) * Time.deltaTime;

        if ((moveInput > 0 && !facingRight) || (moveInput < 0 && facingRight))
        {
            Flip();
        }

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

        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || WallCollision.collider != null))
        {
            Vector2 L_jumpDirection = Vector2.up;
            float CurrentJumpForce = jumpForce;

            if(WallCollision.collider != null)
            { 
                _rigidbody2D.gravityScale = 30;
                jumpForce = wallJumpForce;
                StatsForWall();
                L_jumpDirection += WallCollision.normal;
                L_jumpDirection.Normalize();
            }            
            Debug.DrawRay(transform.position,  L_jumpDirection * 10.0f, Color.red, 0.1f);
            _rigidbody2D.AddForce(L_jumpDirection * CurrentJumpForce, ForceMode2D.Impulse);
        }

        if (moveInput > 0 || moveInput < 0)
        {
            animator.SetBool("IsMoving", true);
        }else 
        {
            animator.SetBool("IsMoving", false);
        }

        animator.SetBool("IsGrounded", isGrounded);
    }
    
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    private void StatsForWall()
    {
        Debug.Log("Wall");
        _rigidbody2D.gravityScale = 5;
        GroundedMovementSpeed = 2;
        StartCoroutine(TimerStats());
    }
    IEnumerator TimerStats()
    {
        Debug.Log("Timer...");
        yield return new WaitForSeconds(1); 
        StatsForGround();
    }
    private void StatsForGround()
    {
        Debug.Log("Ground");
        _rigidbody2D.gravityScale = 10;
        GroundedMovementSpeed = 20f;
        jumpForce = 25f;
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
            //spawn = checkPoint.transform;
        }
    }

}
