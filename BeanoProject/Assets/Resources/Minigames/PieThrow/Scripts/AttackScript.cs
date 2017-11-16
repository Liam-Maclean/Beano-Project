using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///===================================================================
///
///(may not be the final functionality of the enemies health).
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
	//Access enemy manager script
	EnemyManagerScript enemyManager;

	public GameObject pie;
	private int currentHealth;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.touchCount == 1) {
			//get that touch input
			Touch touch = Input.GetTouch (0);

			switch (touch.phase) {
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
                    //Get the position of the touch 
					Vector2 objPos = Camera.main.ScreenToWorldPoint (m_touch);
                    //set the pie gameObject to the position of the touch
					pie.transform.position = objPos;
                    //handles the mechanics of the touch
					OnTouch ();
				}
				break;


			}
		}
	}

	//Checks for a touch on the enemy and responds by deducting health
	void OnTouch()
	{
        //Raycast
		Ray ray = Camera.main.ScreenPointToRay (m_touch);
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);

        //If there is a hit 
		if (hit)
		{
            //Checkt the gameObject's tag
			if (hit.collider.gameObject.tag == "Enemy")
			{
                //Get access to the gameObject's enemyManager script
				enemyManager = hit.collider.gameObject.GetComponent<EnemyManagerScript>();
				// - health
				currentHealth = enemyManager.GetHealth ();
				currentHealth--;
                //Set the new health
				enemyManager.SetHealth (currentHealth);


				//Check if enemy is dead
				if (enemyManager.GetHealth () <= 0) 
				{
					enemyManager.SetIsDead (true);
				}

				//If enemy is dead reset
				if (enemyManager.GetIsDead () == true) 
                {
					//Reset the enemies position
					hit.collider.gameObject.transform.position = enemyManager.GetSpawnPos ();
					//Reset enemies health
					enemyManager.ResetHealth ();
					//Set enemy to alive
					enemyManager.SetIsDead (false);
				}
			}
		}
	}
}

