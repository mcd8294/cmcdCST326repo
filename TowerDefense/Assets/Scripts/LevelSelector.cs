using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public SceneFader sceneFader;
    public Button[] levelButtons;

    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i+1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void LevelSelect(string levelName)
    {
        sceneFader.FadeTo(levelName);
    }
}
