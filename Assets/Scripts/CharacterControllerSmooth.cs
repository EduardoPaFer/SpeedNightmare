using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerSmooth : MonoBehaviour
{
    public float movementSpeed;
    Rigidbody2D rb;
    public bool grounded;

    //Jump
    public float jumpForce;
    Vector2 jump;
    public Collider2D groundDetect;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jump = new Vector2(0, jumpForce);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 hVelocidad = new Vector2(Input.GetAxisRaw("Horizontal"), 0).normalized;
        rb.velocity = hVelocidad * movementSpeed;


        if (grounded && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(jump, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObject.FindWithTag("jumpCollision"))
        {
            grounded = true;

        }
        else
        {
            grounded = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameObject.FindWithTag("jumpCollision"))
        {
            grounded = false;

        }
    }
}
