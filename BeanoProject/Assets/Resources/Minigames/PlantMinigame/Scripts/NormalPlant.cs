using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;


public enum PlantType
{
	normal,
	bonus,
	water,
	dirt
}

public class NormalPlant : BasePlant {
	public Sprite[] sprites;
	private SpriteRenderer sr;
	public int score = 1;

		
	void Start()
	{
		sr = this.gameObject.GetComponent<SpriteRenderer> ();

	}

	public override void ActivatePlant()
	{
		score++;
		base.ActivatePlant ();
	}
}

