using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking.Types;

public class Migrator : NetworkMigrationManager {

    //from NetworkMigrationManager source code, set up globals and make code from gui connect buttons execute automatically on disconnect
    //https://bitbucket.org/Unity-Technologies/networking/src/78ca8544bbf4e87c310ce2a9a3fc33cdad2f9bb1/Runtime/NetworkMigrationManager.cs?at=5.3&fileviewer=file-view-default

    bool m_HostMigration = true;

    NetworkClient m_Client;
    bool m_WaitingToBecomeNewHost;
    bool m_WaitingReconnectToNewHost;
    bool m_DisconnectedFromHost;
    bool m_HostWasShutdown;

    MatchInfo m_MatchInfo;
    int m_OldServerConnectionId = -1;
    string m_NewHostAddress;

    PeerInfoMessage m_NewHostInfo = new PeerInfoMessage();
    PeerListMessage m_PeerListMessage = new PeerListMessage();

    PeerInfoMessage[] m_Peers;

    protected override void OnClientDisconnectedFromHost(NetworkConnection conn, out SceneChangeOption sceneChange)
    {
        base.OnClientDisconnectedFromHost(conn, out sceneChange);
        if (m_WaitingToBecomeNewHost)
        {
            if (NetworkManager.singleton != null)
            {
                BecomeNewHost(NetworkManager.singleton.networkPort);
            }
            else
            {
                Debug.LogWarning("MigrationManager Client BecomeNewHost - No NetworkManager.");
            }
        }
        else if (m_WaitingReconnectToNewHost)
        {
            Reset(m_OldServerConnectionId);

            if (NetworkManager.singleton != null)
            {
                NetworkManager.singleton.networkAddress = m_NewHostAddress;
                NetworkManager.singleton.client.ReconnectToNewHost(m_NewHostAddress, NetworkManager.singleton.networkPort);
            }
            else
            {
                Debug.LogWarning("MigrationManager Client reconnect - No NetworkManager.");
            }
        }
        else
        {
            bool youAreNewHost;
            if (FindNewHost(out m_NewHostInfo, out youAreNewHost))
            {
                m_NewHostAddress = m_NewHostInfo.address;
                if (youAreNewHost)
                {
                    m_WaitingToBecomeNewHost = true;
                }
                else
                {
                    m_WaitingReconnectToNewHost = true;
                }
            }
        }
    }

    protected override void OnServerHostShutdown()
    {
        base.OnServerHostShutdown();
        if (m_WaitingReconnectToNewHost)
        {
            Reset(ClientScene.ReconnectIdHost);

            if (NetworkManager.singleton != null)
            {
                NetworkManager.singleton.StartClient();
            }
            else
            {
                Debug.LogWarning("MigrationManager Old Host Reconnect - No NetworkManager.");
            }
        }
        else
        {
            bool youAreNewHost;
            if (FindNewHost(out m_NewHostInfo, out youAreNewHost))
            {
                m_NewHostAddress = m_NewHostInfo.address;
                if (youAreNewHost)
                {
                    Debug.LogWarning("MigrationManager FindNewHost - new host is self?");
                }
                else
                {
                    m_WaitingReconnectToNewHost = true;
                }
            }
        }
    }
}
