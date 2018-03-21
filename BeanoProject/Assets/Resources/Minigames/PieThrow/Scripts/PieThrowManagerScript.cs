﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieThrowManagerScript : MonoBehaviour
{
    private enum GAMESTATE { Start, Playing, Finished};
    private GAMESTATE m_currState;

    public int minZDist;
    public int maxZDist;

    public Text timer;
    public float timeLeft;

	//public Text score;
	private GameObject[] portraits;
	private List<PortaitScript> portraitScripts = new List<PortaitScript>();
	private PortaitScript localPortrait;


    public float spawnRateMin;
    public float spawnRateMax;
    private float[] m_spawnTimer;
    private float[] m_spawnRateRand;

    public float aircraftOdds;

    public int basicPedTypes;
    public int specialTypes;

    public Vector2 targetPos;
    public float xFlipDistance;

    public GameObject[] pedPrefabs;
    private List<GameObject> m_pedObjects;

    public Animator readyMenuAnim;
    public GameObject readyMenu;


	public GameObject gameCanvas;

	public GameObject endGameCanvas;
	private GameObject newCanvas;

    public GameObject tutorialCanvas;
    private GameObject newTutCanvas;

    public float startDelayTimer;

	private bool isEnd;

	public GameObject handSpawn;
	private HandSpawn handSpawnScript;

	private float playerScore;

    void Awake()
    {
        playerScore = 0.0f;
        m_currState = GAMESTATE.Start;

        m_pedObjects = new List<GameObject>();

        m_spawnTimer = new float[maxZDist - minZDist];
        m_spawnRateRand = new float[maxZDist - minZDist];

        for (int i = 0; i < maxZDist - minZDist; i++)
        {
            m_spawnRateRand[i] = Random.Range(spawnRateMin, spawnRateMax);
        }
        newTutCanvas = Instantiate(tutorialCanvas, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
    }

	// Use this for initialization
	void Start ()
    {
        // Set Client ID
        // Set Portraits
        // Set Powerup state

        // Call start menu
//        StartMenu();

		portraits = GameObject.FindGameObjectsWithTag ("Portrait");

		//Iterate through the length of the portrait scripts
		for (int i = 0; i < portraits.Length; i++) 
		{
			//add the portraits locally
			portraitScripts.Add (portraits [i].GetComponent<PortaitScript> ());

			//find the local player ad set it to the relative portrait
			if (portraitScripts [i].IsLocalPlayerPortrait ())
			{
				localPortrait = portraitScripts [i];
			}

		}
		handSpawnScript = handSpawn.GetComponent<HandSpawn> ();
	}
	
	// Update is called once per frame
	void Update ()
    {

		switch (m_currState)
        {
            case GAMESTATE.Start:
                //start FUNC only;


                startDelayTimer -= Time.deltaTime;

                if (startDelayTimer <= 0.0f)
                {
                    Destroy(newTutCanvas);
                    m_currState = GAMESTATE.Playing;
                }

                break;
		case GAMESTATE.Playing:
                // Normal Gameplay
				//Overall game timer
			GameTimer ();
			//DisplayScore ();
			SpawnPed ();
			if (timeLeft <= 0.0f) {
				m_currState = GAMESTATE.Finished;
				isEnd = true;
			}
                break;
            case GAMESTATE.Finished:
                // Outro Plz
				GameOver();
               	break;
            default:
                Debug.Log("GameState Error");
                break;
        }

	}

    // Called at the begining of the game to make sure all users are loaded into the game correctly
 //   void StartMenu()
 //   {
  //      readyMenuAnim.SetBool("Active", true);
  //  }
		
	void SpawnPed()
	{
		for (int i = 0; i < maxZDist - minZDist; i++)
		{
			if (m_spawnTimer[i] >= m_spawnRateRand[i])
			{
				m_spawnTimer[i] -= m_spawnRateRand[i];
				m_spawnRateRand[i] = Random.Range(spawnRateMin, spawnRateMax);

				float threshold = Random.Range(0.00f, 1.00f);
				bool isLeft = (Random.value > 0.5f);

				if (threshold > aircraftOdds)
				{
					//PED
					if (isLeft)
					{
						CreatePed(true, false, i + minZDist);
					}
					else
					{
						CreatePed(true, true, i + minZDist);
					}
				}
				else
				{
					//PLANE
					if (isLeft)
					{
						CreatePed(false, true, i + minZDist);
					}
					else
					{
						CreatePed(false, false, i + minZDist);
					}
				}
			}

			m_spawnTimer[i] += Time.deltaTime;
		}
	}



    void CreatePed(bool isBasic, bool isLeft, int zPos)
    {
        GameObject newPed;
        int typeHelper = 0;

        if (isBasic)
        {
            typeHelper = Random.Range(0, basicPedTypes);
        }
        else
        {
			typeHelper = Random.Range(basicPedTypes, specialTypes);
        }

        if (isLeft)
        {

            newPed = (GameObject)Instantiate(pedPrefabs[typeHelper], new Vector3(targetPos.x, targetPos.y, zPos), Quaternion.identity);

                newPed.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        }
        else
        {

           newPed = (GameObject)Instantiate(pedPrefabs[typeHelper], new Vector3(targetPos.x + xFlipDistance, targetPos.y, zPos), Quaternion.identity);
        }

        m_pedObjects.Add(newPed);


            m_pedObjects[m_pedObjects.Count - 1].GetComponent<PedScript>().InitPed(isLeft, zPos);

    }

    void GameTimer()
    {
        timeLeft -= Time.deltaTime;

        //convert to integer
        int tempTime = (int)timeLeft;

		//set & display the current time in the scene
        timer.text = tempTime.ToString();

		if (timeLeft < 11.0f) 
		{
			timer.color = Color.red;
			Animator textAnimator = timer.GetComponent<Animator> ();

			textAnimator.SetTrigger(0);
			textAnimator.Play("TimeLeft");	
		}
    }

    void GameOver()
    {
		if (isEnd) {

			//create the endgame canvas and spawn it in the scene
			newCanvas = Instantiate (endGameCanvas, new Vector3(0.0f,0.0f, 0.0f), Quaternion.identity);
			isEnd = false;

			//destroy the hand object and for mouse controls set the cursor to visible
			handSpawnScript.Destroy ();
			//delete the current game canva
			Destroy (gameCanvas);
			//DisplayScore ();
			Cursor.visible = true;
		}
    }

//void DisplayScore()
//{
//	//convert to integer
//	int tempScore = (int)playerScore;
//
//	//score.text = tempScore.ToString();
//}

	//GETTERS
	public float GetScore()
	{
		return playerScore;
	}

	public int GetState()
	{
		int	tempCurrentState = (int)m_currState;
		return tempCurrentState;
	}

	public void AddScore(float newScore)
	{

		if (localPortrait)
		{
			//increment the score of the portraits
			playerScore += newScore;
			localPortrait.IncrementScore ((int)newScore);
		}
		else
		{
			//increment the score offline (for debugging purposes)
			portraitScripts[0].IncrementScore((int)newScore);
		}
	}

}
