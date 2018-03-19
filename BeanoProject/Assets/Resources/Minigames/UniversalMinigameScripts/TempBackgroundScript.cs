using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//temporary script to randomise background
//Will be moved to a "theme" script or the game manager script at a later time
public class TempBackgroundScript : MonoBehaviour {

	//background sprites in folder
	public Sprite[] backgroundSprites;

	// Use this for initialization
	void Start () {
		
		//Create a random value between 0 and the amount of sprites
		int randValue = Random.Range (0, backgroundSprites.Length);

		//use random value to get a random background sprite
		this.GetComponent<SpriteRenderer> ().sprite = backgroundSprites [randValue];

	}
}
