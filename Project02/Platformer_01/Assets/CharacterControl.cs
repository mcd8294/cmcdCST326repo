using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public Rigidbody rb;
    public Collider charCollider;
    public bool isGrounded = false;
    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        charCollider = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter(moveSpeed);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(rb.transform.up * jumpForce);
        }
    }

    void MoveCharacter(float speed)
    {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0f, 0f);
    }
}
