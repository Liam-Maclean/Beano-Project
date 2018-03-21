using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MoleGameManagerScript : MonoBehaviour
{
    public enum GameState { Setup, Waiting, Starting, Playing, Timeup, Endscreen };
    private GameState m_currState;

    public enum Biome { Residential, School, Park, Forest, Downtown, Beanoland };
    private GameObject m_background;

    public enum Actor { Aqua, Blue, Green, Red };
    public GameObject hammerPrefab;
    private List<GameObject> m_hammers;

    public GameObject molePrefab;
    private List<GameObject> m_moles;
    private float[] m_spawner;
    const int SPAWNERS = 9;
    public float maxSpawnTime;
    public float minSpawnTime;

    public float startTime;
    private float m_gameTime;

    private bool m_isOldTouch;

    private GameObject m_playerIDObject;
    private int m_playerCount;
    //private NetworkInstanceId m_clientID;

    // Called on launch
    void Awake()
    {
        m_currState = GameState.Setup;

        m_hammers = new List<GameObject>();

        m_spawner = new float[SPAWNERS];

        for (int i = 0; i < SPAWNERS; i++)
        {
            m_spawner[i] = Random.Range(0.1f, maxSpawnTime);
        }
    }

    // Use this for initialization
    void Start()
    {
        m_playerIDObject = GameObject.FindGameObjectWithTag("Player");
        //m_clientID = m_playerIDObject.GetComponent<CustomLobby>().playerDetails.Identifier; 

        m_background = GameObject.FindGameObjectWithTag("Background");

        m_gameTime = startTime;

        m_isOldTouch = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_currState)
        {
            case GameState.Setup:
                InitGame(Biome.Residential, 1);
                break;
            case GameState.Waiting:
                // users ready up // Fed instructions
                m_currState = GameState.Starting;
                break;
            case GameState.Starting:
                // countdown and intro animation
                m_currState = GameState.Playing;
                break;
            case GameState.Playing:
                if (m_gameTime > 0)
                {
                    for (int i = 0; i < SPAWNERS; i++)
                    {
                        if (m_spawner[i] > 0)
                        {
                            m_spawner[i] -= Time.deltaTime;

                            if (m_spawner[i] <= 0)
                            {
                                GameObject newMole = (GameObject)Instantiate(molePrefab, new Vector3(0.0f, 0.0f, -1.0f), Quaternion.identity);
                                newMole.GetComponent<MolesScript>().InitMole(i);
                                m_moles.Add(newMole);
                            }
                        }
                    }

                    if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && m_isOldTouch == false)
                    {
                        Debug.Log("Input detected");
                        
                        Vector3 pointPos = new Vector3(0.0f,0.0f,0.0f);

                        if (Input.GetMouseButtonDown(0))
                        {
                            pointPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        }
                        else
                        {
                            pointPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        }

                        // Set to 0 for current player CHANGE TO HAMMER ID/CUSTOMLOBBY ID
                        m_hammers[0].GetComponent<IconHandlerScripts>().Touched(pointPos.x, pointPos.y);

                        RaycastHit2D[] hits = Physics2D.RaycastAll(pointPos, new Vector2(0.0f, 0.0f));

                        for (int i = 0; i < hits.Length; i++)
                        {
                            if (hits[i].collider.tag == "Mole")
                            {
                                GameObject hitMole = hits[i].collider.gameObject;
                                hitMole.GetComponent<MolesScript>().Hit();
                            }
                        }

                        m_isOldTouch = true;
                    }
                    else if (Input.GetMouseButtonDown(0) == false && Input.touchCount == 0)
                    {
                        m_isOldTouch = false;
                    }

                    m_gameTime -= Time.deltaTime;
                }
                else
                {
                    m_currState = GameState.Timeup;
                }
                break;
            case GameState.Timeup:

                GameObject[] endMoles = GameObject.FindGameObjectsWithTag("Mole");

                foreach (GameObject mole in endMoles)
                {
                    mole.GetComponent<MolesScript>().End();
                }

                Debug.Log("End Game");
                // Play outro animation
                // Fade screen
                m_currState = GameState.Endscreen;
                break;
            case GameState.Endscreen:
                // Show scores
                // Return to overworld button
                break;
            default:
                m_currState = GameState.Endscreen;
                Debug.Log("Error! gamemode unidentified");
                break;
        }
    }

    // Called by overworld to set params
    public void InitGame(Biome currBiome, int playerCount)
    {
        m_playerCount = playerCount;

        m_background.GetComponent<BackgroundScript>().SetSprite((int)currBiome);

        for(int i = 0; i < playerCount; i++)
        {
            GameObject newHammer = (GameObject)Instantiate(hammerPrefab, new Vector3(0.0f, 0.0f, -3.0f), Quaternion.identity);
            m_hammers.Add(newHammer);

            // Get saved char from lobby script from I wherever saved character info is stored
            Actor currActor = Actor.Aqua; // Set as default to blue
            m_hammers[i].GetComponent<IconHandlerScripts>().InitHammer(playerCount, (int)currActor ,i);
        }

        m_currState = GameState.Waiting;
    }

    public void ResetSpawner(int pos)
    {
        if (m_currState == GameState.Playing)
        {
            m_spawner[pos] = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }
}
