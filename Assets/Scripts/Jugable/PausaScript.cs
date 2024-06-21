using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaScript : MonoBehaviour
{
    public ControllerDef characterController;
    public GameObject menuPausa, mouse, menuP;
    public SceneManager sceneManager;
    private enum GameState
    {
        Pause,
        Running
    }

    private GameState actualState = GameState.Running;


    void Start()
    {        
        CambiarState(GameState.Running);
        characterController.enabled = true;
        Cursor.visible = false;
        menuPausa.SetActive(false);
        mouse.SetActive(false);
        menuP.SetActive(false);
    }


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
                menuP.SetActive(false); 
               
                break;

            case GameState.Pause:
                Time.timeScale = 0;
                characterController.enabled = false;
                Cursor.visible = true;
                menuPausa.SetActive(true);
                mouse.SetActive(true);
                menuP.SetActive(true);  
                
                break;
        }
    }

    public void Salir()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    
}
