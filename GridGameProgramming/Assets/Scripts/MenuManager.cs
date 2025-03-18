using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour 
{
    public TextMeshProUGUI endScoreText;

    public void toStartScene()
    {
        SceneManager.LoadScene(0);
    }
    public void toPlayScene()
    {
        SceneManager.LoadScene(1);
    }
    public void exitGame()
    {
        Application.Quit();
    }

	private void Update()
	{
		if(endScoreText != null)
        {
            endScoreText.text = "Your ending score was " + PlayerPrefs.GetInt("score") + ". The current high score is " + PlayerPrefs.GetInt("HighScore") + ".";
        }
	}
}
