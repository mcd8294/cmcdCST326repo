using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearLevelProgress : MonoBehaviour
{
    public void ResetGame()
    {
        PlayerPrefs.SetInt("levelReached", 1);
    }
}
