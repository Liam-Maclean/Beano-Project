using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Canvas portrait setup
/// 
/// This script Orders the portraits into the correct order on the gamescreen.
/// It also sets up the portraits at the start of the game with the correct customlobby for each player
/// Uses a simple sort to change around values (can be changed to a different sort if computationally complex)
/// 
/// Liam MacLean 17/02/2018 15:31
/// </summary>
public class CanvasPortraitSetup : MonoBehaviour
{

    //score list used to calculate who is first
    private List<int> m_scores = new List<int>();

    //gameobject portraits with tag "Portrait"
	private List<GameObject> m_portraits = new List<GameObject>();

    //portrait script containing all details of the portrait from the gameobjects found
    private List<PortaitScript> m_potraitScripts = new List<PortaitScript>();

    //positions on the screen for 1st, 2nd, 3rd and 4th
    public Vector3[] m_portraitPositions;

	private GameObject LocalPortrait;
    private GameObject[] m_opponents;
    private List<GameObject> opponents = new List<GameObject>();

    // Use this for initialization
    void Awake()
    {	
		
        //get gameobjects with the tag portraits in the scene
        //m_portraits = GameObject.FindGameObjectsWithTag("Portrait");
        m_opponents = GameObject.FindGameObjectsWithTag("Player");


        //foreach (GameObject player in m_opponents)
        //{
        //    //if (player.GetComponent<CustomLobby>().playerDetails.Identifier != CustomLobby.local.playerDetails.Identifier)
        //    //{
        //    opponents.Add(player);
        //    //}
        //}

        ////gets all the portrait scripts from the portrait objects
        ////if there are more than 0 opponents
		for (int i = 0; i < m_opponents.Length; i++) {
			m_portraits.Add(Instantiate(Resources.Load("Minigames/UniversalMinigamePrefabs/PlayerPortait"), m_portraitPositions[i], Quaternion.identity)as GameObject);
			m_portraits [i].transform.SetParent (GameObject.Find ("MinigameCanvas").transform);
			m_portraits [i].transform.localScale = new Vector3(1.2f, 1.4f, 1.0f);
		}
		if (m_opponents.Length == 0) {
			LocalPortrait = Instantiate(Resources.Load("Minigames/UniversalMinigamePrefabs/PlayerPortait"), m_portraitPositions[0], Quaternion.identity)as GameObject;
			LocalPortrait.transform.SetParent (GameObject.Find ("MinigameCanvas").transform);
			LocalPortrait.transform.localScale = new Vector3(1.2f, 1.4f, 1.0f);
		}

        //itterate through portrait objects
		for (int i = 0; i < m_portraits.Count; i++)
        {
            //add portrait script list to list of portraits
            m_potraitScripts.Add(m_portraits[i].GetComponent<PortaitScript>());

       		//hand a portrait a network lobby
            m_potraitScripts[i].HandPlayerNetworkLobby(m_opponents[i].GetComponent<CustomLobby>());
        }
		RepositionPotraits ();
    }

    //function to reposition portraits ascending order
    void OrderPortraits()
    {
        //get the scores from the portraits and store them into an array
        for (int i = 0; i < m_potraitScripts.Count; i++)
        {
            m_scores.Add(m_potraitScripts[i].GetScore());
        }

        //temp potrait script for swapping values
        PortaitScript temp;

        //simple sort algoithm on portraits
        //for every score value in the array
        for (int i = 0; i < m_scores.Count; i++)
        {
            //for every value above that
            for (int x = i + 1; x < m_scores.Count; x++)
            {
                //if the score is greater than the next value
                if (m_scores[i] < m_scores[x])
                {
                    //order them properly
                    temp = m_potraitScripts[i];
                    m_potraitScripts[i] = m_potraitScripts[x];
                    m_potraitScripts[x] = temp;
                }
            }
        }

        //reposition potraits and clear scores for recalculation
        m_scores.Clear();
        RepositionPotraits();
    }


    //reposition the potraits in order of positions passed in
    void RepositionPotraits()
    {
        //for every potrait
		if (m_opponents.Length > 0) {
			for (int i = 0; i < m_potraitScripts.Count; i++) {
				//reposition the corresponding gameobject
				m_potraitScripts [i].gameObject.transform.localPosition = m_portraitPositions [i];
			}
		}
		else
		{
			LocalPortrait.transform.localPosition = m_portraitPositions [0];
		}
    }

    // Update is called once per frame
    void Update()
    {
		for (int i = 0; i < m_portraits.Count; i++)
        {
            //hand player the network lobby info
            m_potraitScripts[i].HandPlayerNetworkLobby(m_opponents[i].GetComponent<CustomLobby>());
        }
       
		for (int i = 0; i < m_opponents.Length; i++) {
			//if (m_opponents [i].GetComponent<CustomLobby> ().isLocalPlayer == false) {
				CustomLobby.local.SendDetailsRequestForNetId (m_opponents [i].GetComponent<CustomLobby> ().playerDetails.Identifier);
			//}
        }

        //order the portraits every frame (bit inefficient)
        //OrderPortraits();
    }
}

