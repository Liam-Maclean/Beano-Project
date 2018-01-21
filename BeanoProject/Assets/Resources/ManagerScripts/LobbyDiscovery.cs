﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyDiscovery : NetworkDiscovery {

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        NetworkLobbyManager.singleton.networkAddress = fromAddress;
        NetworkLobbyManager.singleton.StartClient();
    }
}
