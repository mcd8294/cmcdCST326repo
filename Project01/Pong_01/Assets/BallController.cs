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
    public int bounces = 0;
    public Quaternion rotation = Quaternion.Euler(0f, -60f, 0f);
    public float bounceForce = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        BoxCollider bc = collision.gameObject.GetComponent<BoxCollider>();
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
                if (collision.gameObject.name == "LPaddle")
                {
                    rotation = Quaternion.Euler(0f, -120f, 0f);
                }
                else
                {
                    rotation = Quaternion.Euler(0f, 120f, 0f);
                }
            }
            else if (rb.transform.position.z > minZ && rb.transform.position.z < midZ)
            {
                Debug.Log($"ball is near bottom");
                if (collision.gameObject.name == "LPaddle")
                {
                    rotation = Quaternion.Euler(0f, -60f, 0f);
                }
                else
                {
                    rotation = Quaternion.Euler(0f, 60f, 0f);
                }
            }
            else
            {
                if (collision.gameObject.name == "LPaddle")
                {
                    rotation = Quaternion.Euler(0f, 0f, 0f);
                }
                else
                {
                    rotation = Quaternion.Euler(0f, -180f, 0f);
                }
            }
            Vector3 bounceDirection = rotation * Vector3.back;
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Acceleration);
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.name == "RGoal")
        {
            lPoints++;
            lPointsBoard.GetComponent<TextMeshProUGUI>().SetText(lPoints.ToString());
            rb.transform.position = new Vector3(0, 0, 0);
            direction = new Vector3(1000,0,0);
            bounces = 0;
            bounceForce = 1000f;
            if (lPoints >= 10)
            {
                rb.isKinematic = true;
                winText.GetComponent<TextMeshProUGUI>().SetText("Game Over, Left Paddle Wins!");
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
            rPoints++;
            rPointsBoard.GetComponent<TextMeshProUGUI>().SetText(rPoints.ToString());
            rb.transform.position = new Vector3(0, 0, 0);
            bounces = 0;
            bounceForce = 1000f;
            direction = new Vector3(-1000,0,0);
            if (rPoints >= 10)
            {
                rb.isKinematic = true;
                winText.GetComponent<TextMeshProUGUI>().SetText("Game Over, Right Paddle Wins!");
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
        rb.isKinematic = false;
        rb.AddForce(direction, ForceMode.Acceleration);
        rb.transform.position = new Vector3(0, 0, 0);

    }
}
