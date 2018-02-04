using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScorePlant : BasePlant {
	
	void Start()
	{
		sr = this.gameObject.GetComponent<SpriteRenderer> ();
		SetScore (2);
	}

	public override void ActivatePlant(out int score)
    {
		int tempscore;
		tempscore = GetScore () * 2;
		score = tempscore;
    }
}
