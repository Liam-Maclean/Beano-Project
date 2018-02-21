using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mole class
/// 
///	Hit these "moles" and you'll get positive score
/// 
/// Liam MacLean 20:09 06/02/2018
/// </summary>
public class Mole : BaseMole {

	//Initialises on instantiation
	void Start()
	{
		SetScore (1);
		SetDuration(Random.Range(0.5f, 2.0f));
	}
}
