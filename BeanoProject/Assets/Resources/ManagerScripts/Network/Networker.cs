using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Networker : NetworkBehaviour {

	public int persistentCurrency;
    public string handle;
    public static int avatar;

    private void Awake()
	{
		//keep stuff alive, unsure if neccessary, better safe than sorry until researched and tested
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		//get stored value for currency
		persistentCurrency = PlayerPrefs.GetInt("Currency");

        // get character portrait index and handle
        handle = PlayerPrefs.GetString("handle");
        avatar = PlayerPrefs.GetInt("Avatar");
    }

	// Update is called once per frame
	void Update () {

	}

    [ClientRpc]
    public void RpcLoadGame(int sceneIndex)
    {
        //SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        FindObjectOfType<OverworldScript>().SceneToUnload = sceneIndex;
    }

    //to be used both when purchasing and being rewarded, changes currency by passed amount
    void UpdatePersistentCurrency(int change)
	{
		persistentCurrency += change;
		PlayerPrefs.SetInt("Currency", persistentCurrency);
	}
}