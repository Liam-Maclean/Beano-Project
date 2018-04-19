using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Electric Plant 
/// 
/// Extra plant component (not in use)
/// 
/// Liam MacLean 17/02/2018, 16:03
/// </summary>
public class ElectricPlant : BasePlant {

	//initialise values
	void Start()
	{
		//Initialise variables (score and color)
		sr = this.gameObject.GetComponent<SpriteRenderer> ();
		sr.color = new Color (255, 255, 255);
		SetScore (30);
	}
	//override activate plant method (not in use)
	public override void ActivatePlant(out int score)
	{
		base.ActivatePlant (out score);
	}
}
