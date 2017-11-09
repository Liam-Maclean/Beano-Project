using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// ======================================
/// 
/// Enemy movement script
/// This script will handle all the movement of the enemy
/// 
/// This is a very basic script intended for only showcasing the general mechanic of the minigame
/// 
/// Some features may include:
/// 
/// Different movement speeds of enemies
/// Enemies move in an unusual way 




public class EnemyMovementScript : MonoBehaviour {


	public float enemyMovSpeedX;
	public float enemyMovSpeedY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//Move the enemy by a speed the user specifies * by delta time
		gameObject.transform.Translate (enemyMovSpeedX * Time.deltaTime, enemyMovSpeedY * Time.deltaTime ,0);
		
	}
}
