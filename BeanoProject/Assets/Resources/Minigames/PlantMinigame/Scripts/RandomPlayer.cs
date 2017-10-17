using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlayer : MonoBehaviour {

	SpriteRenderer sr;
	void Awake ()
	{
		sr = this.GetComponent<SpriteRenderer> ();
	}

	// Use this for initialization
	void Start () {
		this.transform.Translate (new Vector3 (1.0f, 1.0f, 0.0f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
	}


}
