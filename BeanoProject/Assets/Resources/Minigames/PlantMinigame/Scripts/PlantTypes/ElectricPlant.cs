using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPlant : BasePlant {

	void Start()
	{
		sr = this.gameObject.GetComponent<SpriteRenderer> ();
		sr.color = new Color (255, 255, 255);
		SetScore (3);
	}

	public override void ActivatePlant(out int score)
	{
		score = GetScore ();
	}
}
