using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Update is called once per frame
    void Update()
    {
        text.SetText("$"+PlayerStats.Money.ToString());
    }
}
