using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieAttackScript : MonoBehaviour {


	private GameObject pieSpawner;
	private PieScript pieScript;

	private GameObject pedSpawner;
	private PieThrowManagerScript gameManagerScript;

	private SpriteRenderer sr;
	private PieSpriteChanger pieSpriteManager;

	public Sprite pieSplat;


	private bool isHit;
	private float hitScore;

    // Use this for initialization
    void Start ()
    {
		FloatingTextManager.Initialise ();
		//find the two spawners to acces scripts
        pieSpawner = GameObject.FindGameObjectWithTag("PieSpawner");
		pedSpawner = GameObject.FindGameObjectWithTag ("PedSpawner");

		//get the script and component references
        pieScript = pieSpawner.GetComponent<PieScript>();
		gameManagerScript = pedSpawner.GetComponent<PieThrowManagerScript>();
		sr = gameObject.GetComponent<SpriteRenderer>();
		pieSpriteManager = gameObject.GetComponent<PieSpriteChanger> ();
	
		isHit = false;
   }
	
	// Update is called once per frame
	void Update ()
    {
		if(!isHit)
		{
			if (pieScript.GetLaunched () == true) 
			{
            	//cast a ray out of all the objects and store them in an array
				RaycastHit[] hit = Physics.RaycastAll (gameObject.transform.position, new Vector3 (0.0f, 0.0f, 1.0f), Mathf.Infinity);
            

				for (int i = 0; i < hit.Length; i++)
				{
					
					isHit = true;
                    PedScript pedScript;

					//get the ped script of the object that the pie has collided with
                    pedScript = hit[i].collider.gameObject.GetComponent<PedScript>();

					//get the unique score of the collided object
                    hitScore = pedScript.GetScore();
					//add the score to the player's score
					gameManagerScript.AddScore (hitScore);
					FloatingTextManager.CreateFloatingText (hitScore.ToString(), hit [i].collider.transform, Color.red);


					pieSpriteManager.SetPieSprite (pieSplat);
					//destroy the object the pie has collided with
					Destroy (hit [i].collider.gameObject);
					//respawn the pie
					pieScript.Respawn ();

					//stop the velocity of the pie for animation purposes
					pieScript.SetDistance (new Vector3 (0.0f, 0.0f, 0.0f));
					break;
				}      
			}
		}
	}
}


