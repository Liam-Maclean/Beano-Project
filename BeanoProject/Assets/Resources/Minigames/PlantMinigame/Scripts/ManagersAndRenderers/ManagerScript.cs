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

enum GameState
{
	transition,
	countdown,
	playing,
	end,
	counting
}
	
public class ManagerScript : MonoBehaviour {


	//Game Information
	public float gameDuration = 30.0f;
	private float m_gameTimer;

	//multiplayer local player info
	PlayerInfo LocalPlayerInfo;
	private PortaitScript m_LocalPlayerStats;

	//gamestate 
	private GameState m_gameState = GameState.playing;

    //grid for background and plants
	PlantGrid pGrid = new PlantGrid();
	public int height, width, backgroundHeight, backgroundWidth;

	//calcualting score for plants
	private int m_combinedScore;
	private List<int> m_plantScore = new List<int>();

	//raycast stuff
	private Vector3 m_StartDrag, m_EndDrag;
	private bool m_oldMouseDown = false, m_newMouseDown = false;

	//Score stuff
	public Text timer;
	public GameObject Player1, Player2, Player3, Player4;
	private PortaitScript Player1Stats, Player2Stats, Player3Stats, Player4Stats;

	//Game Ended boolean function
	public bool GameEnded()
	{
		//if time hasn't ended return false
		if (m_gameTimer <= 0.0f) {
			return true;
		} else {
			return false;
		}
	}

	//Countdown the game timer
	public void CountDown()
	{
		//remove the time elapsed
		m_gameTimer -= Time.deltaTime;

		//cast to interger so you don't see float values
		int tempTimer = (int)m_gameTimer;

		//Set game text timer
		timer.text = tempTimer.ToString ();
	}



    //start function
	void Start()
	{
		//initialise timer
		m_gameTimer = (int)gameDuration;

		//floating text manager public static utility class initialisation
		FloatingTextManager.Initialise ();

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
			m_LocalPlayerStats = Player1Stats;
			break;
		case 2:
			m_LocalPlayerStats = Player2Stats;
			break;
		case 3: 
			m_LocalPlayerStats = Player3Stats;
			break;
		case 4: 
			m_LocalPlayerStats = Player4Stats;
			break;
		}
	}

    //update function
	void Update()
	{
		
		switch (m_gameState) {
		case GameState.transition:
			break;
		case GameState.countdown:
			break;
		case GameState.playing:
			if (!GameEnded ()) {
				CountDown ();
				OnTileClick ();
			}
			break;
		case GameState.end: 
			break;
		case GameState.counting:
			break;
		}
	}
		
    //mouse input class
	void OnTileClick()
	{
		m_newMouseDown = Input.GetMouseButton (0);

        //if left mouse button is down
		if (m_newMouseDown == true && m_oldMouseDown == false)
		{
            //shoot a ray from the mouse position to the screen
			m_StartDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			m_StartDrag.z = 0;
		}
		if (m_newMouseDown == false && m_oldMouseDown == true)
		{
			m_EndDrag =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
			m_EndDrag.z = 0;

			Vector2 directionPreNorm = (m_EndDrag - m_StartDrag);
			Vector2 direction = (m_EndDrag - m_StartDrag).normalized;


			RaycastHit2D[] hits = Physics2D.RaycastAll (m_StartDrag, direction, directionPreNorm.magnitude);

			//check all the hits from the raycast
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

			//Create float text feedback numbers
			FloatingTextManager.CreateFloatingText (m_combinedScore.ToString(), Player1.transform);

			//increment the local players score
			Player1Stats.IncrementScore (m_combinedScore);
			m_combinedScore = 0;
			m_plantScore.Clear();
		}
		m_oldMouseDown = m_newMouseDown;
	}
}
