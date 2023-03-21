using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Update is called once per frame
    void Update()
    {
        text.SetText(PlayerStats.Lives.ToString() + " Lives");
    }
}
