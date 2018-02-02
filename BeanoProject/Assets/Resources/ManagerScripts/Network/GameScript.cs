using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameScript : NetworkBehaviour {

    [SyncVar(hook = "UpdatePlayerDetails")]
    public bool hasPlayerDetails = false;
    [SyncVar]
    public int playerCount;

    MinigamePlayerDetails playerDetails;

    public static GameScript local { get; private set; }

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

        if (hasPlayerDetails)
        {
            RequestDetails();
        }
    }



    public void SetDetails(PlayerDetails lobbyDetails)
    {
        local.playerDetails.Avatar = lobbyDetails.Avatar;
        local.playerDetails.Handle = lobbyDetails.Handle;
        local.playerDetails.Identifier = lobbyDetails.Identifier;

        SendCachedDetailRequests();

        SendDetails(new MinigamePlayerDetails(local.playerDetails.MiniScore, local.playerDetails.MetaScore, local.playerDetails.Identifier, local.playerDetails.Handle, local.playerDetails.Avatar));
    }

    private static List<NetworkInstanceId> cachedRequestIDs;

    [Client]
    private void RequestDetails()
    {
        if (!isServer)
        {
            if (CustomLobby.local == null)
            {
                if (cachedRequestIDs == null)
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
            foreach (NetworkInstanceId requestedID in cachedRequestIDs)
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

    private void SendDetails(MinigamePlayerDetails playerDetailsTemp)
    {
        NetworkClient.allClients[0].Send(CustomMsgType.HostRecievePlayerDetails, new GamePlayerDetailsMessage(netId, playerDetailsTemp));
    }

    private void OnHostRecievePlayerDetails(NetworkMessage netMessage)
    {
        GamePlayerDetailsMessage playerDetailsMessage = netMessage.ReadMessage<GamePlayerDetailsMessage>();

        GameObject sendingPlayerObject = NetworkServer.FindLocalObject(playerDetailsMessage.playerID);
        GameScript sendingPlayer = sendingPlayerObject.GetComponent<GameScript>();

        sendingPlayer.hasPlayerDetails = true;
        sendingPlayer.playerDetails = playerDetailsMessage.CreatePlayerDetails();
    }

    private void OnClientRequestPlayerDetails(NetworkMessage netMessage)
    {
        PlayerRequestPlayerDataMessage requestedMessage = netMessage.ReadMessage<PlayerRequestPlayerDataMessage>();

        NetworkInstanceId senderID = requestedMessage.SenderID;
        NetworkInstanceId subjectID = requestedMessage.SubjectID;

        GameObject subjectPlayerObject = NetworkServer.FindLocalObject(subjectID);
        GameScript subjectPlayer = subjectPlayerObject.GetComponent<GameScript>();

        NetworkServer.SendToClient(int.Parse(senderID.ToString()), CustomMsgType.ClientRecievePlayerDetails, new GamePlayerDetailsMessage(subjectID, subjectPlayer.playerDetails));
    }

    private void OnClientRecievePlayerDetails(NetworkMessage netMessage)
    {
        GamePlayerDetailsMessage playerDetailsMessage = netMessage.ReadMessage<GamePlayerDetailsMessage>();

        GameObject targetPlayerObject = ClientScene.FindLocalObject(playerDetailsMessage.playerID);
        GameScript targetPlayer = targetPlayerObject.GetComponent<GameScript>();

        targetPlayer.playerDetails = playerDetailsMessage.CreatePlayerDetails();

    }

    public void Score(int score)
    {
        local.playerDetails.MiniScore += score;
        SendDetails(local.playerDetails);
    }


    private void UpdatePlayerDetails(bool hasDetails)
    {
        hasPlayerDetails = hasDetails;

        if (hasDetails && isLocalPlayer == false)
        {
            RequestDetails();
            playerCount = NetworkClient.allClients.Count;
        }
    }

}
