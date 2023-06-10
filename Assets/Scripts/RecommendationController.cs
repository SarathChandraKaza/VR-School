using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System;
using TMPro;

public class RecommendationController : MonoBehaviour
{
    // Data loaded from final.csv and distance.csv
    private Dictionary<string, int> finalReq;
    private Dictionary<string, Dictionary<string, float>> sim;
    public TextMeshProUGUI[] outputs;

    public void LoadDataFromCSV(string s)
    {
        // Load final.csv
        string finalReqFilePath = Path.Combine(Application.streamingAssetsPath, "final.csv");
        StartCoroutine(LoadCSVData(finalReqFilePath, (lines) =>
        {
            finalReq = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                string[] values = line.Split(',');
                if (values.Length >= 2)
                {
                    string chapterName = values[1];
                    if (int.TryParse(values[0], out int index))
                    {
                        if (!finalReq.ContainsKey(chapterName))
                        {
                            finalReq.Add(chapterName, index);
                        }
                        else
                        {
                            // Key already exists, update the value
                            finalReq[chapterName] = index;
                        }
                    }
                    else
                    {
                        // Failed to parse index value
                        Debug.LogWarning("Failed to parse index value: " + values[0]);
                    }
                }
                else
                {
                    // Insufficient values in the line
                    Debug.LogWarning("Insufficient values in the line: " + line);
                }
            }

            // Load distance.csv
            string simFilePath = Path.Combine(Application.streamingAssetsPath, "distance.csv");
            StartCoroutine(LoadCSVData(simFilePath, (lines_dist) =>
            {
                string userInput = s; // Get user input from Unity UI input text field
                int index2 = finalReq[userInput];
                string[] values2 = lines_dist[index2].Split(',');
                Dictionary<int, string> dictionary = new Dictionary<int, string>();
                for (int i = 0; i < values2.Length; i++)
                {
                    dictionary.Add(i, values2[i]);
                }
                var sortedDictionary = dictionary.OrderByDescending(kvp => kvp.Value).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                int[] top6Keys = sortedDictionary.Take(6).Select(kvp => kvp.Key).ToArray();
                int j = 0;
                foreach (int key in top6Keys)
                {
                    if (finalReq.ContainsValue(key))
                    {
                        var chapterName = finalReq.FirstOrDefault(kvp => kvp.Value == key).Key;
                        outputs[j].text = chapterName;
                        j++;
                    }
                }
            }));
        }));
    }

    IEnumerator LoadCSVData(string filePath, Action<string[]> onComplete)
    {
        string csvData;

#if UNITY_ANDROID && !UNITY_EDITOR
        if (filePath.Contains("://"))
        {
            UnityWebRequest www = UnityWebRequest.Get(filePath);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error loading CSV file: " + www.error);
                yield break;
            }

            csvData = www.downloadHandler.text;
        }
        else
        {
            // Read local file directly on Android
            using (StreamReader sr = new StreamReader(filePath))
            {
                csvData = sr.ReadToEnd();
            }
        }
#else
        // Read local file on other platforms
        using (StreamReader sr = new StreamReader(filePath))
        {
            csvData = sr.ReadToEnd();
        }
#endif

        string[] lines = csvData.Split('\n');
        onComplete?.Invoke(lines);

        yield break;
    }
}
