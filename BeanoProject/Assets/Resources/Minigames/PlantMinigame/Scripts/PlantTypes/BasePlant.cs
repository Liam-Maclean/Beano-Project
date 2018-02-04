using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==============================================
//
// Base Plant Class
//
// This class is the base class for the plant heirarchy 
//
// all plant types must conform to this base class
// 
// Contains a self-destructor method and sprite setting methods
//
// Liam MacLean - 25/10/2017 03:42
public class BasePlant : MonoBehaviour
{
	//set if the plant is active or not 
	private bool bActive = true;
	protected int m_score = 0;
	protected SpriteRenderer sr;

	//getter
	public int GetScore()
	{
		return m_score;
	}

	//setter
	public void SetScore(int score)
	{
		m_score = score;
	}
		
	//getter
	public bool GetActive()
	{
		return bActive;
	}

	//setter
	public void SetActive(bool active)
	{
		bActive = active;
	}

	//getter
	public Sprite GetSprite()
	{
		return sr.sprite;
	}

	//setter
	public void SetSprite(Sprite sprite)
	{
		sr.sprite = sprite;
	}
		
	public void RemoveComponent()
	{
		Destroy (this);
	}

	void Awake()
	{
		sr = this.transform.GetComponent<SpriteRenderer> ();
	}

	public virtual void ActivatePlant(out int score)
	{
		score = GetScore ();
	}

}