using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//global game over screen for every minigame hopefully
//should work on every mini game by using gameobject.find
//on the game's portraits and then using them to populate
//the end game screen
public class GameOverCanvasScript : MonoBehaviour {

	public GameObject[] portraits;
	public Vector3[] positions;


	// Use this for initialization
	void Start () {

		//find all the objects in the scene with the tag portriats
		portraits = GameObject.FindGameObjectsWithTag ("Portrait");

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
			break;
		case 2:
			//two players
			break;
		case 3:
			//three players
			positions [0] = new Vector3 (-600, 0, 0);
			positions [1] = new Vector3 (0, 400, 0);
			positions [2] = new Vector3 (600, 0, 0);
			break;
		case 4:
			//four players
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

	////instantiates all the end game portraits
	//void InstantiateEndGamePortraits()
	//{
	//	//for each portrait in the game being used (for each player)
	//	for (int i = 0; i < portraits.Length; i++) {
	//		
	//		//instantiate a new prefab
	//		GameObject portraitTemp = Instantiate (Resources.Load ("Minigames/PlantMinigame/Prefabs/PlayerPortait"), new Vector3(0,0,0), Quaternion.identity) as GameObject;
	//		portraitTemp.transform.SetParent (this.transform);
	//		portraitTemp.transform.localPosition = positions[i];
	//
	//
	//		//Destroy default portrait script
	//		//Destroy(portraitTemp.GetComponent<PortaitScript> ());
	//		//portraitTemp.GetComponent<PortaitScript>().(portraitScripts[i];
	//		//give the new prefab the existing player portrait script
	//		//portraitTemp.AddComponent(portraitScripts [i]);
	//
	//	}
	//
	//}


	// Update is called once per frame
	void Update () {
		
	}
}
