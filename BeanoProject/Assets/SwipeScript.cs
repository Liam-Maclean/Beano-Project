using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Destroy swipe on instantiate after 3 seconds
		Destroy (this.gameObject, 3.0f);
	}
}
