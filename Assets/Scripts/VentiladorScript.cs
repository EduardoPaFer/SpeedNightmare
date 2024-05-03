using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentiladorScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D catRb2D;
    float realGravity = 10f;
    float fuerzaVentilador = -5f;
    bool ventilando = false;

    void Update ()
    {
        if (ventilando)
        {
            catRb2D.gravityScale = fuerzaVentilador;
        }
        if (!ventilando) 
        {
            catRb2D.gravityScale = realGravity;
        }
    }

    private void OnTriggerStay2D (Collider2D collider)
    {
        if (collider.tag == "Ventilador")
        {
            ventilando = true;
        }
    }
    private void OnTriggerExit2D (Collider2D collider) {
        if(collider.tag == "Ventilador") {
            ventilando = false;
        }
    }
    
}
