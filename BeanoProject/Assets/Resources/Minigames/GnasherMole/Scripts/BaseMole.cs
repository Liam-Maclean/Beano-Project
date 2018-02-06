using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Mole class for interactable "moles" that pop up on screen
/// 
/// 
/// 
///	Only contains data for the derived classes
/// 
/// 
/// 
/// Liam MacLean 20:08 06/02/2018
/// </summary>

public class BaseMole : MonoBehaviour {
	
	//Variables
	private float m_duration;
	private int m_score;

	//set method
	public void SetDuration(float duration)
	{
		m_duration = duration;
	}

	//get method
	public float GetDuration()
	{
		return m_duration;
	}

	//set method
	public void SetScore(int score)
	{
		m_score = score;
	}

	//get method
	public int GetScore()
	{
		return m_score;
	}

	//checks if duration of mole has expired
	public bool DurationExpired()
	{
		if (m_duration <= 0) {
			return true;
		} else {
			return false;
		}
	}

	//Counts down duration time
	public void CountdownDuration()
	{
		m_duration -= Time.deltaTime;
	}

	//Update method
	void Update()
	{
		CountdownDuration ();
		if (DurationExpired ()) {
			Destroy (this);
		}
	}


}
