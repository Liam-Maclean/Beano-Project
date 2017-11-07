using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour {

	public float transitionLength;
	private bool startTransition = false;
	public Canvas canvas;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		DontDestroyOnLoad(canvas);
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		float transitionTimer = transitionLength;

		if (startTransition)
			transitionTimer -= Time.deltaTime;

		if (transitionTimer <= 0)
			Transition();
	}

	public void Transition()
	{
		canvas.enabled = !canvas.enabled;
		startTransition = !startTransition;
	}
}