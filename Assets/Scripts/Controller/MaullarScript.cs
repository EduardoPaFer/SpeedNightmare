using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaullarScript : MonoBehaviour
{

    private AudioSource audioSource;

    public AudioClip[] maullidos;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();        
    }

    void Update()
    {        
        if(Input.GetKeyDown(KeyCode.M)) 
        {
            if (maullidos.Length > 0)
            {                
                int randomIndex = UnityEngine.Random.Range(0, maullidos.Length);
                AudioClip randomClip = maullidos[randomIndex];
                
                audioSource.clip = randomClip;
                audioSource.Play();
            }
        }
    }
}
