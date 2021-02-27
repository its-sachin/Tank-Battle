using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Mainmenu : MonoBehaviour
{
	public void PlayGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
	}

	public void MiramarPlay()
	{
		SceneManager.LoadScene(1);
	}

	public void SanokPlay()
	{
		SceneManager.LoadScene(2);
	}

	public void QuitGame()
	{
		Debug.Log("Quit!");
		Application.Quit();
	}

	public void OpenFB()
    {
    	Application.OpenURL("https://www.facebook.com/profile.php?id=100010131825138");
    }


    public void OpenINSTA()
    {
    	Application.OpenURL("https://www.instagram.com/_.mr._sachin._/");
    }


}
