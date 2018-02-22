using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomLobby : NetworkLobbyPlayer {

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    //syncvar will call when UpdatePlayerDetails is called
    [SyncVar(hook = "UpdatePlayerDetails")]
    public bool hasPlayerDetails = false;

    public int playerCount = 0;

    /// <summary>
    /// Active powerup used on player
    /// </summary>
    int effect = 0;

    public MinigamePlayerDetails playerDetails;

    /// <summary>
    /// local data can be found in each PlayerID object
    /// </summary>
    public static CustomLobby local { get; private set; }

    /// <summary>
    /// associate our custom message types with a function on the server when running the base OnStartServer() function
    /// </summary>
    public override void OnStartServer()
    {
        base.OnStartServer();

        NetworkServer.RegisterHandler(CustomMsgType.HostRecievePlayerDetails, OnHostRecievePlayerDetails);
        NetworkServer.RegisterHandler(CustomMsgType.ClientRequestPlayerDetails, OnClientRequestPlayerDetails);
        NetworkServer.RegisterHandler(CustomMsgType.PlayerSendPowerUp, OnPlayerSendPowerUp);
    }

    /// <summary>
    /// associate our custom message type with a function on the client when running the base OnStartClient() function 
    /// </summary>
    public override void OnStartClient()
    {
        base.OnStartClient();

        NetworkClient.allClients[0].RegisterHandler(CustomMsgType.ClientRecievePlayerDetails, OnClientRecievePlayerDetails);

        if(hasPlayerDetails)
        {
            RequestDetails();
        }
    }

    /// <summary>
    /// set values for the local data and send them to the server
    /// </summary>
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        local = this;

        local.playerDetails.Avatar = PlayerPrefs.GetInt("Avatar");
        local.playerDetails.Handle = PlayerPrefs.GetString("Handle");
        local.playerDetails.Identifier = netId;
        local.playerDetails.MetaScore = 0;
        local.playerDetails.MiniScore = 0;

        SendCachedDetailRequests();

        SendDetails(new MinigamePlayerDetails(local.playerDetails.MiniScore, local.playerDetails.MetaScore, local.playerDetails.Identifier, local.playerDetails.Handle, local.playerDetails.Avatar));
    }

    /// <summary>
    /// list of IDs to request details for
    /// </summary>
    private static List<NetworkInstanceId> cachedRequestIDs;

    /// <summary>
    /// request our details from the server
    /// </summary>
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

    /// <summary>
    /// send detail requests we couldn't send previously
    /// </summary>
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

    /// <summary>
    /// reuest details for a specific ID
    /// </summary>
    /// <param name="requestedID">the ID details are being requested for</param>
    public void SendDetailsRequestForNetId(NetworkInstanceId requestedID)
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

        GameObject sendingPlayerObject = NetworkServer.FindLocalObject(playerDetailsMessage.playerID);
        CustomLobby sendingPlayer = sendingPlayerObject.GetComponent<CustomLobby>();

        sendingPlayer.hasPlayerDetails = true;
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

        GameObject subjectPlayerObject = NetworkServer.FindLocalObject(subjectID);
        CustomLobby subjectPlayer = subjectPlayerObject.GetComponent<CustomLobby>();

        NetworkServer.SendToClient(int.Parse(senderID.ToString()), CustomMsgType.ClientRecievePlayerDetails, new GamePlayerDetailsMessage(subjectID, subjectPlayer.playerDetails));
    }

    /// <summary>
    /// client recieves player details message, update that player
    /// </summary>
    /// <param name="netMessage"> the recieved message</param>
    private void OnClientRecievePlayerDetails(NetworkMessage netMessage)
    {
        GamePlayerDetailsMessage playerDetailsMessage = netMessage.ReadMessage<GamePlayerDetailsMessage>();

        GameObject targetPlayerObject = ClientScene.FindLocalObject(playerDetailsMessage.playerID);
        CustomLobby targetPlayer = targetPlayerObject.GetComponent<CustomLobby>();

        targetPlayer.playerDetails = playerDetailsMessage.CreatePlayerDetails();

    }

    private void OnPlayerSendPowerUp(NetworkMessage networkMessage)
    {
        PowerUpMessage message = networkMessage.ReadMessage<PowerUpMessage>();

        NetworkInstanceId subjectID = message.SubjectID;
        int powerUp = message.PowerUp;

        GameObject subjectObject = ClientScene.FindLocalObject(subjectID);
        CustomLobby subjectPlayer = subjectObject.GetComponent<CustomLobby>();

        subjectPlayer.effect = powerUp;
    }

    /// <summary>
    /// syncvar hook to keep a copy of each player's details on each client
    /// </summary>
    /// <param name="hasDetails"></param>
    private void UpdatePlayerDetails(bool hasDetails)
    {
        hasPlayerDetails = hasDetails;

        if(hasDetails && isLocalPlayer == false)
        {
            RequestDetails();
            playerCount = NetworkClient.allClients.Count;
        }
        hasPlayerDetails = false;
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

    /// <summary>
    ///update score and send new details
    ///</summary>
    ///<param name="scoreChange">the amount to change the player's score by</param>
    public void Score(int scoreChange)
    {
        local.playerDetails.MiniScore += scoreChange;
        
        if (isServer)
        {
            GameObject sendingPlayerObject = NetworkServer.FindLocalObject(local.playerDetails.Identifier);
            CustomLobby sendingPlayer = sendingPlayerObject.GetComponent<CustomLobby>();

            sendingPlayer.hasPlayerDetails = true;
            sendingPlayer.playerDetails = local.playerDetails;
        }

        SendDetails(local.playerDetails);
    }

    /// <summary>
    /// send a powerup effect to another player
    /// </summary>
    /// <param name="powerUpType">integer for the powerup used</param>
    /// <param name="subject">The player to send this to, get this by finding that player in the scene and getting CustomLobby.playerDetails.Identifier </param>
    public void PowerUp(int powerUpType, NetworkInstanceId subject)
    {
        NetworkClient.allClients[0].Send(CustomMsgType.PlayerSendPowerUp, new PowerUpMessage(powerUpType, subject));
    }
}
