using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitApp : MonoBehaviour
{
    public void ExitApplication ()
    {
        SceneManager.LoadScene(0);
    }
}
