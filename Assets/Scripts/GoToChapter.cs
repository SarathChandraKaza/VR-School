
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToChapter : MonoBehaviour
{
    public void LoadSolarSystem()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadAtoms()
    {
        SceneManager.LoadScene(13);
    }
    public void ChemicalBonding()
    {
        SceneManager.LoadScene(16);
    }
    public void FoodChain()
    {
        SceneManager.LoadScene(17);
    }
}
