using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomMsgType : MsgType {

    public const short HostRecievePlayerDetails = 16;
    public const short ClientRequestPlayerDetails = 17;
    public const short ClientRecievePlayerDetails = 18;

}
