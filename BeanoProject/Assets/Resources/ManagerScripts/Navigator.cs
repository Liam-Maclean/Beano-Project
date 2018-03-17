using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigator : MonoBehaviour {

	//Canvasses
	public Canvas titleCard;
	public Canvas mainMenu;
	public Canvas unlockRoom;
	public Canvas settings;
	public Canvas select;
	public SceneTransition transitionScript;


	//menu fun stuff
	public GameObject touchEffect;

	// Use this for initialization
	void Start ()
	{
		// find all the canvasses and disable them
		Canvas[] canvasses = FindObjectsOfType(typeof(Canvas)) as Canvas[];
		foreach (Canvas canvas in canvasses)
		{
			canvas.enabled = false;
		}
		//enable title card
		titleCard.enabled = true;
	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			Vector2 touchPos = Input.GetTouch(0).position;
			Vector2 objPos = Camera.main.ScreenToWorldPoint(touchPos);
			touchEffect = Instantiate(touchEffect, objPos, Quaternion.identity);
		}
	}

	//progress from title screen
	public void TapToStart()
	{
		//titleCard.enabled = false;
		mainMenu.enabled = true;
	}

	//open lobby
	public void StartShortGame()
	{
		transitionScript.Transition();
		//needs variable for game length added
		PlayerPrefs.SetInt("Game Length", 5);                       //Variable added, will look at other ways to do this; it is not automatically zeroed on game start. This can be added or it may not be
		SceneManager.LoadScene("Loading", LoadSceneMode.Single);    // necessary, depending on if and how it is handled during actual gameplay
	}

	//open lobby
	public void StartLongGame()
	{
		transitionScript.Transition();
		//needs variable for game length added
		PlayerPrefs.SetInt("Game Length", 20);                      //Variable added
		SceneManager.LoadScene("Loading", LoadSceneMode.Single);
	}

	//go from unlock screen to main menu
	public void ReturnFromUnlock()
	{
		unlockRoom.enabled = false;
		mainMenu.enabled = true;
	}

	//go to unlock screen from main menu
	public void EnterUnlock()
	{
		mainMenu.enabled = false;
		unlockRoom.enabled = true;
	}

	//open settings screen from main menu
	public void Settings()
	{
		settings.enabled = true;
		mainMenu.enabled = false;
	}

	//return to menu from settings screen
	public void ReturnFromSettings()
	{
		mainMenu.enabled = true;
		settings.enabled = false;
	}

	//go to level select for dev testing
	public void EnterLevelSelect()
	{
		select.enabled = true;
		mainMenu.enabled = false;
	}

    public void CharacterSelect()
    {
        mainMenu.enabled = false;
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    public void ReturnFromSelect()
    {
        mainMenu.enabled = true;
    }
}