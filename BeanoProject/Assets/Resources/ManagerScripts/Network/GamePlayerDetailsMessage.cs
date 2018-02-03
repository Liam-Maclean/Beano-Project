using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GamePlayerDetailsMessage : MessageBase {

    /// <summary>
    /// unique identifier
    /// </summary>
    public NetworkInstanceId playerID;

    /// <summary>
    /// the name of the player
    /// </summary>
    public string playerHandle;
    //the player's picture
    public int playerAvatar;

    /// <summary>
    /// the player's minigame score
    /// </summary>
    public int mini;
    /// <summary>
    /// the player's overwold score
    /// </summary>
    public int meta;


    public GamePlayerDetailsMessage() { }
    public GamePlayerDetailsMessage(NetworkInstanceId playerNetId, MinigamePlayerDetails playerDetails)
    {
        playerID = playerNetId;
        playerHandle = playerDetails.Handle;
        playerAvatar = playerDetails.Avatar;
        mini = playerDetails.MiniScore;
        meta = playerDetails.MetaScore;
    }

    /// <summary>
    /// create a new struct with the player information
    /// </summary>
    /// <returns>the new struct to replace the old one</returns>
    public MinigamePlayerDetails CreatePlayerDetails()
    {
        return new MinigamePlayerDetails(mini, meta, playerID, playerHandle, playerAvatar);
    }

}
