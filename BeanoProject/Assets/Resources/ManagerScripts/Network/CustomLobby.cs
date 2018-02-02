using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomLobby : NetworkLobbyPlayer {

    
    
    //syncvar will call when UpdatePlayerDetails is called
    [SyncVar(hook = "UpdatePlayerDetails")]
    public bool hasPlayerDetails = false;
    [SyncVar]
    public int playerCount = 0;

    public PlayerDetails playerDetails;

    public static CustomLobby local { get; private set; }

    public override void OnStartServer()
    {
        base.OnStartServer();

        NetworkServer.RegisterHandler(CustomMsgType.HostRecievePlayerDetails, OnHostRecievePlayerDetails);
        NetworkServer.RegisterHandler(CustomMsgType.ClientRequestPlayerDetails, OnClientRequestPlayerDetails);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        NetworkClient.allClients[0].RegisterHandler(CustomMsgType.ClientRecievePlayerDetails, OnClientRecievePlayerDetails);

        if(hasPlayerDetails)
        {
            RequestDetails();
        }
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        local = this;

        local.playerDetails.Avatar = PlayerPrefs.GetInt("Avatar");
        local.playerDetails.Handle = PlayerPrefs.GetString("Handle");
        local.playerDetails.Identifier = netId;

        SendCachedDetailRequests();

        SendDetails(new PlayerDetails(local.playerDetails.Handle, local.playerDetails.Avatar, local.playerDetails.Identifier));
    }

    private static List<NetworkInstanceId> cachedRequestIDs;

    [Client]
    private void RequestDetails()
    {
        if(!isServer)
        {
            if(CustomLobby.local == null)
            {
                if(cachedRequestIDs == null)
                {
                    cachedRequestIDs = new List<NetworkInstanceId>();
                }
                cachedRequestIDs.Add(netId);
            }
            else
            {
                SendDetailsRequestForNetId(netId);
            }
        }
    }

    private void SendCachedDetailRequests()
    {
        if (cachedRequestIDs != null)
        {
            foreach(NetworkInstanceId requestedID in cachedRequestIDs)
            {
                SendDetailsRequestForNetId(requestedID);
            }
            cachedRequestIDs.Clear();
        }
    }

    private void SendDetailsRequestForNetId(NetworkInstanceId requestedID)
    {
        NetworkClient.allClients[0].Send(CustomMsgType.ClientRequestPlayerDetails, new PlayerRequestPlayerDataMessage(CustomLobby.local.netId, requestedID));
    }

    private void SendDetails(PlayerDetails playerDetailsTemp)
    {
        NetworkClient.allClients[0].Send(CustomMsgType.HostRecievePlayerDetails, new PlayerDetailsMessage(netId, playerDetailsTemp));
    }

    private void OnHostRecievePlayerDetails(NetworkMessage netMessage)
    {
        PlayerDetailsMessage playerDetailsMessage = netMessage.ReadMessage<PlayerDetailsMessage>();

        GameObject sendingPlayerObject = NetworkServer.FindLocalObject(playerDetailsMessage.playerID);
        CustomLobby sendingPlayer = sendingPlayerObject.GetComponent<CustomLobby>();

        sendingPlayer.hasPlayerDetails = true;
        sendingPlayer.playerDetails = playerDetailsMessage.CreatePlayerDetails();
    }

    private void OnClientRequestPlayerDetails(NetworkMessage netMessage)
    {
        PlayerRequestPlayerDataMessage requestedMessage = netMessage.ReadMessage<PlayerRequestPlayerDataMessage>();

        NetworkInstanceId senderID = requestedMessage.SenderID;
        NetworkInstanceId subjectID = requestedMessage.SubjectID;

        GameObject subjectPlayerObject = NetworkServer.FindLocalObject(subjectID);
        CustomLobby subjectPlayer = subjectPlayerObject.GetComponent<CustomLobby>();

        NetworkServer.SendToClient(int.Parse(senderID.ToString()), CustomMsgType.ClientRecievePlayerDetails, new PlayerDetailsMessage(subjectID, subjectPlayer.playerDetails));
    }

    private void OnClientRecievePlayerDetails(NetworkMessage netMessage)
    {
        PlayerDetailsMessage playerDetailsMessage = netMessage.ReadMessage<PlayerDetailsMessage>();

        GameObject targetPlayerObject = ClientScene.FindLocalObject(playerDetailsMessage.playerID);
        CustomLobby targetPlayer = targetPlayerObject.GetComponent<CustomLobby>();

        targetPlayer.playerDetails = playerDetailsMessage.CreatePlayerDetails();

    }

    private void UpdatePlayerDetails(bool hasDetails)
    {
        hasPlayerDetails = hasDetails;

        if(hasDetails && isLocalPlayer == false)
        {
            RequestDetails();
            playerCount = NetworkClient.allClients.Count;
        }
    }
}
