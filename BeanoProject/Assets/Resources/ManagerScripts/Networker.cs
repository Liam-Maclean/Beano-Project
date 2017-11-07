using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Networker : MonoBehaviour {

	public int persistentCurrency;

	private void Awake()
	{
		//keep stuff alive, unsure if neccessary, better safe than sorry until researched and tested
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		//get stored value for currency
		persistentCurrency = PlayerPrefs.GetInt("Currency");

		// get character portrait index
	}

	// Update is called once per frame
	void Update () {

	}

	//to be used both when purchasing and being rewarded, changes currency by passed amount
	void UpdatePersistentCurrency(int change)
	{
		persistentCurrency += change;
		PlayerPrefs.SetInt("Currency", persistentCurrency);
	}
}