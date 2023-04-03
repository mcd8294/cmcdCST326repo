using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "MainLevel";
    // Start is called before the first frame update
    public void Play()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    // Update is called once per frame
    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }
}
