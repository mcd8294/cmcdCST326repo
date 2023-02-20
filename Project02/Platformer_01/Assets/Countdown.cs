using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI _timerUI;
    public float timeLimit = 360f;
    void Start()
    {
        _timerUI = gameObject.GetComponent<TextMeshProUGUI>();
        displayTime(timeLimit);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLimit > 0f)
        {
            timeLimit -= Time.deltaTime;
            displayTime(timeLimit);
        }else
        {
            timeLimit = 0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void displayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float seconds = math.floor(timeToDisplay);
        _timerUI.SetText(seconds.ToString());
    }
}