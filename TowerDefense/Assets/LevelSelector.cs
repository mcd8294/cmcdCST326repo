using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public SceneFader sceneFader;
    
    public void LevelSelect(string levelName)
    {
        sceneFader.FadeTo(levelName);
    }
}
