using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerDetailsMessage : MessageBase {

    public NetworkInstanceId playerID;

    public string playerHandle;
    public int playerAvatar;

    public PlayerDetailsMessage() { }
    public PlayerDetailsMessage(NetworkInstanceId playerNetId, PlayerDetails playerDetails)
    {
        playerID = playerNetId;
        playerHandle = playerDetails.Handle;
        playerAvatar = playerDetails.Avatar;
    }

    public PlayerDetails CreatePlayerDetails()
    {
        return new PlayerDetails(playerHandle, playerAvatar, playerID);
    }
}
