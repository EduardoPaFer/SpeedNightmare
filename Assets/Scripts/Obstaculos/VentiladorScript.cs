using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentiladorScript : MonoBehaviour
{
    public Rigidbody2D catRb2D;
    float realGravity = 10f;
    float fanForce = -5f;
    bool isFanSpinning = false;

    void Update ()
    {
        if (isFanSpinning)
        {
            catRb2D.gravityScale = fanForce;
        }
        if (!isFanSpinning) 
        {
            catRb2D.gravityScale = realGravity;
        }
    }

    private void OnTriggerStay2D (Collider2D collider)
    {
        if (collider.tag == "Ventilador")
        {
            isFanSpinning = true;
        }
    }
    private void OnTriggerExit2D (Collider2D collider) {
        if(collider.tag == "Ventilador") 
        {
            isFanSpinning = false;
        }
    }    
}
