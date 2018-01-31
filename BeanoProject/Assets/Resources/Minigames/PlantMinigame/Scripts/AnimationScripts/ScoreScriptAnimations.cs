using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScriptAnimations : MonoBehaviour {

	private Animator anim;

	void Start()
	{
		anim = this.GetComponent<Animator> ();
	}

	public void PlayScoreIncreaseAnimation()
	{
		anim.SetTrigger ("ScoreIncrease");
	}


}
