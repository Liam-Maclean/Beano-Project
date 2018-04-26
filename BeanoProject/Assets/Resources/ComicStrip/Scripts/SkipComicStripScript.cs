using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Skip comic strip script.
/// 
/// Script for skipping the comic strip animation
/// 
/// Use left mouse button or any touch input to skip the animation
/// 
/// Liam MacLean 26/04/2018, 01:25
/// </summary>
public class SkipComicStripScript : MonoBehaviour {

	//Camera with animation
	public Camera sceneCamera;

	//transition object 
	public SceneTransition transition;

	//animation variables
	private Animator m_animator;
	private AnimatorStateInfo m_animatorInfo;
	bool doOnce = false;


	// Use this for initialization
	void Start () {
		//get the animator from the camera
		m_animator = sceneCamera.GetComponent<Animator> ();	
	}
	
	// Update is called once per frame
	void Update () {
		//if there is any input from the mouse down, or a touch input and it hasn't already been triggered
		if ((Input.GetMouseButtonDown (0) || Input.touchCount > 0) && doOnce == false) {
			//stop the animation
			m_animator.enabled = false;

			//load the next scene with the transition prefab
			transition.InstantiateTransitionPrefab ("Menu", LoadSceneMode.Single, false);

			//make sure to do this only once
			doOnce = true;
		}
	}
}
