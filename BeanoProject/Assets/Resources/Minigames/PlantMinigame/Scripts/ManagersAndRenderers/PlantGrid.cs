using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==============================================
//
// Plant grid Script
//
// This class creates the plant grid in formation
//
// Move the parent "Garden" gameobject in inspector to move plant grid
// 
// Liam MacLean - Edited 17/02/2018 15:49
public class PlantGrid : MonoBehaviour{

	//variables
	private float m_killTimer = 0.1f;
	private int m_plantCountX, m_plantCountY;
	private int m_width, m_height;

	//gameobjects
	private GameObject[,] m_plantGrid;
	private GameObject plantParent;

	//Creates the plant grid for the minigame (takes in Width and height from editor
	public void CreateGrid(int width, int height)
	{
		//creates an array of gameobjects for plant grid
		m_plantGrid = new GameObject[height,width];
		plantParent = GameObject.Find("PlantParent");

		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
               
                //DO NOT MESS WITH THIS THIS GAVE ME A MASSIVE HEADACHE TO MESS WITH

                //Sets the parent of the instantiated Grid object, to the Tile parent (you can move this Gameobject in the editor to move the whole tilegrid)
                m_plantGrid[y, x] = Instantiate ((GameObject)Resources.Load ("Minigames/PlantMinigame/Prefabs/pTile"));
                m_plantGrid[y, x].transform.localPosition = plantParent.transform.position;
				m_plantGrid[y, x].transform.localPosition = new Vector3(plantParent.transform.position.x + (x * 2.1f), plantParent.transform.position.y + (y * 2.2f), -2 + (x*y*0.1f));
                m_plantGrid[y, x].transform.parent = plantParent.transform;
            }
		}

		//store width and height for later
		m_width = width;
		m_height = height;

		//Store amount for killing game later on
		m_plantCountX = m_width-1;
		m_plantCountY = m_height-1;
	}

	//Kills the plants at the end of the game
	public bool KillGame()
	{
		//run down kill timer
		m_killTimer -= Time.deltaTime;

		//if all of the plants are not destroyed
		if ((m_plantCountX != -1)  &&  (m_plantCountY != -1)) {
			//if the kill timer has triggered
			if (m_killTimer <= 0.0f) {
				//get the component from the next plant and kill plant
				m_plantGrid [m_plantCountY, m_plantCountX].GetComponent<PlantScriptManager> ().KillPlant ();

				//reset timer
				m_killTimer = 0.1f;

				//if the plant is the last in the row
				if (m_plantCountX == 0) {
					//decrement row
					m_plantCountY--;
					m_plantCountX = m_width;
				}

				//decrement X 
				m_plantCountX--;
			}
			return false;
		}
		else  
		{
			return true;
		}
	}
}
