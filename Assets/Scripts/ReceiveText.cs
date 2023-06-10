using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReceiveText : MonoBehaviour
{
    public RecommendationController rc;
    public TextMeshProUGUI chapter;
    void Start()
    {
        string textReceived = PlayerPrefs.GetString("TextToSend");
        chapter.text = textReceived;
        rc.LoadDataFromCSV(textReceived);

    }
}
