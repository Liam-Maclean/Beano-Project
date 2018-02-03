using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NLM : NetworkLobbyManager {

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
    private List<GameScript> playerObjects = new List<GameScript>();
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
            return playersReady && GameScript.local != null && sentReady == false;
        }
    }

    /// <summary>
    /// determine if all the players have joined
    /// </summary>
    private bool playersReady
    {
        get
        {
            return playerObjects.Count == GameScript.local.playerCount;
        }
    }

    /// <summary>
    /// When the player object is creted, add it to the list
    /// If this was the last player that needed to join, send ready message
    /// </summary>
    /// <param name="clientPlayer">the joining player</param>
    public void OnCreatedClientPlayerObject(GameScript clientPlayer)
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
    public void OnCreatedLocalPlayerObject(GameScript localPlayer)
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

        //TODO set up readying system
    }

    /// <summary>
    /// when the scene loads, transfer information from the lobby player to the game player
    /// </summary>
    /// <param name="lobbyPlayerObject">the object holding player data in the lobby</param>
    /// <param name="gamePlayerObject">the object holding player data in the game</param>
    /// <returns></returns>
    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayerObject, GameObject gamePlayerObject)
    {
        CustomLobby lobbyPlayer = lobbyPlayerObject.GetComponent<CustomLobby>();
        GameScript gamePlayer = gamePlayerObject.GetComponent<GameScript>();

        gamePlayer.playerCount = lobbyPlayer.playerCount;

        
        gamePlayer.SetDetails(CustomLobby.local.playerDetails);

        return true;
    }

}
