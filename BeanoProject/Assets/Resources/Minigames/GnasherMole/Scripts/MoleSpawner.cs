using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mole Spawner class, one each to control the behaviours of the moles coming out the holes.
/// 
/// Works similarly to the plant minigame, 1 individual spawner for each mole, spawning different
/// kind of moles and controlling the overall behaviour of the moles, such as duration, spawning,
/// despawning, animations etc.
/// 
/// Liam MacLean 19:57 06/02/2018
/// </summary>

//mole type enums
enum MoleTypes
{
	Mole = 0,
	Human = 1
}
public class MoleSpawner : MonoBehaviour {

	//mole type
	private MoleTypes moleType;

	//mole sprite
	public Sprite moleTexture;

	//Highest and lowest range of time between mole spawning
	public float lowestRangeSpawnTimer;
	public float highestRangeSpawnTimer;

	//mole component
	private BaseMole m_mole;

	//respawn timer
	private float m_respawnTimer;

	//Initialises values on instantiation
	void Start()
	{
		ResetRespawnTimer ();
	}

	// Update is called once per frame
	void Update () {

		if (!m_mole) {
			this.GetComponent<SpriteRenderer> ().sprite = null;
			CountdownRespawnDuration ();
		}

		//if mole is ready to spawn
		if (RespawnDurationExpired ()) {
			this.GetComponent<SpriteRenderer> ().sprite = moleTexture;
			moleType = (MoleTypes)Random.Range (0, 1);
			switch (moleType) {
			case MoleTypes.Mole:
				m_mole = this.gameObject.AddComponent<Mole> ();
				break;
			case MoleTypes.Human:
				m_mole = this.gameObject.AddComponent<Human> ();
				break;
			}    

			ResetRespawnTimer();
		}


	}

	//Respawn timer expired
	public bool RespawnDurationExpired()
	{
		if (m_respawnTimer < 0.0f) {
			return true;
		} else {
			return false;
		}
	}

	//method to countdown the respawn time
	public void CountdownRespawnDuration()
	{
		m_respawnTimer -= Time.deltaTime;
	}


	//Method for reseting the spawn timer of moles
	public void ResetRespawnTimer()
	{
		m_respawnTimer = Random.Range (lowestRangeSpawnTimer, highestRangeSpawnTimer);
	}
}
