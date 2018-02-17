﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plant game canvas.
/// 
/// This script Orders the portraits into the correct order on the gamescreen.
/// 
/// Uses a simple sort to change around values (can be changed to a different sort if computationally complex)
/// 
/// Liam MacLean 17/02/2018 15:31
/// </summary>
public class PlantGameCanvas : MonoBehaviour {

	//score list used to calculate who is first
	private List<int> m_scores = new List<int> ();

	//gameobject portraits with tag "Portrait"
	private  GameObject[] m_portraits;

	//portrait script containing all details of the portrait from the gameobjects found
	private List<PortaitScript> m_potraitScripts = new List<PortaitScript> ();

	//positions on the screen for 1st, 2nd, 3rd and 4th
	public Vector3[] m_portraitPositions;


	// Use this for initialization
	void Start () {

		//get gameobjects with the tag portraits in the scene
		m_portraits = GameObject.FindGameObjectsWithTag ("Portrait");

		//gets all the portrait scripts from the portrait objects
		for (int i = 0; i < m_portraits.Length; i++) {
			m_potraitScripts.Add(m_portraits [i].GetComponent<PortaitScript> ());
		}
	}

	//function to reposition portraits ascending order
	void OrderPortraits()
	{
		//get the scores from the portraits and store them into an array
		for (int i = 0; i < m_potraitScripts.Count; i++) {
			m_scores.Add(m_potraitScripts[i].GetScore ());
		}

		//temp potrait script for swapping values
		PortaitScript temp;

		//simple sort algoithm on portraits
		//for every score value in the array
		for (int i = 0; i < m_scores.Count; i++) {
			//for every value above that
			for (int x = i+1; x < m_scores.Count; x++) {
				//if the score is greater than the next value
				if (m_scores [i] < m_scores [x]) {
					//order them properly
					temp = m_potraitScripts [i];
					m_potraitScripts [i] = m_potraitScripts [x];
					m_potraitScripts [x] = temp;
				}
			}
		}

		//reposition potraits and clear scores for recalculation
		m_scores.Clear();
		RepositionPotraits ();
	}


	//reposition the potraits in order of positions passed in
	void RepositionPotraits()
	{
		//for every potrait
		for (int i = 0; i < m_potraitScripts.Count; i++) {
			//reposition the corresponding gameobject
			m_potraitScripts [i].gameObject.transform.localPosition = m_portraitPositions [i];
		}
	}
		
	// Update is called once per frame
	void Update () {
		//order the portraits every frame (bit inefficient)
		OrderPortraits ();
	}
}