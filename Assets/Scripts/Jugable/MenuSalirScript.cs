using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSalirScript : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        gameObject.transform.position = mousePos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boton" && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);
        }
    }

}
