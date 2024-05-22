using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PausaScript : MonoBehaviour
{
    public ControllerDef characterController;
    public GameObject menuPausa, mouse;
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
        Cursor.visible = false;
        menuPausa.SetActive(false);
        mouse.SetActive(false);
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
                Cursor.visible = false;
                menuPausa.SetActive(false);
                mouse.SetActive(false);
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                characterController.enabled = false;
                Cursor.visible = true;
                menuPausa.SetActive(true);
                mouse.SetActive(true);
                break;
        }
    }
}
