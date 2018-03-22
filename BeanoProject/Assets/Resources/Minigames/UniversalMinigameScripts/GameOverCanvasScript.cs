using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//global game over screen for every minigame hopefully
//should work on every mini game by using gameobject.find
//on the game's portraits and then using them to populate
//the end game screen
public class GameOverCanvasScript : MonoBehaviour {

	//portraits in game with the game's score in it
	public GameObject[] portraits;

	private Text countDownText;

	float timer = 15;
	bool timeElapsed = false;
	//positions of the portraits that are spawned on the canvas
	private List<Vector3> positions = new List<Vector3>();

	// Use this for initialization
	void Start () {

		//find all the objects in the scene with the tag portriats
		portraits = GameObject.FindGameObjectsWithTag ("Portrait");

		countDownText = GameObject.Find("GameOverTimer").GetComponent<Text>();

        //add a new position for each portrait in the list of positions
		for (int i = 0; i < portraits.Length; i++) {
			positions.Add (new Vector3 (0, 0, 0));
		}

        //set up and reparent portraits from the main game
		SetUpPortraitPositions ();
		ReparentPortraits ();
	}

	public void CountDownTimerToEnd()
	{
		countDownText.text = "" + (int)timer;
		if (timer > 0) {
			timer -= Time.deltaTime;
		} else {
			timeElapsed = true;	
		}
	}

	void Update()
	{
		CountDownTimerToEnd ();
		//if time has elapsed at the end of hte minigame
		if (timeElapsed) {
			//return to the overworld
			CustomLobby.local.EndMiniGame ();
		}
	}

	//Sets up the positions for the end game portraits dependant on how many players available
	void SetUpPortraitPositions()
	{
		//switch for how many portraits exist in the game
		switch (portraits.Length) {
		case 0:
			//this is definitely wrong
			break;
		case 1:
			//one player
			positions [0] = new Vector3 (0, 0, 0);
			break;
		case 2:
			//two players
			positions [0] = new Vector3 (-400, 0, 0);
			positions [1] = new Vector3 (400, 0, 0);
			break;
		case 3:
			//three players
			positions [0] = new Vector3 (-600, 0, 0);
			positions [1] = new Vector3 (0, 0, 0);
			positions [2] = new Vector3 (600, 0, 0);
			break;
		case 4:
			//four players
			positions [0] = new Vector3 (-600, 0, 0);
			positions [1] = new Vector3 (-300, 0, 0);
			positions [2] = new Vector3 (300, 0, 0);
			positions [3] = new Vector3 (600, 0, 0);
			break;
		default:
			//Incase all else fails
			break;
		}
	}

    //Return to the overworld scene through this button method
    //@BUTTON METHOD
    public void ReturnToOverworld()
    {
       //change scene
       //Increment overworld score to the winner
    }


	//calls when the end game portrait has been instantiated
	void ReparentPortraits()
	{
		//for each portrait
		for (int i = 0; i < portraits.Length; i++) {
			//reparent to the end game screen
			portraits[i].transform.SetParent (this.transform);
			portraits[i].transform.localPosition = positions[i];
		}
	}
}
