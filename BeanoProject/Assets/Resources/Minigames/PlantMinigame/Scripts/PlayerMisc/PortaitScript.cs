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

	private XMLDialogueDatabase m_dialogueSet;
	PlayerInfo localPlayerInfo;
	public int playerIndex;
	public Text playerScoreText;
	public ScoreScriptAnimations animScript;
	private int playerScore = 0;
	public GameObject[] portaitSprites;
	CustomLobby networkPlayerInfo;
	public Sprite[] sprites;


	//get the score from the portrait script
	public int GetScore()
	{
		//if (networkPlayerInfo != null) {
		//	return networkPlayerInfo.playerDetails.MiniScore;
		//} else {
			return playerScore;
		//}

		//return 0;
	}

	//check the portrait is the local player's portrait
	public bool IsLocalPlayerPortrait()
	{
		if (networkPlayerInfo) {
			if (networkPlayerInfo.isLocalPlayer) {
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}


	//start function
	void Start()
	{
		playerScoreText = GetComponentInChildren<Text> ();
		animScript = GetComponentInChildren<ScoreScriptAnimations> ();
	}

	//hands the player's network information (customlobby) from the plantgamecanvas
	public void HandPlayerNetworkLobby(CustomLobby player)
	{
		networkPlayerInfo = player;
		SetSpriteWithCustomLobby (true);
	}

	//sets the sprite with the network player info avatar int
	public void SetSpriteWithCustomLobby(bool usingCustomLobby)
	{
		if (usingCustomLobby) {
			Image sr = GetComponent<Image> ();
			sr.sprite = sprites [(int)networkPlayerInfo.playerDetails.Identifier.Value];
		} else {
			
		}
	}


	//increment score for text in child object
	public void IncrementScore(int value)
	{
		animScript.PlayScoreIncreaseAnimation ();
		//playerScore += value;
		if (networkPlayerInfo) {
			if (networkPlayerInfo.isLocalPlayer) {
				CustomLobby.local.Score (value);
				playerScore += value;
			} 
		} else {
			playerScore += value;
		}

	}

	//update function
	void Update()
	{
		if (networkPlayerInfo) {
			playerScoreText.text = "Score: " + networkPlayerInfo.playerDetails.MiniScore;
		} else {
			playerScoreText.text = "Score: " + playerScore;
		}

	}

	//loads dialogue database
	public void LoadDialogueDatabase()
	{
		m_dialogueSet = new XMLDialogueDatabase ();
		m_dialogueSet = XMLSerializer.Deserialize<XMLDialogueDatabase> ("DialogueFile.xml", "");
	}

	//Spawns dialogue box relative to player portrait position
	public void SpawnDialogueBox()
	{
		//SET UP DIALOGUE BOX SPAWN POINT TO BE REALITVE TO PLAYERS PORTRAIT POSITION
		GameObject box = Instantiate (Resources.Load ("Minigames/PlantMinigame/Prefabs/SpeachBubble")) as GameObject;
		box.transform.SetParent (GameObject.Find ("PlayerPortait").transform);
		box.transform.localPosition = new Vector3 (55, 20, 1.0f);
	}
}
