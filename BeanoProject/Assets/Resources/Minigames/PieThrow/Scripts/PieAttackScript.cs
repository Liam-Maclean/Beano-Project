using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieAttackScript : MonoBehaviour {


    private GameObject pieSpawner;
	private PieScript pieScript;

	private SpriteRenderer sr;
	private bool isHit;


    // Use this for initialization
    void Start ()
    {
        pieSpawner = GameObject.FindGameObjectWithTag("PieSpawner");

        pieScript = pieSpawner.GetComponent<PieScript>();
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


