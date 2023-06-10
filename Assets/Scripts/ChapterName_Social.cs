using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Networking;

public class ChapterName_Social : MonoBehaviour
{
    public TextMeshProUGUI textField;

    void Start()
    {
        LoadAndDisplayData();
    }

    void LoadAndDisplayData()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string csvFilePath = Path.Combine(Application.persistentDataPath, "DataSet.csv");

        if (!File.Exists(csvFilePath))
        {
            StartCoroutine(CopyStreamingAssetToPersistentDataPath("DataSet.csv", csvFilePath, () =>
            {
                string[] lines = File.ReadAllLines(csvFilePath);
                ProcessCSVData(lines);
            }));
        }
        else
        {
            string[] lines = File.ReadAllLines(csvFilePath);
            ProcessCSVData(lines);
        }
#else
        string csvFilePath = Path.Combine(Application.streamingAssetsPath, "DataSet.csv");

#if UNITY_EDITOR
        // Check if we need to adjust the file path when running in the Unity Editor
        if (!File.Exists(csvFilePath))
        {
            csvFilePath = Path.Combine(Application.dataPath, "StreamingAssets", "DataSet.csv");
        }
#endif

        string[] lines = File.ReadAllLines(csvFilePath);
        ProcessCSVData(lines);
#endif
    }

    void ProcessCSVData(string[] lines)
    {
        List<string> m3 = new List<string>();

        for (int i = 0; i < lines.Length; i++)
        {
            string[] m2 = lines[i].Split(',');
            for (int j = 0; j < m2.Length; j++)
            {
                if (m2[j] == "Social")
                {
                    m3.Add(m2[1]);
                }
            }
        }

        int lowerBound = 0;
        int upperBound = m3.Count;
        int rngNum = Random.Range(lowerBound, upperBound);

        textField.text = m3[rngNum].ToUpper();
    }

    IEnumerator CopyStreamingAssetToPersistentDataPath(string sourceFilePath, string destinationFilePath, System.Action onComplete)
    {
        string sourcePath = Path.Combine(Application.streamingAssetsPath, sourceFilePath);

#if UNITY_ANDROID && !UNITY_EDITOR
        UnityWebRequest www = UnityWebRequest.Get(sourcePath);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error loading CSV file: " + www.error);
            yield break;
        }

        File.WriteAllBytes(destinationFilePath, www.downloadHandler.data);
#else
        using (WWW www = new WWW(sourcePath))
        {
            yield return www;

            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log("Error loading CSV file: " + www.error);
                yield break;
            }

            File.WriteAllBytes(destinationFilePath, www.bytes);
        }
#endif

        onComplete?.Invoke();
    }
}
