using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerRequestPlayerDataMessage : MessageBase {
    /// <summary>
    /// the player sending the message
    /// </summary>
    public NetworkInstanceId SenderID;
    /// <summary>
    /// the player the message concerns
    /// </summary>
    public NetworkInstanceId SubjectID;

    public PlayerRequestPlayerDataMessage() { }
    public PlayerRequestPlayerDataMessage(NetworkInstanceId senderID, NetworkInstanceId subjectID)
    {
        SenderID = senderID;
        SubjectID = subjectID;
    }
}
