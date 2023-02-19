using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questionScroll : MonoBehaviour
{
    private float imgPhase = 0f;

    public Material txtr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (imgPhase < 0.6f)
        {
            imgPhase+=0.2f;
            txtr.mainTextureOffset = new Vector2(0, imgPhase);
        }
        else
        {
            imgPhase = 0;
        }
    }
}
