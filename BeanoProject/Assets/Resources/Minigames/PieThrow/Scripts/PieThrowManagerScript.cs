using System.Collections;
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

	public Text score;

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

	public GameObject endGameCanvas;
	private GameObject newCanvas;
	private bool isEnd;

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
    }

	// Use this for initialization
	void Start ()
    {
        // Set Client ID
        // Set Portraits
        // Set Powerup state

        // Call start menu
        StartMenu();
	}
	
	// Update is called once per frame
	void Update ()
    {



		switch (m_currState)
        {
            case GAMESTATE.Start:
                //start FUNC only;
				m_currState = GAMESTATE.Playing;
                break;
		case GAMESTATE.Playing:
                // Normal Gameplay
				//Overall game timer
			GameTimer ();
			DisplayScore ();
			SpawnPed ();
			if (timeLeft <= 0.0f) 
			{
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
    void StartMenu()
    {
        readyMenuAnim.SetBool("Active", true);
    }

    // Called to start the active game & timers
    void StartGame()
    {
     	   
    }
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
						CreatePed(false, false, i + minZDist);
					}
					else
					{
						CreatePed(false, true, i + minZDist);
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
            typeHelper = Random.Range(0, specialTypes);
            typeHelper += basicPedTypes;
        }

        if (isLeft)
        {
            newPed = (GameObject)Instantiate(pedPrefabs[typeHelper], new Vector3(targetPos.x, targetPos.y, zPos), Quaternion.identity);
        }
        else
        {
           newPed = (GameObject)Instantiate(pedPrefabs[typeHelper], new Vector3(targetPos.x + xFlipDistance, targetPos.y, zPos), Quaternion.identity);

            if (isBasic)
            {
                newPed.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
        }

        m_pedObjects.Add(newPed);

        if (isBasic)
        {
            m_pedObjects[m_pedObjects.Count - 1].GetComponent<PedScript>().InitPed(isLeft, zPos);
        }
    }

    void GameTimer()
    {
        timeLeft -= Time.deltaTime;

        //convert to integer
        int tempTime = (int)timeLeft;

        timer.text = tempTime.ToString();
    }

    void GameOver()
    {
		if (isEnd) {
			
			newCanvas = Instantiate (endGameCanvas, new Vector3(0.0f,0.0f, 0.0f), Quaternion.identity);
			isEnd = false;
		}
    }

	void DisplayScore()
	{
		//convert to integer
		int tempScore = (int)playerScore;

		score.text = tempScore.ToString();
	}

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
		playerScore += newScore;
	}

}
