using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float speed = 10f;
    private float offset;

    public GameObject subject;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position.z - subject.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //MoveCamera(new Vector2(Input.GetAxis("Horizontal"), 0f));
        transform.position = new Vector3(subject.transform.position.x,transform.position.y,offset);
    }

    void MoveCamera(Vector2 direction)
    {
        transform.Translate(speed*Time.deltaTime*direction);
    }
}
