using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NLM : NetworkLobbyManager {

    public static bool goAhead = false;

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
    private List<CustomLobby> playerObjects = new List<CustomLobby>();
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

    


}
