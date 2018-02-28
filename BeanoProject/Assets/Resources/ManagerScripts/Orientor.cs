using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientor : MonoBehaviour {

    public static bool pieThrow = false;

    private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		Screen.orientation = ScreenOrientation.Landscape;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
        if (pieThrow)
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        else
        {
            Screen.orientation = ScreenOrientation.Landscape;
        }
	}
}