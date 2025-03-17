using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour 
{
    public void toStartScene()
    {
        SceneManager.LoadScene(0);
    }
    public void toPlayScene()
    {
        SceneManager.LoadScene(1);
    }
    public void toEndScene()
    {
        SceneManager.LoadScene(2);
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
