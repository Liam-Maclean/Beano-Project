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

public enum specialityType
{
	none,
	sparks,
	glow
}

public class BasePlant : MonoBehaviour
{
	//set if the plant is active or not 
	private bool bActive = true;
	protected int m_score = 0;
	protected SpriteRenderer sr;
	public specialityType m_specialityType;



	//getter
	public specialityType GetSpecialityType()
	{
		return m_specialityType;
	}

	//setter
	public void SetSpecialityType(specialityType specialityTsype)
	{
		m_specialityType = specialityTsype;
	}


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
		
	//destroys the component on the object
	public void RemoveComponent()
	{
		Destroy (this);
	}

	//on awake function
	void Awake()
	{
		sr = this.transform.GetComponent<SpriteRenderer> ();
	}

	//activates the plant with override functionality
	public virtual void ActivatePlant(out int score)
	{
		score = GetScore ();
	}

}