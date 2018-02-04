using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffPlant : BasePlant {

	void Start()
	{
		sr = this.gameObject.GetComponent<SpriteRenderer> ();
		SetScore (3);
	}

	public override void ActivatePlant(out int score)
	{
		base.ActivatePlant (out score);
	}
}
