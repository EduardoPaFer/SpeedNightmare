using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class FlechasScript : MonoBehaviour
{
    
    public GameObject flecha;
    public Transform dispensador;
    private bool shooting = false;

    void Update()
    {
        if (!shooting)
        {
            StartCoroutine(DisparaFlecha());
        }

        
    }
    IEnumerator DisparaFlecha()
    {
        shooting = true;
        Instantiate(flecha, dispensador);
        yield return new WaitForSecondsRealtime(3);
        StartCoroutine(Cooldown());
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSecondsRealtime(2);
        shooting = false;
    }
}
