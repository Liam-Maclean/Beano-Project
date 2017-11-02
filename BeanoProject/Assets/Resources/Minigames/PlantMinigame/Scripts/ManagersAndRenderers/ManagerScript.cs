using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//==============================================
//
// Manager Script
//
// This class controls all the necessary over-arching functionality such as mouse clicks and instantiating environment grids
//
// This class will most likely be removed after prototyping
// 
// Contains mouse input and grid stuff
// 
// Also deals with screen orientation a bit
//
// Liam MacLean - 25/10/2017 03:42

public class ManagerScript : MonoBehaviour {

    //grid for background and plants
	PlantGrid pGrid = new PlantGrid();
	public int height, width, backgroundHeight, backgroundWidth;

    public int PlayerScore = 0;

    public Text ScoreText;

    //start function
	void Start()
	{
        ScoreText.text = "Score: " + PlayerScore.ToString();
		Screen.orientation = ScreenOrientation.Portrait;
		pGrid.CreateGrd (width, height, backgroundHeight, backgroundWidth);
	}

    //increment score method
    public void IncrementScore(int value)
    {
        PlayerScore += value;
    }

    //update function
	void Update()
	{
        ScoreText.text = "Score: " + PlayerScore.ToString();

        OnTileClick ();
	}

    //mouse input class
	void OnTileClick()
	{
        ////if left mouse button is down
		//if (Input.GetMouseButtonDown(0))
		//{
        //    //shoot a ray from the mouse position to the screen
		//	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //
        //    //create a raycast
		//	RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
        //
        //
        //    //if that raycast hit's something that's to do with plants
		//    if (hit.collider.gameObject.tag == "Plant") {
        //        //that plant has been "swiped" (TESTING PURPOSES ONLY)
        //        hit.collider.gameObject.GetComponent<PlantScriptManager>().Swiped();
		//    }
		//	
		//}
	}
}
