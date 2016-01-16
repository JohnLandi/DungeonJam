using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void toMainMenu()
	{
		Debug.Log("to main menu");
		Application.LoadLevel(0);
	}

	public void toGameScene()
	{
		Application.LoadLevel(1);
	}

	public void toIntstructions()
	{
		Application.LoadLevel(4);
	}

	public void toCredits()
	{
		Application.LoadLevel(5);
	}

	public void replay()
	{

	}
}
