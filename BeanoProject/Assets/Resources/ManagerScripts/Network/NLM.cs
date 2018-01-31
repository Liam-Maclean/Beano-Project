using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NLM : NetworkLobbyManager {

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);

        OnStartHost();
    }
}
