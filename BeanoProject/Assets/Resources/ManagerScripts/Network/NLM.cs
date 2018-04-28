using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NLM : NetworkLobbyManager {

    public static bool goAhead = false;
    bool shouldReady = false;
    public Canvas hostJoin;
    public Canvas inGame;
    protected static bool[] readyList = new bool[Selector.activeMinigames.Count];
    public Image readyButton;
    public Sprite isReady;
    public Sprite isntReady;

    /// <summary>
    /// when server connects, start the host
    /// </summary>
    /// <param name="conn">connection</param>
    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);

        OnStartHost();
    }

    /// <summary>
    /// list of player objects
    /// </summary>
    public static List<CustomLobby> playerObjects = new List<CustomLobby>();
    /// <summary>
    /// bool to determine if ready message has been sent
    /// </summary>
    private bool sentReady = false;
    /// <summary>
    /// bool to determine if ready message should now be sent
    /// </summary>
    private bool sendReady
    {
        get
        {
            return playersReady && CustomLobby.local != null && sentReady == false;
        }
    }

    /// <summary>
    /// determine if all the players have joined
    /// </summary>
    private bool playersReady
    {
        get
        {
            return playerObjects.Count == CustomLobby.local.playerCount;
        }
    }

    /// <summary>
    /// When the player object is creted, add it to the list
    /// If this was the last player that needed to join, send ready message
    /// </summary>
    /// <param name="clientPlayer">the joining player</param>
    public void OnCreatedClientPlayerObject(CustomLobby clientPlayer)
    {
        playerObjects.Add(clientPlayer);

        if (sendReady)
        {
            SendSceneReady();
        }
    }

    /// <summary>
    /// if the local player is the last to join, send ready message
    /// </summary>
    /// <param name="localPlayer"></param>
    public void OnCreatedLocalPlayerObject(CustomLobby localPlayer)
    {
        if (sendReady)
        {
            SendSceneReady();
        }
    }

    /// <summary>
    /// send ready message
    /// </summary>
    private void SendSceneReady()
    {
        sentReady = true;

        goAhead = true;
    }

    public void HostButton()
    {
        this.GetComponent<LobbyDiscovery>().Host();
        StartHost();
        hostJoin.enabled = false;
        inGame.enabled = true;
    }

    public void JoinButton()
    {
        this.GetComponent<LobbyDiscovery>().Join();
    }

    public void ReadyButton()
    {
        shouldReady = !shouldReady;
        foreach (CustomLobby player in FindObjectsOfType<CustomLobby>())
        {
            if (player.playerDetails.Identifier == CustomLobby.local.playerDetails.Identifier)
            {
                if (shouldReady)
                {
                    player.SendReadyToBeginMessage();
                    readyButton.sprite = isReady;
                }
                else
                {
                    player.SendNotReadyToBeginMessage();
                    //readyButton.sprite = isntReady;
                }
            }
            //CheckReadyToBegin();
        }
    }

    public void FoundGame(string fromAddress)
    {
        networkAddress = fromAddress;
        StartClient();
        hostJoin.enabled = false;
        inGame.enabled = true;
    }

    private void Start()
    {
        inGame.enabled = false;
    }

    public static void ResumeWalking(NetworkInstanceId id)
    {
        readyList[int.Parse(id.ToString())] = true;
        for (int i = 0; i<Selector.activeMinigames.Count; i++)
        {
            if (readyList[i] == false)
            {
                goto NotReady;
            }
        }
        FindObjectOfType<OverworldScript>().Go();
        NotReady:;
    }
    
    public static void Unready()
    {
        for (int i = 0; i < Selector.activeMinigames.Count; i++)
        {
            readyList[i] = false;
        }
    }
}
