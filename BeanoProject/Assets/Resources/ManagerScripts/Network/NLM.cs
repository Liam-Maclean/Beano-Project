using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NLM : NetworkLobbyManager {

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);

        OnStartHost();
    }

    private List<GameScript> playerObjects = new List<GameScript>();
    private bool sentReady = false;
    private bool sendReady
    {
        get
        {
            return playersReady && GameScript.local != null && sentReady == false;
        }
    }

    private bool playersReady
    {
        get
        {
            return playerObjects.Count == playerCount;
        }
    }

    public void OnCreatedClientPlayerObject(GameScript clientPlayer)
    {
        playerObjects.Add(clientPlayer);

        if (sendReady)
        {
            SendSceneReady();
        }
    }

    public void OnCreatedLocalPlayerObject(GameScript localPlayer)
    {
        if (sendReady)
        {
            SendSceneReady();
        }
    }

    private void SendSceneReady()
    {
        sentReady = true;

        
    }

    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayerObject, GameObject gamePlayerObject)
    {
        CustomLobby lobbyPlayer = lobbyPlayerObject.GetComponent<CustomLobby>();
        GameScript gamePlayer = gamePlayerObject.GetComponent<GameScript>();

        gamePlayer.playerCount = lobbyPlayer.playerCount;

        
        gamePlayer.SetDetails(lobbyPlayer.playerDetails);

        return true;
    }

}
