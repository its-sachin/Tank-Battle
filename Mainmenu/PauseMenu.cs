using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject pauseMenu;
    public bool ispaused;

    void Start()
    {
    	pauseMenu.SetActive(false);
    }

    public void MainMenu()
	{
		SceneManager.LoadScene(0);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(ispaused)
			{
				ResumeGame();
			}

			else
			{
				PauseGame();
			}
		}
	}

	public void QuitGame()
	{
		Debug.Log("Quit!");
		Application.Quit();
	}

	public void PauseGame()
	{
		pauseMenu.SetActive(true);
		Time.timeScale = 0f;
		ispaused = true;
	}

	public void ResumeGame()
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
		ispaused = false;
	}

}
