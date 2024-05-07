using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlataformaCaida : MonoBehaviour
{
    // Start is called before the first frame update
    int life = 2;
    public BoxCollider2D Ground;
    public SpriteRenderer groundSprite;
    private bool recentlyDamaged = false;
    void Start()
    {
        groundSprite.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (life == 0)
        {
            StartCoroutine(Break());
            life = 2;
        }
    }
    IEnumerator Break()
    {
        Debug.Log("breaking...");
        groundSprite.color = Color.red;
        yield return new WaitForSecondsRealtime(2);
        Ground.enabled = false;
        groundSprite.enabled = false;
        StartCoroutine(SpawnBack());
    }
    IEnumerator SpawnBack()
    {
        Debug.Log("Respawning platform...");
        yield return new WaitForSecondsRealtime(5);
        Debug.Log("Respawned ground" + life);
        Ground.enabled = true;
        groundSprite.enabled = true;
        groundSprite.color = Color.white;
    }
    IEnumerator FinishDamage()
    {
        yield return new WaitForEndOfFrame();
        recentlyDamaged = false;
    }
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            if (!recentlyDamaged)
            {
                life = life - 1;
                Debug.Log("-1 hp ground" + life);
                groundSprite.color = Color.yellow;
            }
            recentlyDamaged = true;
            StartCoroutine(FinishDamage());
        }
    }
}
