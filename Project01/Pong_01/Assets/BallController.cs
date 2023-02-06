using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public Vector3 direction = new Vector3(-1,0,0);
    public Rigidbody rb;
    public GameObject lPointsBoard;
    public int lPoints = 0;
    public GameObject rPointsBoard;
    public int rPoints = 0;
    public GameObject winText;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction, ForceMode.Acceleration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        direction *= -1;
        rb.AddForce(direction, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.name == "RGoal")
        {
            lPoints++;
            lPointsBoard.GetComponent<TextMeshProUGUI>().SetText(lPoints.ToString());
            rb.transform.position = new Vector3(0, 0, 0);
            if (lPoints >= 10)
            {
                winText.GetComponent<TextMeshProUGUI>().SetText("Left Side Wins!");
                Debug.Log($"Left Side Wins! Final Score is {lPoints} to {rPoints}");
                winText.SetActive(true);
                rb.isKinematic = true;
                Invoke(nameof(ResetBoard),5);
            }
            else
            {
                Debug.Log($"Left Side Scored! Current Score is {lPoints} to {rPoints}");
            }
        }else if (other.name == "LGoal")
        {
            rPoints++;
            rPointsBoard.GetComponent<TextMeshProUGUI>().SetText(rPoints.ToString());
            rb.transform.position = new Vector3(0, 0, 0);
            if (rPoints >= 10)
            {
                rb.isKinematic = true;
                winText.GetComponent<TextMeshProUGUI>().SetText("Right Side Wins!");
                Debug.Log($"Left Side Wins! Final Score is {lPoints} to {rPoints}");
                winText.SetActive(true);
                rb.isKinematic = true;
                Invoke(nameof(ResetBoard),5);
            }
            else
            {
                Debug.Log($"Right Side Scored! Current Score is {lPoints} to {rPoints}");
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
        rb.isKinematic = false;
        rb.AddForce(direction, ForceMode.Acceleration);

    }
    
}
