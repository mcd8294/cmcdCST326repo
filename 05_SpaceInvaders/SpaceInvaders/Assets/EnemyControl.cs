using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyControl : MonoBehaviour
{
    public int enemyCount;
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;
    public bool isOnCooldown = false;
    public GameObject hitCooldown;
    public float speed = 15f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        rb.velocity = Vector2.right * direction * speed;
    }
    public void UpdateEnemyCount()
    {
        enemyCount--;
        speed *= 1.15f;
        if (enemyCount <= 0)
        {
            RestartScene();
        }
    }
    public void ChangeEnemyDirection()
    {
        if (hitCooldown.activeSelf)
        {
            StartCoroutine(Cooldown(isOnCooldown, 1f));            
            rb.position += new Vector2(0f, -.5f);
            direction = -direction;
        }
    }
    private IEnumerator Cooldown(bool cooldown, float waitTime)
    {
        if (!cooldown)
        {
            cooldown = true;
        }
        yield return new WaitForSeconds(waitTime);
        cooldown = false;
    }
    IEnumerator delayRestart(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void RestartScene()
    {
        StartCoroutine(delayRestart(3f));
    }
}
