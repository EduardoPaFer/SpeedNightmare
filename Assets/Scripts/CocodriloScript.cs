using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CocodriloScript : MonoBehaviour
{

    private enum CocodriloState
    {
        Wait,
        Attacking
    }

    private CocodriloState actualState = CocodriloState.Wait;
    
    public BoxCollider2D cocodriloCollider;
    public CircleCollider2D cocodriloDetector;

    private bool playerClose = false;
    private bool breakAtack = false;
    // Start is called before the first frame update
    void Start()
    {
        cocodriloCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerClose)
        {
            CambiarState(CocodriloState.Attacking);
        }
        else
        {
            CambiarState(CocodriloState.Wait);
        }
        StartCoroutine(CocodriloTimer());
    }


    //States cambios
    private void CambiarState(CocodriloState nuevoState)
    {
        if (nuevoState == actualState)
        {
            return;
        }
        BorrarState();
        actualState = nuevoState;
        ElegirState();
    }

    private void ElegirState()
    {
        switch (actualState)
        {
            case CocodriloState.Attacking:
                StartCoroutine(CocodriloTimer());
                break;
            case CocodriloState.Wait:
                breakAtack = true;
                break;
        }
    }
    private void BorrarState()
    {
        switch (actualState)
        {
            case CocodriloState.Attacking:
                breakAtack = true;
                break;
                case CocodriloState.Wait:
                breakAtack = false; 
                break;
        }
    }
    IEnumerator CocodriloTimer()
    {        
        yield return new WaitForSecondsRealtime(1);
        if (breakAtack)
        {
            
        }
        else
        {
            cocodriloCollider.enabled = true;
            StartCoroutine(CocodriloCooldown());
        }
    }
    IEnumerator CocodriloCooldown()
    {
        yield return new WaitForSecondsRealtime(3);
        if(breakAtack)
        {
            
        }
        else
        {
            cocodriloCollider.enabled = false;
            StartCoroutine(CocodriloCooldowns());
        }
    }
    IEnumerator CocodriloCooldowns()
    {
        yield return new WaitForSecondsRealtime(2);
        if (breakAtack)
        {

        }
        else
        {
            StartCoroutine(CocodriloTimer());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            playerClose = true;
        }
    }
}
