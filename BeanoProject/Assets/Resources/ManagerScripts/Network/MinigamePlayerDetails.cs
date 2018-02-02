using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public struct MinigamePlayerDetails  {

    public int MiniScore;
    public int MetaScore;
    public NetworkInstanceId Identifier;
    public string Handle;
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
