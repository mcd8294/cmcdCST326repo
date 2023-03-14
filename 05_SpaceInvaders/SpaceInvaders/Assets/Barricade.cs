using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
  public GameObject cooldown;
    // Start is called before the first frame update
       private void OnCollisionEnter2D(Collision2D col)
       {
         if (col.gameObject.CompareTag("PlayerBullet"))
         {
           cooldown.SetActive(true);
           Destroy(col.gameObject);
           Destroy(gameObject);
         }
       }
}
