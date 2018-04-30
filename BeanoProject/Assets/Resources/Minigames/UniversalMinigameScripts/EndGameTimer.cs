using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This is a standard timer for the end game screen
public class EndGameTimer : MonoBehaviour {

    public float timerValue;
    public Text timerText;

    private GameObject overworldManager;
    private OverworldScript overworldScript;

	// Use this for initialization
	void Start ()
    {
        overworldManager = GameObject.FindGameObjectWithTag("GameManager");
        overworldScript = overworldManager.GetComponent<OverworldScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //decrease the time value
        timerValue -= Time.deltaTime;


        //convert to integer
        int tempTime = (int)timerValue;

        //set & display the current time in the scene
        timerText.text = tempTime.ToString();

        
        //if timer = 0 then return to menu
        if (timerValue < 0.0f)
        {
            overworldScript.EndOverworld();
        }

    }
}
