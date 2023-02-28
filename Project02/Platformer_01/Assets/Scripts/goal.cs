using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class goal : MonoBehaviour
{
    public UnityEvent goalReached;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            goalReached.Invoke();
            Invoke(nameof(resetScene), 3f);
        }        
    }

    void resetScene()
    {
        SceneManager.LoadScene("LevelParser");
    }
}
