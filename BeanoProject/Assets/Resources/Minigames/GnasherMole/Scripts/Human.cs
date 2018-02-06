using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Human "Mole" class
/// 
/// 
/// 
///	Hit these "moles" and you'll get negative score (for testing purposes)
/// 
/// 
/// 
/// Liam MacLean 20:08 06/02/2018
/// </summary>
public class Human : BaseMole {

	//Initialises on instantiation
	void Start()
	{
		SetScore (-1);
		SetDuration (Random.Range (0.5f, 2.0f));
	}
}
