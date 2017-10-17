﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlantGrid : MonoBehaviour{

	private GameObject [,] m_tileGrid;
	private GameObject[,] m_plantGrid;
	private GameObject parent;
	private GameObject plantParent;
	//public int width,height;

	public void CreateGrd(int width, int height, int backgroundWidth, int backgroundHeight)
	{
		m_tileGrid = new GameObject[backgroundWidth,backgroundHeight];
		parent = new GameObject ("Tile Parent");
		for (int y = 0; y < backgroundHeight; y++)
		{
			for (int x = 0; x < backgroundWidth; x++)
			{
				m_tileGrid [y,x] = Instantiate (Resources.Load ("Minigames/PlantMinigame/Prefabs/BackgroundTiles"), new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
				m_tileGrid [y, x].transform.SetParent (parent.transform);
			}
		}

		parent.transform.position =  new Vector3 (-(backgroundWidth / 2), -(backgroundHeight / 2), 0.0f);

		m_plantGrid = new GameObject[width,height];
		plantParent = new GameObject ("Plant Parent");
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				m_plantGrid [y,x] = Instantiate (Resources.Load ("Minigames/PlantMinigame/Prefabs/pTile"), new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
				m_plantGrid [y, x].transform.SetParent (plantParent.transform);
			}
		}

		plantParent.transform.position =  new Vector3 (-(width / 2), -(height / 2), 0.0f);

	}
}
