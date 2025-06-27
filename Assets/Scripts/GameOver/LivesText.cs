using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesText : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.SetText("X " + PlayerData.CurrentLives);
    }
}
