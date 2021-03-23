using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	//Play button
	public void StartGame()
	{
		SceneManager.LoadScene("SCENE", LoadSceneMode.Single);
	}

	//Quit button
	public void QuitGame()
	{
		Application.Quit();
	}

	//Tutorial button zet MainMenu uit en TutorialMenu aan, geen functie voor nodig.
}
