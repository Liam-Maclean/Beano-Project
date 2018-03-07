using System.Collections;
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

	private GameObject[] m_opponents;
	private List<GameObject> opponents = new List <GameObject> ();

	// Use this for initialization
	void Awake () {

		//get gameobjects with the tag portraits in the scene
		m_portraits = GameObject.FindGameObjectsWithTag ("Portrait");
		m_opponents = GameObject.FindGameObjectsWithTag ("Player");


		foreach (GameObject player in m_opponents)
		{
			//if (player.GetComponent<CustomLobby>().playerDetails.Identifier != CustomLobby.local.playerDetails.Identifier)
			//{
				opponents.Add(player);
			//}
		}
		//gets all the portrait scripts from the portrait objects
		//if there are more than 0 opponents
		if (m_opponents.Length > 0) {
			//itterate through portrait objects
			for (int i = 0; i < m_portraits.Length; i++) {
				//add portrait script list to list of portraits
				m_potraitScripts.Add (m_portraits [i].GetComponent<PortaitScript> ());

				//hand a portrait a network lobby
				m_potraitScripts [i].HandPlayerNetworkLobby (m_opponents [i].GetComponent<CustomLobby> ());
			}
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

		//if opponents exist
		if (m_opponents.Length > 0) {
			for (int i = 0; i < m_portraits.Length; i++) {
				//hand player the network lobby info
				m_potraitScripts [i].HandPlayerNetworkLobby (m_opponents [i].GetComponent<CustomLobby> ());
			}
		}

		foreach(GameObject opponent in opponents)
		{
			CustomLobby.local.SendDetailsRequestForNetId(opponent.GetComponent<CustomLobby>().playerDetails.Identifier);
		}

		//order the portraits every frame (bit inefficient)
		OrderPortraits ();
	}
}
