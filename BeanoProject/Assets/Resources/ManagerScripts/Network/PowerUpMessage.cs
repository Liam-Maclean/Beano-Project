using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PowerUpMessage : MessageBase {

    public int PowerUp;

    public NetworkInstanceId SubjectID;

    public PowerUpMessage() { }
    public PowerUpMessage(int powerUp, NetworkInstanceId subjectID)
    {
        PowerUp = powerUp;
        SubjectID = subjectID;
    }

}
