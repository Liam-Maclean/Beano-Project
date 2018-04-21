using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveLobby : MonoBehaviour {

    public SceneTransition transitionScript;
    public GameObject controller;

	// Use this for initialization
	void Start () {
        transitionScript = FindObjectOfType<SceneTransition>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LeaveGame()
    {
        FindObjectOfType<NLM>().StopHost();
        FindObjectOfType<NLM>().StopClient();
        Network.Disconnect();
        //Destroy(controller);
        transitionScript.InstantiateTransitionPrefab("Menu", LoadSceneMode.Single, false);
    }
}
