using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour {

	StopAnimationScript stopAnimScript;
	// Use this for initialization
	void Start () {
		stopAnimScript = this.GetComponent<StopAnimationScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		//if (stopAnimScript.AnimationEnded ()) {
		//	Destroy (this.gameObject);
		//}
	}
}
