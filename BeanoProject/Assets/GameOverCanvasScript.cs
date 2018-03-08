using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//global game over screen for every minigame hopefully
//should work on every mini game by using gameobject.find
//on the game's portraits and then using them to populate
//the end game screen
public class GameOverCanvasScript : MonoBehaviour {

	//portraits in game with the game's score in it
	public GameObject[] portraits;

	//positions of the portraits that are spawned on the canvas
	private List<Vector3> positions = new List<Vector3>();

	// Use this for initialization
	void Start () {

		//find all the objects in the scene with the tag portriats
		portraits = GameObject.FindGameObjectsWithTag ("Portrait");


		for (int i = 0; i < portraits.Length; i++) {
			positions.Add (new Vector3 (0, 0, 0));
		}


		SetUpPortraitPositions ();
		ReparentPortraits ();
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
			positions [1] = new Vector3 (0, 400, 0);
			positions [2] = new Vector3 (600, 0, 0);
			break;
		case 4:
			//four players
			positions [0] = new Vector3 (-600, 0, 0);
			positions [1] = new Vector3 (0, -200, 0);
			positions [2] = new Vector3 (0, 200, 0);
			positions [3] = new Vector3 (600, 0, 0);
			break;
		default:
			//Incase all else fails
			break;
		}
	}



	//calls when the end game portrait has been instantiated
	void ReparentPortraits()
	{
		//for each portrait
		for (int i = 0; i < portraits.Length; i++) {
			//reparent to the end game screen
			portraits [i].transform.SetParent (this.transform);
			portraits[i].transform.localPosition = positions[i];
		}
	}
}
