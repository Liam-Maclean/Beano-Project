using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverworldScript : MonoBehaviour
{
    public int currNodeTESTING;
    public int currPlayersTESTING;

    public int maxNodes;
    public GameObject playerPrefab;

    private int m_clientID;
    private List<GameObject> m_players;

    public enum Biome {Residential, School, Park, Forest, Downtown, Beanoland};
    private string m_lastPlayed;

    void Awake()
    {
        m_players = new List<GameObject>();
    }

    void Start()
    {
        m_clientID = 0;
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
        return currNodeTESTING;
    }

    public void PushNode()
    {
        if (currNodeTESTING + 1 < maxNodes)
        {
            currNodeTESTING++;
            for (int i = 0; i < currPlayersTESTING; i++)
            {
                m_players[i].GetComponent<PlayerScript>().SetTargetPos(GetNodePos(currNodeTESTING));
            }
        }
        else
        {
            Debug.Log("EndGame Function Hit");
            currNodeTESTING = 0;
            SceneManager.LoadSceneAsync("Menu");
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
            if (node.GetComponent<NodeScript>().GetID() == currNodeTESTING)
            {
                if (node.GetComponent<NodeScript>().IsGame())
                {
                    Debug.Log("START GAME FOR NODE: " + currNodeTESTING);
                    LoadMinigame((Biome)node.GetComponent<NodeScript>().GetBiomeType());
                }
                break;
            }
        }

        PushNode();

        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera.GetComponent<CameraScript>().SetTargets(GetNodePos(currNodeTESTING));
    }

    public void LoadMinigame(Biome currBiome)
    {
        if (m_clientID == 0) // NEED TO MATCH ALL CLIENTS TO THE SAME GAME (player 1 will select minigame and will signal the other players the option chosen)
        {
            switch (currBiome)
            {
                case Biome.Residential:
                    switch (Random.Range(0,1))
                    {
                        case 0:
                            SceneManager.LoadSceneAsync(6);
                            break;
                        case 1:
                            SceneManager.LoadSceneAsync(6);
                            break;
                        default:
                            SceneManager.LoadSceneAsync(6);
                            break;
                    }
                    break;
                case Biome.School:
                    switch (Random.Range(0, 1))
                    {
                        case 0:
                            SceneManager.LoadSceneAsync(6);
                            break;
                        case 1:
                            SceneManager.LoadSceneAsync(6);
                            break;
                        default:
                            SceneManager.LoadSceneAsync(6);
                            break;
                    }
                    break;
                case Biome.Park:
                    switch (Random.Range(0, 1))
                    {
                        case 0:
                            SceneManager.LoadSceneAsync(6);
                            break;
                        case 1:
                            SceneManager.LoadSceneAsync(6);
                            break;
                        default:
                            SceneManager.LoadSceneAsync(6);
                            break;
                    }
                    break;
                case Biome.Forest:
                    switch (Random.Range(0, 1))
                    {
                        case 0:
                            SceneManager.LoadSceneAsync(6);
                            break;
                        case 1:
                            SceneManager.LoadSceneAsync(6);
                            break;
                        default:
                            SceneManager.LoadSceneAsync(6);
                            break;
                    }
                    break;
                case Biome.Downtown:
                    switch (Random.Range(0, 1))
                    {
                        case 0:
                            SceneManager.LoadSceneAsync(6);
                            break;
                        case 1:
                            SceneManager.LoadSceneAsync(6);
                            break;
                        default:
                            SceneManager.LoadSceneAsync(6);
                            break;
                    }
                    break;
                case Biome.Beanoland:
                    switch (Random.Range(0, 1))
                    {
                        case 0:
                            SceneManager.LoadSceneAsync(6);
                            break;
                        case 1:
                            SceneManager.LoadSceneAsync(6);
                            break;
                        default:
                            SceneManager.LoadSceneAsync(6);
                            break;
                    }
                    break;
                default:
                    switch (Random.Range(0, 1))
                    {
                        case 0:
                            SceneManager.LoadSceneAsync(6);
                            break;
                        case 1:
                            SceneManager.LoadSceneAsync(6);
                            break;
                        default:
                            SceneManager.LoadSceneAsync(6);
                            break;
                    }
                    break;
            }
        }
    }
}