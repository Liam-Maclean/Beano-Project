using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreMessage : MessageBase {

    public NetworkInstanceId playerID;

    public int score;

    public ScoreMessage() { }
    public ScoreMessage(NetworkInstanceId playerNetID, int scoreChange)
    {
        playerID = playerNetID;
        score = scoreChange;
    }
}
