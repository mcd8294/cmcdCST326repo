using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBoard : MonoBehaviour
{
    public GameObject hitCooldown;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Debug.Log("Miss");
            hitCooldown.SetActive(true);
            Destroy(collision.gameObject,0.1f);
        }
    }
}
