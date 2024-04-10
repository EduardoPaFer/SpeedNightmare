using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Transform spawn;
    public bool isAlive;
    void Start()
    {
        Instantiate(player, spawn);
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive == false)
        {
            Respawn();
        }
    }
    public void Respawn()
    {
        Debug.Log("Respawning...");
        Instantiate(player, spawn);
        isAlive = true;
    }
}
