using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Swap scene behaviour.
/// 
/// Creates the transition between scenes (loads the scene)
/// 
/// can be loaded asynchronously or not, and can define loadmode
/// 
/// Liam Maclean 17/04/2018 14:42
/// </summary>
public class SwapSceneBehaviour : StateMachineBehaviour {

	//variables
	GameObject thisGameObject;
	string sceneToTransitionTo;
	LoadSceneMode loadMode;
	bool isAsynchronous;


	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//get this object that has the tag "transition"
		thisGameObject = GameObject.FindGameObjectWithTag ("Transition");

		//Get the values from the transition
		sceneToTransitionTo = thisGameObject.GetComponent<TransitionManagerScript> ().GetSceneNumberToChangeTo();
		loadMode = thisGameObject.GetComponent<TransitionManagerScript> ().GetLoadSceneMode ();
		isAsynchronous = thisGameObject.GetComponent<TransitionManagerScript> ().GetLoadAsynchronously ();

		//if it is loaded Asynchronously
		if (isAsynchronous) {
			//load scene asynchronously
			SceneManager.LoadSceneAsync(int.Parse(sceneToTransitionTo), loadMode);
		}
		else
		{
			//load scene normally
			SceneManager.LoadScene (sceneToTransitionTo, loadMode);
		}
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//on the next frame (this has to be called on the next frame as the active scene is can only be changed after the first frame)
		if (sceneToTransitionTo == "4")
			SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(4));
		if (sceneToTransitionTo == "5")
			SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(5));
		if (sceneToTransitionTo == "6")
			SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(6));
		if (sceneToTransitionTo == "7")
			SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(7));
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
