using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Portrait Script
/// 
/// Contains score and sprites for characters chosen in the game
/// 
/// can be obtained and manipulated with getcomponent system
/// 
///  Liam MacLean - 17/02/2018 16:08
/// </summary>
public class PortaitScript : MonoBehaviour {

	PlayerInfo localPlayerInfo;
	public int playerIndex;
	public Text playerScoreText;
	public ScoreScriptAnimations animScript;

	private int playerScore = 0;
	public GameObject[] portaitSprites;

	//get the score from the portrait script
	public int GetScore()
	{
		return playerScore;
	}

	//start function
	void Start()
	{
		playerScoreText = GetComponentInChildren<Text> ();
		animScript = GetComponentInChildren<ScoreScriptAnimations> ();
	}

	//increment score for text in child object
	public void IncrementScore(int value)
	{
		animScript.PlayScoreIncreaseAnimation ();
		playerScore += value;
	}

	//update function
	void Update()
	{
		//update score text in child
		playerScoreText.text = "Score: " + playerScore;
	}
}
