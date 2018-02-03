using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public struct PlayerDetails
{
    /// <summary>
    /// player name
    /// </summary>
    public string Handle;
    /// <summary>
    /// player picture
    /// </summary>
    public int Avatar;
    /// <summary>
    /// unique ID
    /// </summary>
    public NetworkInstanceId Identifier;

    public PlayerDetails(string handle, int avatar, NetworkInstanceId id)
    {
        Handle = handle;
        Avatar = avatar;
        Identifier = id;
    }
}
