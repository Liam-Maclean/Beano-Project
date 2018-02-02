using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GamePlayerDetailsMessage : MessageBase {

    public NetworkInstanceId playerID;

    public string playerHandle;
    public int playerAvatar;

    public int mini;
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

    public MinigamePlayerDetails CreatePlayerDetails()
    {
        return new MinigamePlayerDetails(mini, meta, playerID, playerHandle, playerAvatar);
    }

}
