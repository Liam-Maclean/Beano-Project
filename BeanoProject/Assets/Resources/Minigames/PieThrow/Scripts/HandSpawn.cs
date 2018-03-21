using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <This script deals with spawning the hand>
/// Hand spawn.
/// this script will be attatched to an empty game object that will spawn the hand prefab.
/// It spawns a hand prefab at the current mouse position and it moves depending on the current
/// mouse or touch position.
/// <This will be the final version>


public class HandSpawn : MonoBehaviour
{ 
	//gameObject variables
    public GameObject handPrefab;
    private GameObject clone;

	//vector2 variables
	private Vector2 m_touch;


    // Use this for initialization
    void Start()
    {
		//get the current mouse position
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//spawn the hand at the current mouse position
		clone = (GameObject)Instantiate(handPrefab, mousePos, Quaternion.identity);
		//set the cursor to invisible
		Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
		//this handles all the mouse controls
		MouseControls ();
		//this handles all the touch controls
		TouchControls ();
    }


	void MouseControls()
	{
		//get the current mouse position which is updated every frame
		Vector2 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		//if the hand exists
		if (clone)
		{
			//set the hands position to the current mouse position
			clone.transform.position = currMousePos;
		}
	}

	void TouchControls()
	{
		//get the world position of the touch
		Vector2 currTouchPos = Camera.main.ScreenToWorldPoint (m_touch);

		//Touch Controls
		if (Input.touchCount == 1) 
		{
			Touch touch = Input.GetTouch (0);

			//Switch statement determining which type of touch it is
			switch (touch.phase) 
			{
			case TouchPhase.Began:
				//store the initial position
				if (clone)
				{
					//set the hands position to the current touch position 
					clone.transform.position = currTouchPos;
				};
				break;
			case TouchPhase.Moved:
				//set the current touch position
				m_touch = touch.position;
				break;
			}
		}
	}

	public void Destroy()
	{
		Destroy (clone);
	}
}
