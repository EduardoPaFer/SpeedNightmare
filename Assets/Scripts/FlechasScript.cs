using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class FlechasScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject flecha;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DisparaFlecha());
        
    }
    IEnumerator DisparaFlecha()
    {
        yield return new WaitForSecondsRealtime(3);
        Instantiate(flecha);
    }
}
