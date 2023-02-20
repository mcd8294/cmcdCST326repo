using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera(new Vector2(Input.GetAxis("Horizontal"), 0f));
    }

    void MoveCamera(Vector2 direction)
    {
        transform.Translate(speed*Time.deltaTime*direction);
    }
}
