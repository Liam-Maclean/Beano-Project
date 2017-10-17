using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Base component for plants (every plant will do this behaviour)
public class BasePlant : MonoBehaviour
{
	//set if the plant is active or not 
	private bool bActive = true;
	private SpriteRenderer sr;

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

	public virtual void ActivatePlant()
	{
		SetActive (false);
	}

}