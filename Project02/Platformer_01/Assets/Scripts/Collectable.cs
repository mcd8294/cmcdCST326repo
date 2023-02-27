using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Collectable : MonoBehaviour
{
    public Camera gameCamera;
    public TextMeshProUGUI scoreBoard;
    public int coinCount = 0;
    
    void Start()
    {
        
    }

    void Update()
    {
        Ray toMouse = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit mouseHit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(toMouse, out mouseHit))
        {
            GameObject objectHit = mouseHit.collider.gameObject;
            if (objectHit.CompareTag("Brick"))
            {
                Destroy(objectHit);
            }else if (objectHit.name == "QBlock(Clone)")
            {
                UpdateCoins(1);
            }
        }
    }

    public void UpdateCoins(int amount)
    {
        coinCount+=amount;
        scoreBoard.SetText("x" + coinCount);
    }
}
