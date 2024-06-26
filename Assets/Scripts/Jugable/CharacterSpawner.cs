using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer playerSprite;
    public Transform spawn;
    public bool isAlive;
    public ControllerDef controller;
    private AudioSource audioSource;
    public AudioClip sonido;
    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        isAlive = true;
        audioSource = GetComponent<AudioSource>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "KillSwitch")
        {
            isAlive = false;
            Debug.Log("Died");
            controller.enabled = false;
            playerSprite.enabled = false;
            StartCoroutine(RespawnDelay());

            audioSource.clip = sonido;
            audioSource.Play();

        }
    }
    IEnumerator RespawnDelay()
    {
        if(isAlive)
        {
            yield return new WaitForEndOfFrame();
        }
        else
        {
            Debug.Log("Respawning...");
            yield return new WaitForSecondsRealtime(1);
            controller.enabled = true;  
            gameObject.transform.position = spawn.transform.position;
            playerSprite.enabled = true;
        }
    }
}

