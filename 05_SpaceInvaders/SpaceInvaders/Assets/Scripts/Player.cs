using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
  public GameObject bullet;
  public float speed = 10f;
  public float maxSpeed = 3f;
  public Rigidbody2D rb;
  public Transform shottingOffset;
  public GameObject coolDown;

  public UnityEvent restartScene;

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
  }
  // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space) && coolDown.activeSelf)
      { 
        coolDown.SetActive(false);
        GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
        Debug.Log("Bang!");

        Destroy(shot, 3f);

      }
    }
    private void FixedUpdate()
    {
      float axis = Input.GetAxis("Horizontal");
      MoveCharacter(axis);
      float xVelocity = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
      
      if (Mathf.Abs(axis) < 0.1f)
      {
        xVelocity *= 0.9f;
      }

      rb.velocity = new Vector2(xVelocity, rb.velocity.y);

    }
    void MoveCharacter(float axis)
    {
      rb.velocity += axis*Vector2.right*Time.deltaTime*speed;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
      if (col.gameObject.CompareTag("EnemyBullet"))
      {
        Debug.Log("Ouch!");
        restartScene.Invoke();
        Destroy(gameObject);
        Destroy(col.gameObject);
      }
    }
}
