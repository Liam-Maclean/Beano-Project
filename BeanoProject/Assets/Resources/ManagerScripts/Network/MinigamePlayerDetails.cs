using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public struct MinigamePlayerDetails  {
    /// <summary>
    /// score in minigames
    /// </summary>
    public int MiniScore;
    /// <summary>
    /// score in overworld
    /// </summary>
    public int MetaScore;
    //unique ID
    public NetworkInstanceId Identifier;
    /// <summary>
    /// in game name
    /// </summary>
    public string Handle;
    /// <summary>
    /// in game picture
    /// </summary>
    public int Avatar;

    public MinigamePlayerDetails(int mini, int meta, NetworkInstanceId id, string handle, int avatar)
    {
        MiniScore = mini;
        MetaScore = meta;
        Identifier = id;
        Handle = handle;
        Avatar = avatar;
    }
}
