using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// ==================================================
/// Enemy Manager Script
/// This holds all the info of the enemy which can be defined in the editor
/// Also moves the enemy in a user defined direction
/// Has Getters & Setters to edit the variables


public class EnemyManagerScript : MonoBehaviour
{
    //Checks if the enemy is dead
	private bool isDead;

    //Enemy's health
	private int health;
    //Original health of the enemy
	public int originHealth;
    
    //Enemy's movement speed
	public float enemyMovSpeedX;
	public float enemyMovSpeedY;

    //Enemy's spawn and boundary positions
	[SerializeField]
	private Vector2 spawnPos;
	public Vector2 boundPos;

	// Use this for initialization
	void Start () 
	{
        //Set the enemy to alive
		SetIsDead (false);
        //Get the original health 
		health = originHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Move the enemy by a speed the user specifies * by delta time
		gameObject.transform.Translate (enemyMovSpeedX * Time.deltaTime, enemyMovSpeedY * Time.deltaTime ,0);	
	}
	
    //Resets the enemy's health 
	public void ResetHealth()
	{
		health = originHealth;
	}
	//SETTERS
	public void SetIsDead(bool isdead)
	{
		isDead = isdead;
	}

	public void SetHealth(int Health)
	{
		health = Health;
	}

	//GETTERS
	public bool GetIsDead()
	{
		return isDead;
	}

	public Vector2 GetSpawnPos()
	{
		return spawnPos;
	}

	public Vector2 GetBoundPos()
	{
		return boundPos;
	}

	public int GetHealth()
	{
		return health;
	}

}
