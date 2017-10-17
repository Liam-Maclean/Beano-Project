using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour {

	PlantGrid pGrid = new PlantGrid();
	public int height, width;

	void Start()
	{
		pGrid.CreateGrd (width, height);
	}


	void Update()
	{
		OnTileClick ();
	}


	void OnTileClick()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);

			if (hit) {
				if (hit.collider.gameObject.tag == "Plant") {
					hit.transform.GetComponent<PlantScript> ().bActive = false;
				}
			}
		}
	}
}
