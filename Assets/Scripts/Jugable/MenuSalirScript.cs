using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSalirScript : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        gameObject.transform.position = mousePos;
    }

}
