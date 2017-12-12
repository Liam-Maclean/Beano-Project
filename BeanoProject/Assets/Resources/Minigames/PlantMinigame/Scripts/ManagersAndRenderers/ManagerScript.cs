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

	//multiplayer local player info
	PlayerInfo LocalPlayerInfo;
	private PortaitScript LocalPlayerStats;


    //grid for background and plants
	PlantGrid pGrid = new PlantGrid();
	public int height, width, backgroundHeight, backgroundWidth;

	//calcualting score for plants
	private int m_combinedScore;
	private List<int> m_plantScore = new List<int>();

	//raycast stuff
	private Vector3 StartDrag, EndDrag;
	private bool oldMouseDown = false, newMouseDown = false;

	//Score stuff
	public GameObject Player1, Player2, Player3, Player4;
	private PortaitScript Player1Stats, Player2Stats, Player3Stats, Player4Stats;

    //start function
	void Start()
	{
		//set up screen orientation and plant grid
		Screen.orientation = ScreenOrientation.Landscape;
		pGrid.CreateGrd (width, height, backgroundHeight, backgroundWidth);

		//get the component stuff from the portait prefabs
		Player1Stats = Player1.GetComponent<PortaitScript> ();
		Player2Stats = Player2.GetComponent<PortaitScript> ();
		Player3Stats = Player3.GetComponent<PortaitScript> ();
		Player4Stats = Player4.GetComponent<PortaitScript> ();
	}

	//set up local multiplayer info so score only goes up on the local player
	void SetUpLocalPlayer()
	{
		switch (LocalPlayerInfo.multiplayerIndex) {
		case 1:
			LocalPlayerStats = Player1Stats;
			break;
		case 2:
			LocalPlayerStats = Player2Stats;
			break;
		case 3: 
			LocalPlayerStats = Player3Stats;
			break;
		case 4: 
			LocalPlayerStats = Player4Stats;
			break;
		}
	}

    //update function
	void Update()
	{
        //ScoreText.text = "Score: "
		//
        OnTileClick ();
	}
		
    //mouse input class
	void OnTileClick()
	{
		newMouseDown = Input.GetMouseButton (0);

        //if left mouse button is down
		if (newMouseDown == true && oldMouseDown == false)
		{
            //shoot a ray from the mouse position to the screen
			StartDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			StartDrag.z = 0;
		}
		if (newMouseDown == false && oldMouseDown == true)
		{
			EndDrag =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
			EndDrag.z = 0;

			Vector2 directionPreNorm = (EndDrag - StartDrag);
			Vector2 direction = (EndDrag - StartDrag).normalized;


			RaycastHit2D[] hits = Physics2D.RaycastAll (StartDrag, direction, directionPreNorm.magnitude);
			//Gizmos.DrawLine (new Vector3(StartDrag.x, StartDrag.y, -5), new Vector3(EndDrag.x, EndDrag.y, -5));
			//Debug.DrawLine (new Vector3(StartDrag.x, StartDrag.y, 0), new Vector3(EndDrag.x, EndDrag.y, 0), Color.green, 100.0f);
			for (int i = 0; i < hits.Length; i++) {
				//check if any of them are plants, if they are
				if (hits[i].collider.tag == "Plant")
				{
					//Get that plants script and set it to swiped
					PlantScriptManager tempPlantScript = hits[i].collider.gameObject.GetComponent<PlantScriptManager>();

					//add the plants score to the list of scores
					m_plantScore.Add(tempPlantScript.Swiped());
				}
			}

			//for each score swiped
			for (int i = 0; i < m_plantScore.Count; i++)
			{
				//add that to the game score (DO SOMETHING FUNKY WITH MULTIPLIERS HERE)
				m_combinedScore += m_plantScore[i];
			}
			m_combinedScore *= m_plantScore.Count;
			Player1Stats.IncrementScore (m_combinedScore);
			m_combinedScore = 0;
			m_plantScore.Clear();
		}
		oldMouseDown = newMouseDown;
	}
}
