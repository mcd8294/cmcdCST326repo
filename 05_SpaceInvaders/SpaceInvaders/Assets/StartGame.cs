using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject legend;
    public GameObject gameBoard;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(TransitionToGame), 3.5f);
    }

    void TransitionToGame()
    {
        legend.SetActive(false);
        gameBoard.SetActive(true);
    }
}
