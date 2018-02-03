using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomMsgType : MsgType {

    /// <summary>
    /// message where the host recieves details from a player
    /// </summary>
    public const short HostRecievePlayerDetails = 16;
    /// <summary>
    /// message where the client asks for a player's details
    /// </summary>
    public const short ClientRequestPlayerDetails = 17;
    /// <summary>
    /// message where client recieves a player's details
    /// </summary>
    public const short ClientRecievePlayerDetails = 18;

}
