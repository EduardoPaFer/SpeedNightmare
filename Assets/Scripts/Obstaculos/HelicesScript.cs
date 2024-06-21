using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicesScript : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    public float rotationValue;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        _rigidbody2D.rotation = _rigidbody2D.rotation + rotationValue;
    }
}
