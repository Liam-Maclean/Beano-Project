using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlantGrid : MonoBehaviour{

	private GameObject [,] m_tileGrid;
	private GameObject parent;
	//public int width,height;

	public void CreateGrd(int width, int height)
	{
		m_tileGrid = new GameObject[width,height];
		parent = new GameObject ("Tile Parent");
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				m_tileGrid [y,x] = Instantiate (Resources.Load ("Minigames/PlantMinigame/Prefabs/pTile"), new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
				m_tileGrid [y, x].transform.SetParent (parent.transform);
			}
		}

		parent.transform.position =  new Vector3 (-(width / 2), -(height / 2), 0.0f);

	}
}
