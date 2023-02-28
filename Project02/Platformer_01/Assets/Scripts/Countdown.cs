using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI _timerUI;
    public float timeLimit = 360f;
    public bool timed;

    public UnityEvent lvlFailed;

    void Start()
    {
        _timerUI = gameObject.GetComponent<TextMeshProUGUI>();
        displayTime(timeLimit);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLimit > 0f && timed)
        {
            timeLimit -= Time.deltaTime;
            displayTime(timeLimit);
        }else if (timeLimit<=0)
        {
            lvlFailed.Invoke();
            Invoke(nameof(FailLevel), 3f);
            timeLimit = 0f;
        }
    }

    void displayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float seconds = math.floor(timeToDisplay);
        _timerUI.SetText(seconds.ToString());
    }

    public void StopTimer()
    {
        timed = false;
    }
    
    public void FailLevel()
    {
        SceneManager.LoadScene("LevelParser");
    }
}