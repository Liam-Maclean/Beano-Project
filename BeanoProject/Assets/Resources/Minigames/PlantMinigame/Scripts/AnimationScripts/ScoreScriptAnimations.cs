using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// score script animations
/// 
/// script used to trigger animation for the score increasing
/// 
/// Liam MacLean 17/02/2018, 15:58
/// </summary>

public class ScoreScriptAnimations : MonoBehaviour {

	//animation
	private Animator anim;

	//start function
	void Start()
	{
		anim = this.GetComponent<Animator> ();
	}

	//play animation method
	public void PlayScoreIncreaseAnimation()
	{
		//trigger
		anim.SetTrigger ("ScoreIncrease");
	}


}
