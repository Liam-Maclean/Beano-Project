using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ======================================
/// 
/// Enemy spawn script.
/// 
/// This will be organise all the ways the enemy can spawn
/// 
/// Some ideas may include:
/// 
/// Random spawn locations
/// Timer on respawning
/// Spawning with different attributes
/// This is a very basic script intended to showcase the general mechanic of this minigame






public class EnemySpawnScript : MonoBehaviour {

	//In future spawn pos will be static so other scripts can access it 
	public Vector2 spawnPos;
	//Coord variable to declare where the boundary is for the enemy
	public Vector2 boundPos;
	private bool dead;


	// Use this for initialization
	void Start () 
	{

		//Set the enemy to "alive"
		dead = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//If the enemy manages to go out of camera view respawn it
		if (gameObject.transform.position.x > boundPos.x || gameObject.transform.position.y > boundPos.y)
		{
			dead = true;
		}

		//If the player manages to kill the enemy respawn it
		if (dead == true) 
		{
			Spawn ();
		}
	}


	void Spawn()
	{
		//reset the enemies position
		gameObject.transform.position = spawnPos;
	}
}
