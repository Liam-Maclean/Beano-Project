using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerDetails
{
    public string Handle;
    public int Avatar;

    public PlayerDetails(string handle, int avatar)
    {
        Handle = handle;
        Avatar = avatar;
    }
}
