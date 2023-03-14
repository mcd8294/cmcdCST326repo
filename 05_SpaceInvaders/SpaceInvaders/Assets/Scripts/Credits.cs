using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Credits : MonoBehaviour
{
    public Animation myAnimation;
    public GameManager gameManager;

    public UnityEvent EndTrigger;
    
    // Start is called before the first frame update
    void Start()
    {
        myAnimation = gameObject.GetComponent<Animation>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void EndReel()
    {
        myAnimation.Rewind();
        myAnimation.Stop();
        EndTrigger.Invoke();
        gameManager.LoadSceneFromIndex(0);
    }
}
