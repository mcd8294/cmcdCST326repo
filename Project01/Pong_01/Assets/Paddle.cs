using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody rb;
    public Vector3 movement;
    public KeyCode upKey;
    public KeyCode downKey;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(upKey))
        {
            movement = new Vector3(0,0, 1);
        }
        else if (Input.GetKeyUp(upKey))
        {
            movement = new Vector3(0, 0, 0);
        }
        if (Input.GetKey(downKey))
        {
            movement = new Vector3(0,0, -1);
        }
        else if (Input.GetKeyUp(downKey))
        {
            movement = new Vector3(0, 0, 0);
        }
    }

    void MoveCharacter(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }
    
    private void FixedUpdate()
    {
        MoveCharacter(movement);
    }
}
