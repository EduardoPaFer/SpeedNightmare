using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PausaScript : MonoBehaviour
{
    Animator animator;
    public CharacterSpawner spawner;
    
    private enum GameState
    {
        Pause,
        Dead,
        Running
    }

    private GameState actualState = GameState.Running;

    bool playerDead;
    // Start is called before the first frame update
    void Start()
    {
        
        CambiarState(GameState.Running);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!spawner.isAlive)
        {
            CambiarState(GameState.Dead);
            
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            if(actualState == GameState.Running) 
                CambiarState(GameState.Pause);
            else if (actualState == GameState.Pause)
                CambiarState(GameState.Running);
        }
        Debug.Log(Time.timeScale.ToString());
    }


    //States cambios
    private void CambiarState(GameState nuevoState)
    {
        if (nuevoState == actualState)
        {
            return;
        }
        actualState = nuevoState;
        ElegirState();
    }

    private void ElegirState()
    {
        switch (actualState)
        {
            case GameState.Running:
                Time.timeScale = 1;
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                break;
            case GameState.Dead:
                Time.timeScale = 0.5f;
                break;

        }
    }
}
