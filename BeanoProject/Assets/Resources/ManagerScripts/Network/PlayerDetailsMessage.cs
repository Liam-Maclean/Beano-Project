using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerDetailsMessage : MessageBase {
    /// <summary>
    /// the unique ID of the sender
    /// </summary>
    public NetworkInstanceId playerID;
    /// <summary>
    /// the handle of the sender
    /// </summary>
    public string playerHandle;
    /// <summary>
    /// the avatar of the sender
    /// </summary>
    public int playerAvatar;

    public PlayerDetailsMessage() { }
    public PlayerDetailsMessage(NetworkInstanceId playerNetId, PlayerDetails playerDetails)
    {
        playerID = playerNetId;
        playerHandle = playerDetails.Handle;
        playerAvatar = playerDetails.Avatar;
    }
    /// <summary>
    /// create a new playerDetails struct
    /// </summary>
    /// <returns>this new struct to replace the old one</returns>
    public PlayerDetails CreatePlayerDetails()
    {
        return new PlayerDetails(playerHandle, playerAvatar, playerID);
    }
}
