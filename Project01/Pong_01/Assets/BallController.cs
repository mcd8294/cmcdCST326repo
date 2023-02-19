using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public Vector3 direction = new Vector3(-1000,0,0);
    public Rigidbody rb;
    public GameObject lPointsBoard;
    public int lPoints = 0;
    public GameObject rPointsBoard;
    public int rPoints = 0;
    public GameObject winText;
    private int bounces = 0;
    private Quaternion rotation = Quaternion.Euler(0f, -60f, 0f);
    public float bounceForce = 1000f;
   
    public AudioClip bounceUp;
    public AudioClip bounceDown;
    private bool isBounceUp = true;
    public GameObject bonusPowerUp;
    public GameObject growPowerUp;
    private bool bonusActive = false;
    private bool growActive = false;
    public GameObject lPaddle;
    public GameObject rPaddle;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        BoxCollider bc = collision.gameObject.GetComponent<BoxCollider>();
        AudioSource soundSource = collision.gameObject.GetComponent<AudioSource>();
        
        Bounds bounds = bc.bounds;
        float maxZ = bounds.max.z;
        float minZ = bounds.min.z;
        float midZ = bounds.center.z;

        if (collision.gameObject.CompareTag("Paddle"))
        {
            if (bounces < 14)
            {
                bounces++;
                bounceForce *= 1.1f;
            }
            Debug.Log($"z pos of ball is {rb.transform.position.z}");
            rb.velocity = Vector3.zero;

            if (rb.transform.position.z < maxZ && rb.transform.position.z > midZ)
            {
                Debug.Log($"ball is near top");
                isBounceUp = true;
                if (collision.gameObject.name == "LPaddle")
                {
                    rotation = Quaternion.Euler(0f, -130f, 0f);
                }
                else
                {
                    rotation = Quaternion.Euler(0f, 130f, 0f);
                }
            }
            else if (rb.transform.position.z > minZ && rb.transform.position.z < midZ)
            {
                Debug.Log($"ball is near bottom");
                isBounceUp = false;
                if (collision.gameObject.name == "LPaddle")
                {
                    rotation = Quaternion.Euler(0f, -50f, 0f);
                }
                else
                {
                    rotation = Quaternion.Euler(0f, 50f, 0f);
                }
            }
            // else
            // {
            //     if (collision.gameObject.name == "LPaddle")
            //     {
            //         rotation = Quaternion.Euler(0f, 0f, 0f);
            //     }
            //     else
            //     {
            //         rotation = Quaternion.Euler(0f, -180f, 0f);
            //     }
            // }
            Vector3 bounceDirection = rotation * Vector3.back;
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Acceleration);
            if (isBounceUp)
            {
                soundSource.PlayOneShot(bounceUp);
            }
            else
            {
                soundSource.PlayOneShot(bounceDown);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.name == "RGoal")
        {
            if (bonusActive)
            {
                lPoints+=3;
                bonusActive = false;
            }
            else
            {
                lPoints++;
            }

            if (growActive)
            {
                growActive = false;
                rPaddle.transform.localScale /= 1.5f;
                lPaddle.transform.localScale /= 1.5f;
            }
            lPointsBoard.GetComponent<TextMeshProUGUI>().SetText(lPoints.ToString());
            if (lPoints >= 9)
            {
                lPointsBoard.GetComponent<TextMeshProUGUI>().color = Color.yellow;
                bonusPowerUp.SetActive(true);
            }else if (lPoints == 5 && rPoints == 5)
            {
                growPowerUp.SetActive(true);
            }
            rb.transform.position = new Vector3(0, 0, 0);
            direction = new Vector3(1000,0,0);
            bounces = 0;
            bounceForce = 1000f;
            if (lPoints >= 10)
            {
                rb.isKinematic = true;
                winText.GetComponent<TextMeshProUGUI>().SetText("Game Over, Left Paddle Wins!");
                winText.GetComponent<TextMeshProUGUI>().color = Color.green;
                Debug.Log($"Game Over, Left Paddle Wins! Final Score is {lPoints} to {rPoints}");
                winText.SetActive(true);
                Invoke(nameof(ResetBoard),5);
            }
            else
            {
                Debug.Log($"Left Paddle Scored! Current Score is {lPoints} to {rPoints}");
                rb.velocity = Vector3.zero;
                rb.AddForce(direction, ForceMode.Acceleration);
            }
        }else if (other.name == "LGoal")
        {
            if (bonusActive)
            {
                rPoints+=3;
                bonusActive = false;
            }
            else
            {
                rPoints++;
            }
            rPointsBoard.GetComponent<TextMeshProUGUI>().SetText(rPoints.ToString());
            if (rPoints >= 9)
            {
                rPointsBoard.GetComponent<TextMeshProUGUI>().color = Color.yellow;
                bonusPowerUp.SetActive(true);
            }else if (lPoints == 5 && rPoints == 5)
            {
                growPowerUp.SetActive(true);
            }
            rb.transform.position = new Vector3(0, 0, 0);
            bounces = 0;
            bounceForce = 1000f;
            direction = new Vector3(-1000,0,0);
            if (rPoints >= 10)
            {
                rb.isKinematic = true;
                winText.GetComponent<TextMeshProUGUI>().SetText("Game Over, Right Paddle Wins!");
                winText.GetComponent<TextMeshProUGUI>().color = Color.red;
                Debug.Log($"Game Over, Right Paddle Wins! Final Score is {lPoints} to {rPoints}");
                winText.SetActive(true);
                Invoke(nameof(ResetBoard),5);
            }
            else
            {
                Debug.Log($"Right Paddle Scored! Current Score is {lPoints} to {rPoints}");
                rb.velocity = Vector3.zero;
                rb.AddForce(direction, ForceMode.Acceleration);
            }
        }else if (other.gameObject.name == "Bonus")
        {
            GameObject bObj = other.gameObject;
            bObj.SetActive(false);
            bonusActive = true;
        }else if (other.gameObject.name == "Grow")
        {
            GameObject gObj = other.gameObject;
            gObj.SetActive(false);
            growActive = true;
            rPaddle.transform.localScale *= 1.5f;
            lPaddle.transform.localScale *= 1.5f;
        }
    }

    private void ResetBoard()
    {
        winText.SetActive(false);
        lPointsBoard.GetComponent<TextMeshProUGUI>().SetText("0");
        rPointsBoard.GetComponent<TextMeshProUGUI>().SetText("0");
        lPoints = 0;
        rPoints = 0;
        bounces = 0;
        bounceForce = 1000f;
        rb.velocity = Vector3.zero;
        rb.isKinematic = false;
        rb.AddForce(direction, ForceMode.Acceleration);
        rb.transform.position = new Vector3(0, 0, 0);
        rPointsBoard.GetComponent<TextMeshProUGUI>().color = Color.white;
        lPointsBoard.GetComponent<TextMeshProUGUI>().color = Color.white;
        if (growActive)
        {
            growActive = false;
            rPaddle.transform.localScale /= 1.5f;
            lPaddle.transform.localScale /= 1.5f;
        }

        bonusActive = false;
        bonusPowerUp.SetActive(false);
        growPowerUp.SetActive(false);
        lPaddle.transform.position = new Vector3(-15, 0, 0);
        rPaddle.transform.position = new Vector3(15, 0, 0);
    }
}
