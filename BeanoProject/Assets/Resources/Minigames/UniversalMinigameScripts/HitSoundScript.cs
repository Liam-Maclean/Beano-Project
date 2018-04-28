using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSoundScript : MonoBehaviour {


	private AudioSource hitSound;



	// Use this for initialization
	void Start () 
	{
		hitSound = this.gameObject.GetComponent<AudioSource> ();
	}
	
		
	public void HitPlay()
	{
		hitSound.Play ();
	}
}
