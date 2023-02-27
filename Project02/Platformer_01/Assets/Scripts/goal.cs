using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class goal : MonoBehaviour
{
    public UnityEvent goalReached;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

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
