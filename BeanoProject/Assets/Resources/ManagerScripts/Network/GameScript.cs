using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameScript : NetworkBehaviour {

    //when HasPlayerDetails changes on the client, execute UpdatePlayerDetails
    [SyncVar(hook = "UpdatePlayerDetails")]
    public bool hasPlayerDetails = false;
    [SyncVar]
    public int playerCount;

    public MinigamePlayerDetails playerDetails;

    /// <summary>
    /// local player info can be found on every object with this script in the scene
    /// </summary>
    public static GameScript local { get; set; }
    

    /// <summary>
    /// associate our custom message types with a function on the server when running the base OnStartServer() function
    /// </summary>
    public override void OnStartServer()
    {
        base.OnStartServer();

        //associate our custom message types with a function on the server
        NetworkServer.RegisterHandler(CustomMsgType.HostRecievePlayerDetails, OnHostRecievePlayerDetails);
        NetworkServer.RegisterHandler(CustomMsgType.ClientRequestPlayerDetails, OnClientRequestPlayerDetails);
    }

    /// <summary>
    /// associate our custom message type with a function on the client when running the base OnStartClient() function 
    /// </summary>
    public override void OnStartClient()
    {
        base.OnStartClient();

        //associate our custom message type with a function on the client
        NetworkClient.allClients[0].RegisterHandler(CustomMsgType.ClientRecievePlayerDetails, OnClientRecievePlayerDetails);

        if (hasPlayerDetails)
        {
            RequestDetails();
        }
    }


    /// <summary>
    /// take info already gathered in the lobby and put it into the game
    /// </summary>
    /// <param name="lobbyDetails">details from the lobby</param>
    public void SetDetails(PlayerDetails lobbyDetails)
    {
        local = this;
        //take details we already have from the LobbyPlayer
        local.playerDetails.Avatar = /*lobbyDetails.Avatar*/ 10;
        local.playerDetails.Handle = /*lobbyDetails.Handle*/ "Scrublord";
        local.playerDetails.Identifier = lobbyDetails.Identifier;

        //request our details now that we have it
        SendCachedDetailRequests();

        //send local details to the server
        SendDetails(new MinigamePlayerDetails(local.playerDetails.MiniScore, local.playerDetails.MetaScore, local.playerDetails.Identifier, local.playerDetails.Handle, local.playerDetails.Avatar));
    }
    /// <summary>
    /// a list of details requests by ID
    /// </summary>
    private static List<NetworkInstanceId> cachedRequestIDs;

    /// <summary>
    /// request the player's own details from the server
    /// </summary>
    [Client]
    private void RequestDetails()
    {
        //ignore the host
        if (!isServer)
        {
            //don't request details if we haven't made them yet
            if (GameScript.local == null)
            {
                if (cachedRequestIDs == null)
                {
                    cachedRequestIDs = new List<NetworkInstanceId>();
                }
                cachedRequestIDs.Add(netId);
            }
            else
            {
                //request our details
                SendDetailsRequestForNetId(netId);
            }
        }
    }
    /// <summary>
    /// send any details requests we had to put off
    /// </summary>
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
    /// <summary>
    /// reuest details for a specific ID
    /// </summary>
    /// <param name="requestedID">the ID details are being requested for</param>
    private void SendDetailsRequestForNetId(NetworkInstanceId requestedID)
    {
        NetworkClient.allClients[0].Send(CustomMsgType.ClientRequestPlayerDetails, new PlayerRequestPlayerDataMessage(CustomLobby.local.netId, requestedID));
    }
    /// <summary>
    /// send details to the host
    /// </summary>
    /// <param name="playerDetailsTemp">the details to be sent</param>
    private void SendDetails(MinigamePlayerDetails playerDetailsTemp)
    {
        NetworkClient.allClients[0].Send(CustomMsgType.HostRecievePlayerDetails, new GamePlayerDetailsMessage(netId, playerDetailsTemp));
    }
    /// <summary>
    /// when the host recieves player details, update the player with them
    /// </summary>
    /// <param name="netMessage">the message with the details</param>
    private void OnHostRecievePlayerDetails(NetworkMessage netMessage)
    {
        GamePlayerDetailsMessage playerDetailsMessage = netMessage.ReadMessage<GamePlayerDetailsMessage>();

        //find the player that sent the message
        GameObject sendingPlayerObject = NetworkServer.FindLocalObject(playerDetailsMessage.playerID);
        GameScript sendingPlayer = sendingPlayerObject.GetComponent<GameScript>();

        //change their syncvar attached to the details struct
        sendingPlayer.hasPlayerDetails = true;
        //update their playerDetails struct with the updated details
        sendingPlayer.playerDetails = playerDetailsMessage.CreatePlayerDetails();
    }
    /// <summary>
    /// when a client requests details about the player, get the details and send them back
    /// </summary>
    /// <param name="netMessage">the message requesting the details</param>
    private void OnClientRequestPlayerDetails(NetworkMessage netMessage)
    {
        PlayerRequestPlayerDataMessage requestedMessage = netMessage.ReadMessage<PlayerRequestPlayerDataMessage>();

        NetworkInstanceId senderID = requestedMessage.SenderID;
        NetworkInstanceId subjectID = requestedMessage.SubjectID;

        //find the player data is being requested for
        GameObject subjectPlayerObject = NetworkServer.FindLocalObject(subjectID);
        GameScript subjectPlayer = subjectPlayerObject.GetComponent<GameScript>();

        //send updated details about the subject to the sender
        NetworkServer.SendToClient(int.Parse(senderID.ToString()), CustomMsgType.ClientRecievePlayerDetails, new GamePlayerDetailsMessage(subjectID, subjectPlayer.playerDetails));
    }
    /// <summary>
    /// client recieves player details message, update that player
    /// </summary>
    /// <param name="netMessage"> the recieved message</param>
    private void OnClientRecievePlayerDetails(NetworkMessage netMessage)
    {
        GamePlayerDetailsMessage playerDetailsMessage = netMessage.ReadMessage<GamePlayerDetailsMessage>();

        //find the player to be updated
        GameObject targetPlayerObject = ClientScene.FindLocalObject(playerDetailsMessage.playerID);
        GameScript targetPlayer = targetPlayerObject.GetComponent<GameScript>();
        //update details
        targetPlayer.playerDetails = playerDetailsMessage.CreatePlayerDetails();

    }
    /// <summary>
    ///update score and send new details
    ///</summary>
    ///<param name="scoreChange">the amount to change the player's score by</param>
    public void Score(int scoreChange)
    {
        local.playerDetails.MiniScore += scoreChange;
        SendDetails(local.playerDetails);
    }

    /// <summary>
    /// syncvar hook to keep a copy of each player's details on each client
    /// </summary>
    /// <param name="hasDetails"></param>
    private void UpdatePlayerDetails(bool hasDetails)
    {
        hasPlayerDetails = hasDetails;

        if (hasDetails && isLocalPlayer == false)
        {
            RequestDetails();
            playerCount = NetworkClient.allClients.Count;
        }
    }

    /// <summary>
    /// update score in overworld and reset minigame score for the next one, then send
    /// </summary>
    public void EndMiniGame()
    {
        local.playerDetails.MetaScore += local.playerDetails.MiniScore;
        local.playerDetails.MiniScore = 0;
        SendDetails(local.playerDetails);
    }

}
