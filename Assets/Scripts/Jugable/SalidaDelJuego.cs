using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SalidaDelJuego : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            Application.Quit();
        }
    }
}
