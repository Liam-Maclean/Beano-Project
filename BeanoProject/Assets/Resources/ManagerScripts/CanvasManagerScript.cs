using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class CanvasManagerScript : MonoBehaviour {

	public Canvas SplashCanvas;
	public Canvas MainMenuCanvas;
	public Canvas CharacterSelectCanvas;
	public Canvas MinigameSelectCanvas;
	public Canvas MinigameCanvas;

	void Start()
	{
		SplashCanvas.enabled = true;
		MainMenuCanvas.enabled = false;
		CharacterSelectCanvas.enabled = false;
		MinigameSelectCanvas.enabled = false;
		MinigameCanvas.enabled = false;
	}


	public void ChangeToMainMenu()
	{
		
		MainMenuCanvas.enabled = true;
		SplashCanvas.enabled = false;
		CharacterSelectCanvas.enabled = false;
		MinigameCanvas.enabled = false;
		MinigameSelectCanvas.enabled = false;
	}

	public void ChangeToMinigameSelect()
	{
		MinigameSelectCanvas.enabled = true;
		SplashCanvas.enabled = false;
		MainMenuCanvas.enabled = false;
		CharacterSelectCanvas.enabled = false;
		MinigameCanvas.enabled = false;
	}


	public void ChangeToMinigame()
	{
		MinigameCanvas.enabled = true;
		SplashCanvas.enabled = false;
		MinigameSelectCanvas.enabled = false;
		CharacterSelectCanvas.enabled = false;
		MainMenuCanvas.enabled = false;
	}

}
