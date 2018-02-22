using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyDiscovery : NetworkDiscovery {

    

    public void Host()
    {
        base.StartAsServer();
    }

    public void Join()
    {
        base.StartAsClient();
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        this.GetComponent<NLM>().FoundGame(fromAddress);
    }
}
