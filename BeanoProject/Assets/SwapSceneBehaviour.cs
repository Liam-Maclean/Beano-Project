using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapSceneBehaviour : StateMachineBehaviour {

	GameObject thisGameObject;
	string sceneToTransitionTo;
	LoadSceneMode loadMode;
	bool isAsynchronous;


	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		thisGameObject = GameObject.FindGameObjectWithTag ("Transition");
		sceneToTransitionTo = thisGameObject.GetComponent<TransitionManagerScript> ().GetSceneNumberToChangeTo();
		loadMode = thisGameObject.GetComponent<TransitionManagerScript> ().GetLoadSceneMode ();
		isAsynchronous = thisGameObject.GetComponent<TransitionManagerScript> ().GetLoadAsynchronously ();

		Debug.Log (loadMode);
		Debug.Log(sceneToTransitionTo);

		if (isAsynchronous) {

			SceneManager.LoadSceneAsync(int.Parse(sceneToTransitionTo), loadMode);
			Debug.Log(SceneManager.GetActiveScene ().buildIndex);
			Debug.Log ("I got here");
		}
		else
		{
			SceneManager.LoadScene (sceneToTransitionTo, loadMode);
		}

		//Destroy (thisGameObject);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
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
