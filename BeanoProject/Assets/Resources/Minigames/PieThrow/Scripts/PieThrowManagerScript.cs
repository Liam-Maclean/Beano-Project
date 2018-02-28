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

    public GameObject despawnerPrefab;
    private List<GameObject> m_despawnerObjects;

    void Awake()
    {
        timeLeft = 30.0f;


        m_currState = GAMESTATE.Start;

        m_pedObjects = new List<GameObject>();

        m_spawnTimer = new float[maxZDist - minZDist];
        m_spawnRateRand = new float[maxZDist - minZDist];

        for (int i = 0; i < maxZDist - minZDist; i++)
        {
            m_spawnRateRand[i] = Random.Range(spawnRateMin, spawnRateMax);
        }

        m_despawnerObjects = new List<GameObject>();

        GameObject newDespawner;
        newDespawner = (GameObject)Instantiate(despawnerPrefab, new Vector3(targetPos.x - 5f, targetPos.y, maxZDist - minZDist), Quaternion.identity);
        newDespawner.transform.localScale = new Vector3(1.0f, 1.0f, maxZDist - minZDist + maxZDist - minZDist * maxZDist - minZDist);
        m_despawnerObjects.Add(newDespawner);

        newDespawner = (GameObject)Instantiate(despawnerPrefab, new Vector3(targetPos.x + xFlipDistance + 5f, targetPos.y, maxZDist - minZDist), Quaternion.identity);
        newDespawner.transform.localScale = new Vector3(1.0f, 1.0f, maxZDist - minZDist + maxZDist - minZDist * maxZDist - minZDist);
        m_despawnerObjects.Add(newDespawner);
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
        //Overall game timer
      //  GameTimer();



		switch (m_currState)
        {
            case GAMESTATE.Start:
                //start FUNC only;
                break;
            case GAMESTATE.Playing:
                // Normal Gameplay
                break;
            case GAMESTATE.Finished:
                // Outro Plz
                break;
            default:
                Debug.Log("GameState Error");
                break;
        }

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
                        SpawnPed(true, false, i + minZDist);
                    }
                    else
                    {
                        SpawnPed(true, true, i + minZDist);
                    }
                }
                else
                {
                    //PLANE
                    if (isLeft)
                    {
                        SpawnPed(false, false, i + minZDist);
                    }
                    else
                    {
                        SpawnPed(false, true, i + minZDist);
                    }
                }
            }

            m_spawnTimer[i] += Time.deltaTime;
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

    void SpawnPed(bool isBasic, bool isLeft, int zPos)
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

        if (timeLeft <= 0.0f)
        {
            GameOver();
        }


    }

    void GameOver()
    {

    }








}
