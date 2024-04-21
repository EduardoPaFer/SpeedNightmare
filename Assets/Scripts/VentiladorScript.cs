using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentiladorScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D catRb2D;
    public float airForce;
    Vector2 airDirection = Vector2.up;
    bool onAir = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (onAir)
        {
            catRb2D.AddForce(airDirection * airForce, ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D (Collider2D collider)
    {
        if (GameObject.FindGameObjectWithTag("Character"))
        {
            onAir = true;
        }
        else
        {
            onAir = false;
        }
    }
}
