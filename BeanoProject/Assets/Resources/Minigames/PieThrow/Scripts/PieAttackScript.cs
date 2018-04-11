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
                    pieScript.SetHit(isHit);
                    PedScript pedScript;

					//get the ped script of the object that the pie has collided with
                    pedScript = hit[i].collider.gameObject.GetComponent<PedScript>();

					//get the animator component to transition the animation states
					Animator pedAnimator = hit [i].collider.gameObject.GetComponent<Animator> ();
                    AudioSource pedSound = hit[i].collider.gameObject.GetComponent<AudioSource>();

                    pedSound.Play();

					pedAnimator.Play ("Impact");
					//stop the move speed to allow the animation to play
					pedScript.SetMoveSpeed (0.0f);
					//add a delay to the destruction of the enemy to allow for the animation to play
					Destroy (hit [i].collider.gameObject, 1.0f);
	       

					//get the unique score of the collided object
					hitScore = pedScript.GetScore ();
					//add the score to the player's score
					gameManagerScript.AddScore (hitScore);
					FloatingTextManager.CreateFloatingText (hitScore.ToString (), hit [i].collider.transform, Color.red);

					//respawn the pie
					pieScript.Respawn ();
                    pieScript.Destroy();
					//stop the velocity of the pie for animation purposes
					pieScript.SetDistance (new Vector3 (0.0f, 0.0f, 0.0f));
					break;
				}      
			}
                     
            pieScript.SetHit(isHit);
		}
	}
}


