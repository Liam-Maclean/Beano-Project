﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPlant : BasePlant {

	void Start()
	{
		sr = this.gameObject.GetComponent<SpriteRenderer> ();
		SetScore (3);
	}

	public override void ActivatePlant(bool bTrueFalse)
	{
		base.ActivatePlant(bTrueFalse);
	}
}