using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitSpawner : MonoBehaviour {

    public GameObject pPrefab;

    List<GameObject> clones = new List<GameObject>();

    int index = 0;

    Vector3 spawnPoint;
    
    // Use this for initialization
	void Start () {
        spawnPoint = new Vector3(-6, 2, -1);
	}
	
	// Update is called once per frame
	void Update () {
        index = 0;
        CustomLobby[] players = FindObjectsOfType<CustomLobby>();
        Check:;
        if (players.Length > clones.Count)
        {
            clones.Add(Instantiate(pPrefab,spawnPoint,Quaternion.identity));
            spawnPoint.x += 4;
            goto Check;
        }
        else if (players.Length < clones.Count)
        {
            clones.RemoveAt(clones.Count - 1);
            spawnPoint.x -= 4;
            goto Check;
        }
        foreach(CustomLobby player in players)
        {
            clones[index].GetComponent<LobbyPortaitScript>().portrait.GetComponent<SpriteRenderer>().sprite = clones[index].GetComponent<LobbyPortaitScript>().portraitList[player.playerDetails.Avatar];
            if (player.readyToBegin)
            {
                clones[index].GetComponent<LobbyPortaitScript>().readyStatus.GetComponent<SpriteRenderer>().sprite = clones[index].GetComponent<LobbyPortaitScript>().readyList[1];
            }
            else
            {
                clones[index].GetComponent<LobbyPortaitScript>().readyStatus.GetComponent<SpriteRenderer>().sprite = clones[index].GetComponent<LobbyPortaitScript>().readyList[0];
            }
            ++index;
        }
	}
}
