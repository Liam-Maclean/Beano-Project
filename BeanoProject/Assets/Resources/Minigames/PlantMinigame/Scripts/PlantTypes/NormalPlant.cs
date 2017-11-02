using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;


public class NormalPlant : BasePlant {

	public int score = 1;

		
	void Start()
	{
		sr = this.gameObject.GetComponent<SpriteRenderer> ();

	}

	public override void ActivatePlant()
	{
		base.ActivatePlant ();
	}
}

