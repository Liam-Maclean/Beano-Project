using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

	public float transitionLength;
	private bool startTransition = false;
	public Canvas canvas;
    float transitionTimer;
	private GameObject transitionPrefab;
	private void Awake()
	{
		DontDestroyOnLoad (transitionPrefab);
		//DontDestroyOnLoad(gameObject);
		DontDestroyOnLoad(canvas);
		canvas.enabled = true;
	}

	// Use this for initialization
	void Start () {
		
	}

	//Instantiates transition Prefab
	public void InstantiateTransitionPrefab(string sceneToChangeTo, LoadSceneMode sceneMode, bool loadAsync)
	{
		transitionPrefab = Instantiate (Resources.Load ("MenuStuff/TransitionPrefab")) as GameObject;
		transitionPrefab.transform.SetParent (canvas.transform);
		transitionPrefab.transform.localPosition = new Vector3 (0, 0, 0);
		transitionPrefab.GetComponent<TransitionManagerScript> ().SetSceneNumberToChangeTo (sceneToChangeTo);
		transitionPrefab.GetComponent<TransitionManagerScript> ().SetLoadSceneMode (sceneMode);
		transitionPrefab.GetComponent<TransitionManagerScript> ().SetLoadAsynchronously (loadAsync);
	
	}


	// Update is called once per frame
	void Update () {
        if (!startTransition)
        {
            transitionTimer = transitionLength;
        }

        else if (startTransition)
        {
            transitionTimer -= Time.deltaTime;
        }
		if (transitionTimer <= 0)
			Transition();
	}

	public void Transition()
	{
		canvas.enabled = true;
		startTransition = !startTransition;
	}
}