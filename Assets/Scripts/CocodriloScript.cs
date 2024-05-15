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
    
    public GameObject cocodriloCollider;
    public CircleCollider2D cocodriloDetector;

    private bool playerClose = false;
    private bool breakAtack = false;
    // Start is called before the first frame update
    void Start()
    {
        cocodriloCollider.SetActive(false);
        CambiarState(CocodriloState.Wait);
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
        
    }


    //States cambios
    private void CambiarState(CocodriloState nuevoState)
    {
        if (nuevoState == actualState)
        {
            return;
        }
        actualState = nuevoState;
        ElegirState();
        Debug.Log("CambiadoState");
    }

    private void ElegirState()
    {
        switch (actualState)
        {
            case CocodriloState.Attacking:
                Debug.Log("ElegirState: Attacking");
                StartCoroutine(CocodriloTimer());
                break;
            case CocodriloState.Wait:
                Debug.Log("ElegirState: Wait");
                breakAtack = true;
                cocodriloCollider.SetActive(false);
                break;
        }
    }
    IEnumerator CocodriloTimer()
    {
        yield return new WaitForSecondsRealtime(3);
        StartCoroutine(MordidaCocodrilo());
    }
    IEnumerator MordidaCocodrilo()
    {
        cocodriloCollider.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        cocodriloCollider.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            playerClose = true;
            Debug.Log("InRange");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            playerClose = false;
            Debug.Log("OutOfRange");
        }
    }
}
