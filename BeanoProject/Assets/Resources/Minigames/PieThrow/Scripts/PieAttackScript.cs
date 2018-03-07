using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieAttackScript : MonoBehaviour {


	private GameObject pieSpawner;
	private PieScript pieScript;

	private GameObject pedSpawner;
	private PieThrowManagerScript gameManagerScript;

	public GameObject friendlyTarget;
	private PedScript friendlyPedScript;

	public GameObject enemyTarget;
	private PedScript enemyPedScript;

	private SpriteRenderer sr;
	private bool isHit;
	private float hitScore;



    // Use this for initialization
    void Start ()
    {
        pieSpawner = GameObject.FindGameObjectWithTag("PieSpawner");
		pedSpawner = GameObject.FindGameObjectWithTag ("PedSpawner");


        pieScript = pieSpawner.GetComponent<PieScript>();
		gameManagerScript = pedSpawner.GetComponent<PieThrowManagerScript>();
		friendlyPedScript = friendlyTarget.GetComponent<PedScript> ();
		enemyPedScript = enemyTarget.GetComponent<PedScript> ();

		isHit = false;
		sr = gameObject.GetComponent<SpriteRenderer>();
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
					if (hit [i].collider.tag == "Friendly") 
					{
						hitScore = friendlyPedScript.GetScore ();
					}
					else 
					{
						hitScore = enemyPedScript.GetScore ();
					}

					gameManagerScript.AddScore (hitScore);
					//destroy the object the pie has collided with
					Destroy (hit [i].collider.gameObject);
					pieScript.Respawn ();
					pieScript.Destroy ();
					isHit = true;
					break;
				}               
			}
		}
	}
}


