using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Determiner : MonoBehaviour {

    public Canvas inGame;
    public Canvas hostJoin;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (FindObjectOfType<CustomLobby>())
        {
            inGame.enabled = true;
            hostJoin.enabled = false;
        }
	}
}
