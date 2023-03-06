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

    private void Start()
    {
    }

    private void Update()
    {
        if (!isOnCooldown)
        {
            StartCoroutine(Cooldown(Random.Range(10f, 30f)));
        }
    }

    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.CompareTag("Border")&&hitCooldown.activeSelf)
      {
          hitBorder.Invoke();
      }

      if (collision.gameObject.CompareTag("PlayerBullet"))
      {
          Debug.Log("Ouch!");
          Destroy(gameObject);
          Destroy(collision.gameObject);
          bulletCooldown.SetActive(true);
      }
    }
    private IEnumerator Cooldown(float waitTime)
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(waitTime);
        ShootBullet();
        isOnCooldown = false;
    }

    public bool GetCooldown()
    {
        return isOnCooldown;
    }

    public void ShootBullet()
    {
        GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
        Destroy(shot, 3f);
    }
}
