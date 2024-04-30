using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentiladorScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D catRb2D;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
            //catRb2D.AddForce(airDirection * airForce, ForceMode2D.Force);               CAMBIAR GRAVEDAD CUANDO ENTRA EN TRIGGER :))))
    }

    private void OnTriggerEnter2D (Collider2D collider)
    {
    }
}
