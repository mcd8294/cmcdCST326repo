using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletCooldown;
    public GameObject hitCooldown;
    public UnityEvent hitBorder;
    public Transform shottingOffset;
    public bool isOnCooldown = true;

    public UnityEvent destroyed;
    public UnityEvent gameOver;
    private void Update()
    {
        if (!isOnCooldown)
        {
            StartCoroutine(Cooldown(Random.Range(10f, 30f)));
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.CompareTag("Border")&&hitCooldown.activeSelf)
      {
          hitBorder.Invoke();
      }else if (collision.gameObject.CompareTag("Finish")||collision.gameObject.CompareTag("Player"))
      {
          gameOver.Invoke();
      }

      if (collision.gameObject.CompareTag("PlayerBullet"))
      {
          Debug.Log("Ouch!");
          destroyed.Invoke();
          Destroy(collision.gameObject);
          bulletCooldown.SetActive(true);
          Destroy(gameObject);
      }
    }
    private IEnumerator Cooldown(float waitTime)
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(waitTime);
        ShootBullet();
        isOnCooldown = false;
    }
    void ShootBullet()
    {
        GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
        Destroy(shot, 3f);
    }
}
