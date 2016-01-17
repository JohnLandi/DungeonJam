using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void toMainMenu()
	{
		Debug.Log("to main menu");
		SceneManager.LoadScene(0);
	}

	public void toGameScene()
	{
		SceneManager.LoadScene(1);
	}

	public void toIntstructions()
	{
		SceneManager.LoadScene(4);
	}

	public void toCredits()
	{
		SceneManager.LoadScene(5);
	}

	public void replay()
	{

	}
}
