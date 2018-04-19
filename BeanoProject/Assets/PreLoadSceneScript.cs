using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreLoadSceneScript : MonoBehaviour {


	void Awake()
	{
		SceneManager.LoadSceneAsync("MoleControl", LoadSceneMode.Additive);
		SceneManager.LoadSceneAsync("Overworld", LoadSceneMode.Additive);
		SceneManager.LoadSceneAsync("PieThrowScene", LoadSceneMode.Additive);
		SceneManager.LoadSceneAsync("PlantMinigameScene", LoadSceneMode.Additive);
		//SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);
	}
	// Use this for initialization
	void Start () {
		//Load scenes and unload scenes for first time loading

		//SceneManager.LoadSceneAsync("MoleControl", LoadSceneMode.Additive);

		SceneManager.UnloadSceneAsync("MoleControl");
		SceneManager.UnloadSceneAsync("Overworld");
		SceneManager.UnloadSceneAsync("PieThrowScene");
		SceneManager.UnloadSceneAsync("PlantMinigameScene");
		//SceneManager.UnloadSceneAsync("Loading");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
