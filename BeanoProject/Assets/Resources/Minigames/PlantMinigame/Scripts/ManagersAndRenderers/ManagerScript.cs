﻿using System.Collections;
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

//gamestates for the game
enum GameState
{
	transition,
	countdown,
	playing,
	end,
	counting
}


//Manager script
public class ManagerScript : MonoBehaviour {

	private GameObject m_tutorialCanvas;

	//swipe object
	GameObject swipe;

	//stop animation script at end of the game
	GameObject stopText;

	//fade in transition
	private StopAnimationScript FadeInAnimation;

	//stop animation script (end of game)
	private StopAnimationScript stopAnimationScript;
	private bool StopAnimInstantiated;

	//countdown at start of match
	private CountDownScript countDownScript;

	//bool
	private bool bTutorialCanvasInstantiated = false;

	//dialogue variables
	private float dialogueCooldown = 5.0f;

	//Game Information
	public float gameDuration = 30.0f;
	private float m_gameTimer;

	//multiplayer local player info
	PlayerInfo LocalPlayerInfo;
	private PortaitScript m_LocalPlayerStats;

	//gamestate 
	private GameState m_gameState = GameState.transition;

	//The plant grid has been instantiated.
	private bool m_gridGenerated = false;

    //grid for background and plants
	PlantGrid pGrid = new PlantGrid();
	public int height, width;

	//calcualting score for plants
	private int m_combinedScore;
	private List<int> m_plantScore = new List<int>();

	private List<BasePlant> m_plantsHit = new List<BasePlant>();

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

	//checking if a vector is negative or positive
	public Vector2 NegativePositiveFunction(Vector2 value)
	{
		float tempx, tempy;
		tempx = value.x;
		tempy = value.y;
		if (tempx < 0.0f) {
			tempx *= -1.0f;
		}
		if (tempy < 0.0f) {
			tempy *= -1.0f;
		}
		if (tempx > tempy) {
			return new Vector2 (value.x, 0.0f);
		}
		else if  (tempy > tempx) {
			return new Vector2 (0.0f, value.y);
		}
		return new Vector2(0.0f, 0.0f);
	}


    //start function
	void Start()
	{
		FadeInAnimation = GameObject.Find ("FadeIn").GetComponent<StopAnimationScript> ();

		//initialise timer
		m_gameTimer = (int)gameDuration;

		//floating text manager public static utility class initialisation
		FloatingTextManager.Initialise ();

		//set up screen orientation and plant grid
		Screen.orientation = ScreenOrientation.Landscape;

		//get the component stuff from the portait prefabs
		Player1Stats = Player1.GetComponent<PortaitScript> ();
		Player2Stats = Player2.GetComponent<PortaitScript> ();
		Player3Stats = Player3.GetComponent<PortaitScript> ();
		// Player4Stats = Player4.GetComponent<PortaitScript> ();
	}

	//instantiate canvas once
	public void InstantiateTutorialCanvasOnce()
	{
		m_tutorialCanvas = Instantiate(Resources.Load("Minigames/PlantMinigame/Prefabs/TutorialCanvas")) as GameObject;
		bTutorialCanvasInstantiated = true;
	}


