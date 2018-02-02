using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public struct PlayerDetails
{
    public string Handle;
    public int Avatar;
    public NetworkInstanceId Identifier;

    public PlayerDetails(string handle, int avatar, NetworkInstanceId id)
    {
        Handle = handle;
        Avatar = avatar;
        Identifier = id;
    }
}
