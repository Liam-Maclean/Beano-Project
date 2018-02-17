using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

/// <summary>
/// Normal Plant script
/// 
/// simplest plant component, adds 1 score
/// 
/// Liam MacLean 17/02/2018, 15:58
/// </summary>
public class NormalPlant : BasePlant {

	//start function
	void Start()
	{
		//initialise values (score and color)
		sr = this.gameObject.GetComponent<SpriteRenderer> ();
		sr.color = new Color (255, 255, 255);
		SetScore (1);
	}

	//override activate plant method (not in use)
	public override void ActivatePlant(out int score)
	{
		score = GetScore ();
	}
}

