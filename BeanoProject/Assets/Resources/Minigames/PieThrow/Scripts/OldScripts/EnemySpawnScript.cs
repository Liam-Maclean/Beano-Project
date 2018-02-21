using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ======================================
/// 
/// Enemy spawn script.
/// 
/// This will organise all the ways the enemy can spawn
/// 
/// Some ideas may include:
/// 
/// Random spawn locations
/// Timer on respawning
/// Spawning with different attributes
/// This is a very basic script intended to showcase the general mechanic of this minigame






public class EnemySpawnScript : MonoBehaviour {

	//Enemy manager object
	EnemyManagerScript enemyManager;

	// Use this for initialization
	void Start () 
	{
		//Access the enemy manager script
		enemyManager = gameObject.GetComponent<EnemyManagerScript>();
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		//If the enemy manages to go out of camera view respawn it
		if (gameObject.transform.position.x > enemyManager.boundPos.x || gameObject.transform.position.y > enemyManager.boundPos.y)
		{
			//Set the enemy to dead
			enemyManager.SetIsDead (true);
		}

		//If the player manages to kill the enemy respawn it
		if (enemyManager.GetIsDead() == true) 
		{
			//reset the enemies transform
			Spawn ();
			//Set the enemy to alive
			enemyManager.SetIsDead (false);
		}
	}


	void Spawn()
	{
		//reset the enemies position
		gameObject.transform.position = enemyManager.GetSpawnPos();
	}
}
