using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToIndex : MonoBehaviour
{
    public void Index()
    {
        SceneManager.LoadScene(1);
    }
}
