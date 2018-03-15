using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MoleGameManagerScript : MonoBehaviour
{
    public enum Biome { Residential, School, Park, Forest, Downtown, Beanoland };
    private GameObject m_background;

    public enum Actor { Aqua, Blue, Green, Red };
    public GameObject portraitPrefab;
    private List<GameObject> m_portraits;

    /// <summary>
    /// INSERT ICON TOUCHES HERE
    /// </summary>

    public GameObject molePrefab;
    private List<GameObject> m_moles;
    private float[] m_spawner;
    const int SPAWNERS = 9;
    public float maxSpawnTime;
    public float minSpawnTime;

    private GameObject m_playerIDObject;
    private int m_playerCount;
    //private NetworkInstanceId m_clientID;

    // Called on launch
    void Awake()
    {
        m_portraits = new List<GameObject>();

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
    }

    // Update is called once per frame
    void Update()
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

        // DEBUG CALL AND SETUP
        if (Input.GetKey("1") && m_portraits.Count == 0)
        {
            InitGame(Biome.Residential, 1);
        }
        else if (Input.GetKey("2") && m_portraits.Count == 0)
        {
            InitGame(Biome.Downtown, 2);
        }
        else if (Input.GetKey("3") && m_portraits.Count == 0)
        {
            InitGame(Biome.Forest, 3);
        }
        else if (Input.GetKey("4") && m_portraits.Count == 0)
        {
            InitGame(Biome.Park, 4);
        }
    }

    // Called by overworld to set params
    public void InitGame(Biome currBiome, int playerCount)
    {
        m_playerCount = playerCount;

        m_background.GetComponent<BackgroundScript>().SetSprite((int)currBiome);

        for(int i = 0; i < playerCount; i++)
        {
            GameObject newPortrait = (GameObject)Instantiate(portraitPrefab, new Vector3(0.0f, 0.0f, -3.0f), Quaternion.identity);
            m_portraits.Add(newPortrait);

            // Get saved char from lobby script from I wherever saved character info is stored
            Actor currActor = Actor.Aqua; // Set as default
            m_portraits[i].GetComponent<PortraitsHandlerScript>().InitPortrait(playerCount, (int)currActor ,i);
        }
    }

    public void ResetSpawner(int pos)
    {
        m_spawner[pos] = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
