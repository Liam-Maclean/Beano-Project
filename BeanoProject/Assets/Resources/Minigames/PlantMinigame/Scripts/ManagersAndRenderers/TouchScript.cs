
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==============================================
//
// Touch Manager Script
//
// This class controls all the necessary functionality involving touch controls on Android and iOS
//
// This class will most likely be CHANGED after prototyping
// 
// Contains vector maths using Physics2D library and touch controls
// 
// Also deals with screen orientation a bit
//
// Liam MacLean - 25/10/2017 03:42
public class TouchScript : MonoBehaviour
{

	BasePlant plantHit;
	//combined 
    private int m_combinedScore;
    private List<int> m_plantScore = new List<int>();

	//Game manager script
    private ManagerScript manager;

	//Direction vectors for mathematikz
    private Vector2 m_touchBegin;
    private Vector2 m_touchEnd;
    private Vector2 m_swipeDirection;
    private Vector2 m_distance;

    // Use this for initialization
    void Start()
    {
		//manager script initialisation
        manager = this.GetComponent<ManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //SwipeLine();
    }

	//checking if a vector is negative or positive
	public Vector2 NegativePositiveFunction(Vector2 value)
	{
		float tempx, tempy;
		tempx = value.x;
		tempy = value.y;
		if (tempx < 0.0f) {
			tempx *= -1.0f;
		}
		if (tempy < 0.0f) {
			tempy *= -1.0f;
		}
			
		if (tempx > tempy) {
			return new Vector2 (value.x, 0.0f);
		}
		else if  (tempy > tempx) {
			return new Vector2 (0.0f, value.y);
		}
			
		return new Vector2(0.0f, 0.0f);

	}

}
