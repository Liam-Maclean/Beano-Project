using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieAttackScript : MonoBehaviour {

	PieScript pieScript;



    // Use this for initialization
    void Start ()
    {   
        //variable to access the pieScript
		pieScript = gameObject.GetComponent<PieScript>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
		if (pieScript.GetLaunched () == true) 
		{
            //cast a ray out of all the objects and store them in an array
			RaycastHit[] hit = Physics.RaycastAll (gameObject.transform.position, new Vector3 (0.0f, 0.0f, 1.0f), Mathf.Infinity);

            
			for (int i = 0; i < hit.Length; i++)
			{
                //if there has been a collision 
				pieScript.SetDestroyed (true);
                //destroy the object the pie has collided with
				Destroy (hit [i].collider.gameObject);
                //destroy the pie
				Destroy (gameObject);
			}
		}
        





	}

}
