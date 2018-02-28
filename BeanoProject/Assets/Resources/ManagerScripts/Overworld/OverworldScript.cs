using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class OverworldScript : MonoBehaviour
{
    public int currPlayersTESTING;
    private int m_currNode;

    public int maxNodes;
    public GameObject playerPrefab;

    private NetworkInstanceId m_clientID; // CustomLobby.playerdetails.identifiyer
    private List<GameObject> m_players;

    public enum Biome {Residential, School, Park, Forest, Downtown, Beanoland};
    private string m_lastPlayed;

    private GameObject m_playerIDObject;

    void Awake()
    {
        m_players = new List<GameObject>();
    }

    void Start()
    {
        m_playerIDObject = GameObject.FindGameObjectWithTag("Player");
        m_clientID = m_playerIDObject.GetComponent<CustomLobby>().playerDetails.Identifier;

        Debug.Log("Network ID: " + m_clientID);

        InitiWorld();
        NodeReached();
    }

    void InitiWorld()
    {
        for (int i = 0; i < currPlayersTESTING; i++)
        {
            GameObject newPlayer;

            newPlayer = (GameObject)Instantiate(playerPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            m_players.Add(newPlayer);

            m_players[i].GetComponent<PlayerScript>().InitPlayer(i, i % 2, 0);
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
            for (int i = 0; i < currPlayersTESTING; i++)
            {
                m_players[i].GetComponent<PlayerScript>().SetTargetPos(GetNodePos(m_currNode));
            }
        }
        else
        {
            Debug.Log("EndGame Function Hit");
            m_currNode = 0;
            SceneManager.LoadScene("Menu");
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
                    LoadMinigame((Biome)node.GetComponent<NodeScript>().GetBiomeType());
                }
                break;
            }
        }

        PushNode();

        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera.GetComponent<CameraScript>().SetTargets(GetNodePos(m_currNode));
    }

    public void LoadMinigame(Biome currBiome)
    {
        if (m_clientID.Value == 0) // NEED TO MATCH ALL CLIENTS TO THE SAME GAME (player 1 will select minigame and will signal the other players the option chosen)
        {
            switch (currBiome)
            {
                case Biome.Residential:
                    switch (Random.Range(0, 1))
                    {
                        case 0:
                            SceneManager.LoadSceneAsync(4);
                            break;
                        case 1:
                            SceneManager.LoadSceneAsync(4);
                            break;
                        default:
                            SceneManager.LoadSceneAsync(4);
                            break;
                    }
                    break;
                case Biome.School:
                    switch (Random.Range(0, 1))
                    {
                        case 0:
                            SceneManager.LoadSceneAsync(4);
                            break;
                        case 1:
                            SceneManager.LoadSceneAsync(4);
                            break;
                        default:
                            SceneManager.LoadSceneAsync(4);
                            break;
                    }
                    break;
                case Biome.Park:
                    switch (Random.Range(0, 1))
                    {
                        case 0:
                            SceneManager.LoadSceneAsync(4);
                            break;
                        case 1:
                            SceneManager.LoadSceneAsync(4);
                            break;
                        default:
                            SceneManager.LoadSceneAsync(4);
                            break;
                    }
                    break;
                case Biome.Forest:
                    switch (Random.Range(0, 1))
                    {
                        case 0:
                            SceneManager.LoadSceneAsync(4);
                            break;
                        case 1:
                            SceneManager.LoadSceneAsync(4);
                            break;
                        default:
                            SceneManager.LoadSceneAsync(4);
                            break;
                    }
                    break;
                case Biome.Downtown:
                    switch (Random.Range(0, 1))
                    {
                        case 0:
                            SceneManager.LoadSceneAsync(4);
                            break;
                        case 1:
                            SceneManager.LoadSceneAsync(4);
                            break;
                        default:
                            SceneManager.LoadSceneAsync(4);
                            break;
                    }
                    break;
                case Biome.Beanoland:
                    switch (Random.Range(0, 1))
                    {
                        case 0:
                            SceneManager.LoadSceneAsync(4);
                            break;
                        case 1:
                            SceneManager.LoadSceneAsync(4);
                            break;
                        default:
                            SceneManager.LoadSceneAsync(4);
                            break;
                    }
                    break;
                default:
                    switch (Random.Range(0, 1))
                    {
                        case 0:
                            SceneManager.LoadSceneAsync(4);
                            break;
                        case 1:
                            SceneManager.LoadSceneAsync(4);
                            break;
                        default:
                            SceneManager.LoadSceneAsync(4);
                            break;
                    }
                    break;
            }
        }
    }
}