using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicesScript : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.rotation = _rigidbody2D.rotation + 0.2f;
    }
}
