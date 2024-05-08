using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlechaMovement : FlechasScript
{
    // Start is called before the first frame update
    private Rigidbody2D flechaRb2D;
    public float velocidad;
    void Start()
    {
        flechaRb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direccionFlecha = Vector2.right;
        if (arriba)
        {
            direccionFlecha = Vector2.up;
        }
        if (abajo)
        {
            direccionFlecha = Vector2.down;
        }
        if (izquierda)
        {
            direccionFlecha = Vector2.left;
        }
        if (derecha)
        {
            direccionFlecha = Vector2.right;
        }

        flechaRb2D.position += direccionFlecha * velocidad * 0.002f;
        StartCoroutine(Delete());
    }
    IEnumerator Delete()
    {
        yield return new WaitForSecondsRealtime(10);
        Destroy(gameObject);
    }
}
