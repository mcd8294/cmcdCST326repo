using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scoreBoard : MonoBehaviour
{
    public TextMeshProUGUI scoreUI;
    public int score;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreUI = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        scoreUI.SetText(score.ToString("0000000"));
    }
}
