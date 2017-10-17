using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ManagerScript : MonoBehaviour {

	PlantGrid pGrid = new PlantGrid();
	public int height, width, backgroundHeight, backgroundWidth;

	void Start()
	{
		Screen.orientation = ScreenOrientation.Portrait;
		pGrid.CreateGrd (width, height, backgroundHeight, backgroundWidth);
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
				}
			}
		}
	}
}
