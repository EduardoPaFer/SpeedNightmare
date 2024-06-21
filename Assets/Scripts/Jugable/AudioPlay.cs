using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    AudioSource audioSource;
    void Update()
    {
        audioSource.Play();
        Destroy(audioSource);
    }
}
