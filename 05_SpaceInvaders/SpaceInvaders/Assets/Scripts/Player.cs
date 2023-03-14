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
  public AudioSource shootSFX;
  public AudioClip destroySFX;
  public Animator MyAnimator;

  public UnityEvent restartScene;

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    MyAnimator = GetComponent<Animator>();
  }
  // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space) && coolDown.activeSelf)
      { 
        coolDown.SetActive(false);
        GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
        shootSFX.Play();
        MyAnimator.Play("PlayerShoot");
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
        MyAnimator.Play("PlayerDestroyed");
        restartScene.Invoke();
        shootSFX.PlayOneShot(destroySFX);
        Destroy(gameObject, 1.5f);
        Destroy(col.gameObject);
      }
    }
}
