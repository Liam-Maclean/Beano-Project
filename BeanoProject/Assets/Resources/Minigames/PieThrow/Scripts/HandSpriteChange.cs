using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <This script deals with the changing of sprites>
/// Hand sprite change.
/// 
/// Depending whether there has been a touch or mouse click the sprite will change to either a 
/// open hand or a closed hand.
/// 
/// <This wil eb the final version of this script>


public class HandSpriteChange : MonoBehaviour {

	//sprite variables
	private SpriteRenderer spriteRenderer;
	public Sprite handOpen;
	public Sprite handClosed;

	// Use this for initialization
	void Start () 
	{
		//get the object that this script is attached too's sprite renderer 
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();	
	}
	
	// Update is called once per frame
	void Update ()
	{
		MouseClick ();
		TouchInput ();

	}

	void MouseClick()
	{
		//if the mouse button is being held down
		if (Input.GetMouseButton (0)) 
		{
			//change the sprite to be a grabbing hand
			spriteRenderer.sprite = handClosed;
		} 
		else
		{
			//change the sprite to be a open hand
			spriteRenderer.sprite = handOpen;
		}
	}

	void TouchInput()
	{

		//Touch Controls
		if (Input.touchCount == 1) 
		{
			Touch touch = Input.GetTouch (0);

			//Switch statement determining which type of touch it is
			switch (touch.phase) 
			{
			case TouchPhase.Began:
				spriteRenderer.sprite = handClosed;
				break;
			case TouchPhase.Ended:
				spriteRenderer.sprite = handClosed;
				break;

			}
		}
	}

}
