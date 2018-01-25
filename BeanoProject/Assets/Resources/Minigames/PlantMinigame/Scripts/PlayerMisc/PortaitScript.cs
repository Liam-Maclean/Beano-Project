using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PortaitScript : MonoBehaviour {

	PlayerInfo localPlayerInfo;
	public int playerIndex;
	public Text playerScoreText;
	public ScoreScriptAnimations animScript;

	public int playerScore = 0;
	public GameObject[] portaitSprites;



	void Start()
	{
		playerScoreText = GetComponentInChildren<Text> ();
		animScript = GetComponentInChildren<ScoreScriptAnimations> ();
	}

	public void IncrementScore(int value)
	{
		animScript.PlayScoreIncreaseAnimation ();
		playerScore += value;
	}


	void Update()
	{
		playerScoreText.text = "Score: " + playerScore;
	}

}
