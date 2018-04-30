﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class OverworldScript : MonoBehaviour
{
    private int m_currPlayers;
    private int m_currNode;

    public int maxNodes;
    public GameObject playerPrefab;

    private NetworkInstanceId m_clientID; // CustomLobby.playerdetails.identifiyer
    private List<GameObject> m_players;
    private List<int> m_playerChars;

    public enum Biome { Residential, School, Park, Forest, Downtown, Beanoland };
    public Biome minigameBiome;
    private string m_lastPlayed;

    private GameObject mainCamera;
    //private GameObject cloneCamera;

    public int SceneToUnload;

    public NLM nlm;

    int indexInMinigameList;

    private GameObject m_playerIDObject;

    public SpriteRenderer background;

	public GameObject gameOutPrefab;
	public GameObject fadeOutPrefab;
	private bool m_isEndCalled = false;

	private GameObject animationSprites;
    private GameObject m_bugFixer;

    void Start()
    {
        m_bugFixer = GameObject.FindGameObjectWithTag("BugFixer");
        //m_bugFixer.GetComponent<BugFixScript>().SetWorldCanvas();

        m_players = new List<GameObject>();
        m_playerChars = new List<int>();
        Orientor.pieThrow = false;

        animationSprites = GameObject.Find ("OverworldBackground");
        m_playerIDObject = GameObject.FindGameObjectWithTag("Player");
        m_clientID = CustomLobby.local.playerDetails.Identifier;
        m_currPlayers = FindObjectsOfType<CustomLobby>().Length;

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //cloneCamera = Instantiate(mainCamera, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        //mainCamera.SetActive(false);

        Debug.Log("Network ID: " + m_clientID);

        nlm = FindObjectOfType<NLM>();

        InitiWorld();
        NodeReached();
        nlm.ReadyButton();
    }

    void InitiWorld()
    {
        foreach (CustomLobby opp in FindObjectsOfType<CustomLobby>())
        {
            int tempPlayerChar = opp.GetCharID();
            m_playerChars.Add(tempPlayerChar);
        }

        for (int i = 0; i < m_currPlayers; i++)
        {
            GameObject newPlayer;

            newPlayer = (GameObject)Instantiate(playerPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            m_players.Add(newPlayer);

            // Set players character here based from custom lobby
            m_players[i].GetComponent<PlayerScript>().InitPlayer(i, m_playerChars[i]);
        }
    }

    public int GetCurrNode()
    {
        return m_currNode;
    }

    public void PushNode()
    {
        if (m_currNode + 1 < maxNodes)
        {
            m_currNode++;
            for (int i = 0; i < m_currPlayers; i++)
            {
                m_players[i].GetComponent<PlayerScript>().SetTargetPos(GetNodePos(m_currNode));
            }
        }
        else
        {
            Debug.Log("EndGame Function Hit");
            //m_currNode = 0;
            //for (int i = 0; i < currPlayersTESTING; i++)
            //{
            //    m_players[i].GetComponent<PlayerScript>().SetTargetPos(GetNodePos(m_currNode));
            //}

			if (m_isEndCalled == false)
			{
				GameObject gameOut;
				gameOut = Instantiate(gameOutPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
				//gameOut.GetComponent<OverworldOverScript> ().enabled = true;

				GameObject fadeOut;
				fadeOut = Instantiate(fadeOutPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
				fadeOut.transform.localScale += new Vector3(3.0f, 3.0f, 0.0f);
				GameObject realCanvas;
				realCanvas = GameObject.FindGameObjectWithTag("OverworldCanvas");
				fadeOut.transform.SetParent(realCanvas.transform, false);
				//realCanvas.AddComponent<CanvasPortraitSetup> ();
				//realCanvas.GetComponent<CanvasPortraitSetup> ().enabled = true;

				// Remove Score box if created
				//GameObject timerbox;
				//timerbox = GameObject.FindGameObjectWithTag("txt");
				//Destroy(timerbox);

				m_isEndCalled = true;
			}

            //SceneManager.LoadScene ("PlantMinigameScene");
            //SceneManager.LoadScene("Menu");
        }
    }

    public Vector3 GetNodePos(int currNode)
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");

        foreach (GameObject node in nodes)
        {
            if (node.GetComponent<NodeScript>().GetID() == currNode)
            {
                Debug.Log("Node: " + currNode);
                return node.GetComponent<NodeScript>().GetPos();
            }
        }

        Debug.Log("Error Node not found");
        return Vector3.zero;
    }

    public void NodeReached()
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");

        

        foreach (GameObject node in nodes)
        {
            if (node.GetComponent<NodeScript>().GetID() == m_currNode)
            {
                if (node.GetComponent<NodeScript>().IsGame())
                {
                    Debug.Log("START GAME FOR NODE: " + m_currNode);

                    minigameBiome = (Biome)node.GetComponent<NodeScript>().GetBiomeType();

                    if (CustomLobby.local.isServer) // NEED TO MATCH ALL CLIENTS TO THE SAME GAME (player 1 will select minigame and will signal the other players the option chosen)
                    {
                        LoadMinigameHost();
                    }
                    
                    Stop();
                }
                break;
            }
        }

        PushNode();

        GameObject thisCamera = GameObject.FindGameObjectWithTag("MainCamera");
        thisCamera.GetComponent<CameraScript>().SetTargets(GetNodePos(m_currNode));
    }

    public void LoadMinigameHost()
    {   
        float chance = 100/Selector.activeMinigames.Count;
        int x = Random.Range(0, 100);
        indexInMinigameList = 0;
        for (float i=chance; i<=100; i+=chance)
        {
            if (x<i)
            {
                GameObject sceneControlObj = GameObject.FindGameObjectWithTag("SceneController");
                sceneControlObj.GetComponent<Networker>().RpcLoadGame(Selector.activeMinigames[indexInMinigameList]);
                SceneToUnload = Selector.activeMinigames[indexInMinigameList];

                // FindObjectOfType<Networker>().RpcLoadGame(Selector.activeMinigames[indexInMinigameList]);
                // SceneManager.LoadSceneAsync(Selector.activeMinigames[indexInMinigameList], LoadSceneMode.Additive);
                goto BreakOut;
            }
            ++indexInMinigameList;
        }
        BreakOut:;

        //switch (Random.Range(0, 2))
        //{
        //    case 0:
        //        //SceneManager.LoadScene(4, LoadSceneMode.Additive); // Garden Destruction
        //        m_playerIDObject.GetComponent<CustomLobby>().Scene = 4;
        //        break;
        //    case 1:
        //        //SceneManager.LoadScene(5, LoadSceneMode.Additive); // Pie Throw
        //        m_playerIDObject.GetComponent<CustomLobby>().Scene = 5;
        //        break;
        //    case 2:
        //        //SceneManager.LoadScene(6, LoadSceneMode.Additive); // Mole Control
        //        m_playerIDObject.GetComponent<CustomLobby>().Scene = 6;
        //        break;
        //    default:
        //        //SceneManager.LoadScene(1); // Lobby Error
        //        m_playerIDObject.GetComponent<CustomLobby>().Scene = 1; ;
        //        Debug.Log("Error");
        //        break;
        //}
    }

    public void LoadMiniGameClient(int sceneID)
    {
        //Stop();
        //SceneManager.LoadSceneAsync(sceneID);
        
    }

    protected void Stop()
    {
        //CustomLobby.local.ReadyPlayerFUN(false);
        PlayerScript[] players = FindObjectsOfType<PlayerScript>();
        foreach (PlayerScript player in players)
        {
            player.gameState = PlayerScript.GameState.InGame;
        }
			
        mainCamera.SetActive(false);
        background.enabled = false;
		animationSprites.SetActive (false);
    }

    public void Resume()
    {
        FindObjectOfType<PlayerScript>().GetComponent<SpriteRenderer>().enabled = true;
        SceneManager.UnloadSceneAsync(SceneToUnload);
       // m_playerIDObject.GetComponent<CustomLobby>().Scene = 0;
        PlayerScript[] players = FindObjectsOfType<PlayerScript>();
        foreach (PlayerScript player in players)
        {
            //CustomLobby.local.ReadyPlayerFUN(true);
            player.gameState = PlayerScript.GameState.Playing;
        }
        background.enabled = true;
        mainCamera.SetActive(true);
		animationSprites.SetActive (true);
        //cloneCamera = Instantiate(mainCamera, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        //cloneCamera.SetActive(true);

        GameObject[] chibis = GameObject.FindGameObjectsWithTag("Chibi");
        foreach (GameObject chibi in chibis)
        {
            chibi.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

	public void EndOverworld()
	{
        //m_bugFixer.GetComponent<BugFixScript>().SetMenuCanvas();
        //SceneManager.LoadScene ("Loading");
        nlm.ServerReturnToLobby();
		//SceneManager.UnloadSceneAsync ("Overworld");
	}

    public void Go()
    {
        PlayerScript[] players = FindObjectsOfType<PlayerScript>();
        

        foreach (PlayerScript player in players)
        {
            player.gameState = PlayerScript.GameState.Playing;
            //player.GetComponent<SpriteRenderer>().enabled = true;
        }

        GameObject[] chibis = GameObject.FindGameObjectsWithTag("Chibi");
        foreach (GameObject chibi in chibis)
        {
            chibi.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}