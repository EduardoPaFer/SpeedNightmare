using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PausaScript : MonoBehaviour
{
    public CharacterSpawner spawner;
    public ControllerDef characterController;
    
    private enum GameState
    {
        Pause,
        Running
    }

    private GameState actualState = GameState.Running;

    // Start is called before the first frame update
    void Start()
    {        
        CambiarState(GameState.Running);
        characterController.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
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
                characterController.enabled = true;
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                characterController.enabled = false;
                break;
        }
    }
}
