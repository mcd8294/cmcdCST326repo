using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public GameObject ball;
    public Vector3 direction = new Vector3(-1,0,0);

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.AddForce(direction*1000f, ForceMode.Acceleration);


    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
