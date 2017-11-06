using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum for the characters chosen
public enum Character
{
	none,
	Dennis,
	Gnasher,
	DennisDad
}
	
//to retrieve this info, GameObject.getComponent should be used
public class PlayerInfo : MonoBehaviour{
	//multiplayer index for each player
	public int multiplayerIndex;

	//enum storing character chosen
	public Character characterChosen = Character.none;

	//score of the player during and after minigames
	public int score = 0;

	//earned external currency 
	//THIS WILL LIKELY MOVE SOMEWHERE ELSE
	public int earnedCurrency = 0;
}
