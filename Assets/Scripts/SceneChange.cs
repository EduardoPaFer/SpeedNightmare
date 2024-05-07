using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int currentScene = 0;

        if (collision.gameObject.tag == "Character")
        {
            SceneManager.LoadScene(currentScene + 1);
        }
    }

    

}
