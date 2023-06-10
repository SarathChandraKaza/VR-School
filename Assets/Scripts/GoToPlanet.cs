using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToPlanet : MonoBehaviour
{
    public void LoadSun()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadMercury()
    {
        SceneManager.LoadScene(4);
    }
    public void LoadVenus()
    {
        SceneManager.LoadScene(5);
    }
    public void LoadEarth()
    {
        SceneManager.LoadScene(6);
    }
    public void LoadMars()
    {
        SceneManager.LoadScene(7);
    }
    public void LoadJupiter()
    {
        SceneManager.LoadScene(8);
    }
    public void LoadSaturn()
    {
        SceneManager.LoadScene(9);
    }
    public void LoadUranus()
    {
        SceneManager.LoadScene(10);
    }
    public void LoadNeptune()
    {
        SceneManager.LoadScene(11);
    }
    public void LoadPluto()
    {
        SceneManager.LoadScene(12);
    }
    public void Return()
    {
        SceneManager.LoadScene(2);
    }
}
