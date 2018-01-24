using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameScript : MonoBehaviour {

    [SyncVar]
    int metaScore = 0;

    [SyncVar]
    int miniScore = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void updateMiniScore(int score)
    {
        miniScore = score;
    }

    public void updateMetaScore()
    {
        metaScore += miniScore;
    }
}
