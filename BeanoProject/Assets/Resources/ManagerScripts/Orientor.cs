using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientor : MonoBehaviour {

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

	}
}