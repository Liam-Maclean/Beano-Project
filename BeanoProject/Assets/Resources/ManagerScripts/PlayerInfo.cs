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
	int multiplayerIndex;

	//enum storing character chosen
	Character characterChosen = Character.none;

	//score of the player during and after minigames
	int score = 0;

	//earned external currency 
	//THIS WILL LIKELY MOVE SOMEWHERE ELSE
	int earnedCurrency = 0;
}
