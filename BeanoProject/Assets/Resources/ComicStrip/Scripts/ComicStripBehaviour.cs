using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ComicStripBehaviour : StateMachineBehaviour {

	//transition game object 
	public SceneTransition transitionMGR;

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//when the animation has ended, load the main menu
		transitionMGR = GameObject.Find ("Transition").GetComponent<SceneTransition> ();
		transitionMGR.InstantiateTransitionPrefab ("Menu", LoadSceneMode.Single, false);
	}
}
