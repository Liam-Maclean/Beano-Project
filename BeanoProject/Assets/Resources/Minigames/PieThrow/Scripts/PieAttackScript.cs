using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieAttackScript : MonoBehaviour {

	PieScript pieScript;



    // Use this for initialization
    void Start ()
    {   
		pieScript = gameObject.GetComponent<PieScript>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
		if (pieScript.GetLaunched () == true) 
		{

			RaycastHit[] hit = Physics.RaycastAll (gameObject.transform.position, new Vector3 (0.0f, 0.0f, 1.0f), Mathf.Infinity);

			for (int i = 0; i < hit.Length; i++)
			{
				pieScript.SetDestroyed (true);
				Destroy (hit [i].collider.gameObject);
				Destroy (gameObject);
			}
		}
        





	}

}
