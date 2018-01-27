using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerRequestPlayerDataMessage : MessageBase {

    public NetworkInstanceId SenderID;
    public NetworkInstanceId SubjectID;

    public PlayerRequestPlayerDataMessage() { }
    public PlayerRequestPlayerDataMessage(NetworkInstanceId senderID, NetworkInstanceId subjectID)
    {
        SenderID = senderID;
        SubjectID = subjectID;
    }
}
