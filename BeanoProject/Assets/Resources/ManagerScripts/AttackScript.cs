using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///===============================
///This script will attack the enemy and the enemies health is represented by the transparency
/// 
///of the enemy might be a cool effect we want to explore
/// 
///(may not be the final functionality of the enemies health).
/// 
/// I can't figure out why but both enemies share alpha values so if you hit one they both "lose health"
/// 
/// This is the foundation of the mechanic for the minigame and will be refined to create a polished prototype
/// 
/// Some other features may include:
/// 
/// Power-ups affecting the number of hits or what hitting the enemy does
/// Power-downs affecting number of hits or what hitting the enemy does
/// Different type of touches; swipes, multiple touch inputs etc



public class AttackScript : MonoBehaviour 
{
	//Touch variable
	private Vector2 m_touch;
	//"Health"
	private float transparency;
	//Color variable to change the alpha value
	private Color spriteColor; 

	//Bool variable to declare if the enemy is dead or not
	private bool dead;

	//Error with spawnPos with the EnemySpawnScript being shared among multiple objects same with dead 
	// bool will have to look further into passing variables safely between scripts for now just hard-code
	// the spawn posiiton in each script for now.
	public Vector2 spawnPos;

	// Use this for initialization
	void Start ()
	{
		//Set the enemy to "alive"
		dead = false;
		transparency = 1.0f;

	}
	
	// Update is called once per frame
	void Update () 
	{
		HitEnemy ();	
	}



	//Checks for a touch on the enemy and responds by deducting health
	void HitEnemy()
	{
		//if there is touch input
		if (Input.touchCount == 1)
		{
			//get that touch input
			Touch touch = Input.GetTouch(0);

			switch (touch.phase)
			{
			//if the touch has began
			case TouchPhase.Began:
				{
					//store that start position
					m_touch = touch.position;
				}
				break;

				//if the touch has ended
			case TouchPhase.Ended:
				{
					var ray2 = Camera.main.ScreenPointToRay(m_touch);
					//check for multiple hits from a raycast and store them
					RaycastHit2D[] hit = Physics2D.RaycastAll(ray2.origin, m_touch);

					//for everything hit by the raycast
					for (int i = 0; i < hit.Length; i++)
					{
						//Check if any of the hits are enemies if they are
						if (hit [i].collider.gameObject) 
						{
							
							//Lower the alpha making the enemy less opaque
							transparency -= 0.2f;
							//Set the the color values to the same as the gameobject 
							spriteColor = gameObject.GetComponent<SpriteRenderer>().color;

							//Reduces the alpha value 
							spriteColor = new Color (1f, 1f, 1f, transparency);
							//Sets the new color value
							gameObject.GetComponent<SpriteRenderer> ().color = spriteColor;
						}
					}

					//After x hits respawns the enemy
					if (transparency <= 0.41f)
					{
						//Enemy is dead 
						dead = true;

					}
					if (dead == true)
					{
						//Reset the enemies position
						gameObject.transform.position = spawnPos;

						//turn alpha value back to 1.0f
						transparency += 0.6f;
						//Increase the alpha value
						spriteColor = new Color (1f, 1f, 1f, transparency);
						//Sets the new color value
						gameObject.GetComponent<SpriteRenderer> ().color = spriteColor;

						//Enemy is alive
						dead = false;
							
					}
				}
				break;
			}
		}
	}

	void HitEnemyMouse()
	{


	}

}
	