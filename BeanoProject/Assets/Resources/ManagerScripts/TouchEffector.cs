using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffector : MonoBehaviour {

	public float existence;

	// Use this for initialization
	void Start () {

		Destroy(gameObject, existence);
	}

	// Update is called once per frame
	void Update () {

	}
}