	public void SpawnStopAnimation()
	{
		//SET UP DIALOGUE BOX SPAWN POINT TO BE REALITVE TO PLAYERS PORTRAIT POSITION
		stopText = Instantiate (Resources.Load ("Minigames/PlantMinigame/Prefabs/StopText")) as GameObject;
		stopText.transform.SetParent (GameObject.Find ("MinigameCanvas").transform);
		stopText.transform.localPosition = new Vector3 (0, 0, 1.0f);
		stopAnimationScript = stopText.GetComponent<StopAnimationScript> ();
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

		//for every game state 
		switch (m_gameState) {

		//transition between overworld and minigame
		case GameState.transition:

			//fade in animation
			if (FadeInAnimation.AnimationEnded ()) {

				//Display tutorial
				if (!bTutorialCanvasInstantiated) {
					InstantiateTutorialCanvasOnce ();
				}


				////if player wants to continue
				//if (Input.GetMouseButtonDown (0)) {

					Destroy (m_tutorialCanvas);

					//Instantiate mole plane animation
					Instantiate (Resources.Load("Minigames/PlantMinigame/Prefabs/MolePlane"));

					//Instantiate countdown text for countdown phase
					GameObject countDownObject = Instantiate (Resources.Load ("Minigames/PlantMinigame/Prefabs/CountDownText")) as GameObject;
					countDownObject.transform.SetParent (GameObject.Find ("MinigameCanvas").transform);
					countDownObject.transform.localPosition = new Vector3 (0.0f, 0.0f, 1.0f);
					countDownScript = GameObject.Find ("CountDownText(Clone)").GetComponent<CountDownScript> ();
					m_gameState = GameState.countdown;
				//}
			}



			//Transition period between overworld and minigame before game countdown
			//Possible tutorial page
			//wait for everyone to be connected and synced
			break;

		//countdown before game begins
		case GameState.countdown:
			//if the countdown animation ended
			if (countDownScript.AnimationEnded()) {
				//start game and switch gamestate 
				m_gameState = GameState.playing;
			}
			break;

		//playing the game
		case GameState.playing:

			//if the garden hasn't been generated yet 
			if (!m_gridGenerated) {
				//generate it only once
				pGrid.CreateGrid (width, height);
				m_gridGenerated = true;
			}
			//if the game hasn't ended
			if (!GameEnded ()) {
				//update game logic
				CountDown ();
				OnTileClick ();
			
			} 
			//if the game HAS ended
			else if (GameEnded()) {
				// if stop animation hasn't been instantiated
				if (!StopAnimInstantiated) {
					//instantiate only once
					SpawnStopAnimation ();
					StopAnimInstantiated = true;
					//if it has been instantiated
				} else if (StopAnimInstantiated) {
					//check if it has finished animating, if it has
					if (stopAnimationScript.AnimationEnded ()) {
						//Kill plants in the scene, end the minigame
						if (pGrid.KillGame ()) {
							m_gameState = GameState.counting;
						}
					}
				}
			}
			break;
		//game is counting score and return to overworld
		case GameState.counting:

			//GameScript.local.EndMiniGame ();
			//count the score
			//return to overworld option
			break;
		}
	}

    //mouse input class
	void OnTileClick()
	{
		//check if the mouse is down
		m_newMouseDown = Input.GetMouseButton (0);

        //if left mouse button is down
		if (m_newMouseDown == true && m_oldMouseDown == false)
		{
			swipe = Instantiate (Resources.Load ("Minigames/PlantMinigame/Prefabs/Swipe") as GameObject);

            //shoot a ray from the mouse position to the screen
			m_StartDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			m_StartDrag.z = 0;
		}
		if (m_newMouseDown == true && m_oldMouseDown == true)
		{
			swipe.transform.position = new Vector3(Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y, -5);
		}
		if (m_newMouseDown == false && m_oldMouseDown == true)
		{
			m_EndDrag =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
			m_EndDrag.z = 0;

			Vector2 direction = (m_EndDrag - m_StartDrag);

			direction = NegativePositiveFunction (direction);

			Vector2 directionPreNorm = direction;

			direction = direction.normalized;


			RaycastHit2D[] hits = Physics2D.RaycastAll (m_StartDrag, direction, directionPreNorm.magnitude);

			//check all the hits from the raycast
			for (int i = 0; i < hits.Length; i++) {
				//check if any of them are plants, if they are
				if (hits[i].collider.tag == "Plant")
				{
					//Get that plants script and set it to swiped
					PlantScriptManager tempPlantScript = hits[i].collider.gameObject.GetComponent<PlantScriptManager>();

					//get plant component
					m_plantsHit.Add(tempPlantScript.GetPlantComponent ());

					tempPlantScript.Swiped ();
					//add the plants score to the list of scores
					//m_plantScore.Add(tempPlantScript.Swiped());
				}
			}



			for (int i = 0; i < m_plantsHit.Count; i++) {
				m_plantScore.Add (0);
				int temp;
				m_plantsHit [i].ActivatePlant (out temp);
				m_plantScore [m_plantScore.Count-1] = temp;
			}

			//for each score swiped
			for (int i = 0; i < m_plantScore.Count; i++)
			{
				//add that to the game score (DO SOMETHING FUNKY WITH MULTIPLIERS HERE)
				m_combinedScore += m_plantScore[i];
			}

			//m_combinedScore *= m_plantScore.Count;

			//Create float text feedback numbers
			FloatingTextManager.CreateFloatingText (m_combinedScore.ToString(), Player1.transform);

			//increment the local players score
			Player1Stats.IncrementScore (m_combinedScore);
			m_combinedScore = 0;
			m_plantScore.Clear();
			m_plantsHit.Clear ();
		}
		m_oldMouseDown = m_newMouseDown;
	}
}
