using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterControl : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 3f;
    public float sprintBoost = 3f;
    public float jumpForce = 10f;
    public float jumpBoost = 5f;
    public float groundRange;
    public bool isGrounded;
    public float headRange;
    public bool headTouch;
    public Collider col;
    public Animator charAnimator;

    //Events
    public UnityEvent coinCollect;
    public UnityEvent scored;
    
    private void Start()
    {
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        //Left/Right Movement
        float horizontalAxis = Input.GetAxis("Horizontal");
        Rigidbody rbody = GetComponent<Rigidbody>();
        rbody.velocity += horizontalAxis * Vector3.right * Time.deltaTime * acceleration;
        rbody.AddForce(Vector3.right * horizontalAxis * acceleration, ForceMode.Force);

        float xVelocity = Mathf.Clamp(rbody.velocity.x, -maxSpeed, maxSpeed);
        if (Mathf.Abs(horizontalAxis) < 0.1f)
        {
            xVelocity *= 0.9f;
        }

        rbody.velocity = new Vector3(xVelocity, rbody.velocity.y, rbody.velocity.z);
        
        //Sprint Boost
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            maxSpeed += sprintBoost;
        }else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            maxSpeed -= sprintBoost;
        }

        //Jumping
        float halfHeight = col.bounds.extents.y + groundRange;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, halfHeight);
        
        Color lineColor = (isGrounded) ? Color.green : Color.red;
        Debug.DrawLine(transform.position, transform.position + Vector3.down * halfHeight, lineColor, 0f, false);
        
        if (isGrounded && Input.GetKeyDown(KeyCode.Space) )
        {
            rbody.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            rbody.AddForce(Vector3.up*jumpBoost, ForceMode.Force);
        }
        
        //Animation and Turning
        float speed = rbody.velocity.magnitude;
        
        if (rbody.velocity.x < 0f && speed>1)
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0f, 180f, 0f));
        }else if (rbody.velocity.x > 0f && speed>1)
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0f, 0f, 0f));
        }
        
        charAnimator.SetFloat("speed", speed);
        charAnimator.SetBool("Grounded", isGrounded);
        
        Debug.DrawLine(transform.position, transform.position + Vector3.up * (col.bounds.extents.y + headRange), lineColor, 0f, false);

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Block Breaking
        float halfHeight = col.bounds.extents.y + headRange;
        
        RaycastHit headHit;
        headTouch = Physics.Raycast(transform.position, Vector3.up, out headHit, halfHeight);
        
        if (headTouch)
        {
            if (headHit.collider.gameObject.CompareTag("Brick"))
            {
                Destroy(headHit.collider.gameObject);
                scored.Invoke();
            }else if (headHit.collider.gameObject.CompareTag("qBlock"))
            {
                Destroy(headHit.collider.gameObject);
                coinCollect.Invoke();
                scored.Invoke();
            }
        }
    }
}