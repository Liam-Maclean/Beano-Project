using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;


public class NormalPlant : BasePlant {

	void Start()
	{
		sr = this.gameObject.GetComponent<SpriteRenderer> ();
		SetScore (1);
	}

	public override void ActivatePlant(out int score)
	{
		score = GetScore ();
	}
}

