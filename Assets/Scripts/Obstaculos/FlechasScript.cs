using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class FlechasScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    public bool derecha;
    public bool izquierda;
    public bool abajo;
    public bool arriba;
    public GameObject flecha;
    public Transform dispensador;
    private bool shooting = false;
    void Start()
    {

    }

    // Update is called once per frame
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
