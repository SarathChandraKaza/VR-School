using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SendText : MonoBehaviour
{
    string textToSend;
    public TextMeshProUGUI title;

    public void OnClick()
    {
        textToSend = title.text;
        PlayerPrefs.SetString("TextToSend", textToSend);
        PlayerPrefs.Save();
    }
}