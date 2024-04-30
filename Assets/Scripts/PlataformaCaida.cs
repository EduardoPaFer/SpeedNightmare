using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlataformaCaida : MonoBehaviour
{
    // Start is called before the first frame update
    float groundCheckDistance = 1f;
    float groundCheckTimeDistance = 1f;
    public LayerMask breakableGround;
    public LayerMask ground;
    int life = 2;
    public BoxCollider2D Ground;
    public SpriteRenderer groundSprite;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, breakableGround))
        {
            life = life - 1;
            Debug.Log("-1 hp ground");
            groundCheckDistance = -1f;
        }
        if (life == 0)
        {
            StartCoroutine(Break());
            life = 2;
        }
        if (Physics2D.Raycast(transform.position, Vector2.down, groundCheckTimeDistance, ground))
        {
            groundCheckDistance = 1f;
        }

    }
    IEnumerator Break()
    {
        Debug.Log("breaking...");
        yield return new WaitForSecondsRealtime(2);
        Ground.enabled = false;
        groundSprite.enabled = false;
        StartCoroutine(SpawnBack());
    }
    IEnumerator SpawnBack()
    {
        Debug.Log("Respawning platform...");
        yield return new WaitForSecondsRealtime(5);
        Ground.enabled = true;
        groundSprite.enabled = true;
    }
}
