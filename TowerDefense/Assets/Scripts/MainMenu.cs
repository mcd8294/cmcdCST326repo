using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "MainLevel";

    public SceneFader sceneFader;
    // Start is called before the first frame update
    public void Play()
    {
        sceneFader.FadeTo(levelToLoad);
    }

    // Update is called once per frame
    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }
}
