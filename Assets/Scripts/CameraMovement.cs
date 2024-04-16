using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Vector3 offset = new Vector3 (0f, 0f, -10f);
    public float delayMovement = 0.25f;
    public Vector3 speed = Vector3.zero;

    public   Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 characterPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, characterPos, ref speed, delayMovement);
    }
}
