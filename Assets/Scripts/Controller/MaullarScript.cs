using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaullarScript : MonoBehaviour
{

    private AudioSource audioSource;

    public AudioClip[] maullidos;
     
    

    

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
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
