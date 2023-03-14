using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public TextMeshProUGUI scoreOut;
    public TextMeshProUGUI hiScoreOut;
    public int currentScore;
    public int hiScore;

    private string scoreKey = "Hi-Score";
    
    // Start is called before the first frame update
    void Awake()
    {
        currentScore = 0;
        hiScore = PlayerPrefs.GetInt(scoreKey);
        hiScoreOut.SetText(hiScore.ToString("0000"));
    }
    public void UpdateScore(int points)
    {
        currentScore += points;
        scoreOut.SetText(currentScore.ToString("0000"));
        if (currentScore > hiScore)
        {
            hiScore = currentScore;
            hiScoreOut.SetText(hiScore.ToString("0000"));
            SaveScore(hiScore);
        }
    }
    public void SaveScore(int score)
    {
        PlayerPrefs.SetInt(scoreKey, score);
    }
}
