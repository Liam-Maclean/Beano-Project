using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieScript : MonoBehaviour 
{
	//touch variable
	private Vector2 m_touch;

	//maximum stretch and a radius variable
	public float maxStretch;
    private float maxStretchSqr;
    private float circleRadius;

	//distance variables
	private Vector3 pieStartPosition;
	private Vector3 pieEndPosition;
	private Vector3 distance;

	//rigidbody object
	private Rigidbody2D rb;

	//linerenderer objects
    public LineRenderer leftLine;
    public LineRenderer rightLine;


	private bool isTouched;

    private Ray rayToTouch;
    private Ray slingToPie;

	private GameObject slingshot;

	private bool newMouseDown;
	private bool oldMouseDown;

    void Awake()
    {
		slingshot = GameObject.FindGameObjectWithTag ("slingshot");
    }


	void Start()
	{
        InitLineRenderer();

        rayToTouch = new Ray(gameObject.transform.position, Vector3.zero);
        slingToPie = new Ray(leftLine.transform.position, Vector3.zero);

        rb = gameObject.GetComponent<Rigidbody2D>();

        maxStretchSqr = maxStretch * maxStretch;
        CircleCollider2D circle = GetComponent<Collider2D>() as CircleCollider2D;
        circleRadius = circle.radius;
	}


	void Update () {

		//Mouse Controls
		OnMouseDown ();
		//Update the line renders
		LineUpdate ();

		//Touch Controls
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch (0);

			//Switch statement determining which type of touch it is
			switch (touch.phase) 
			{
			case TouchPhase.Began:
				//store the initial position
				pieStartPosition = gameObject.transform.position;
				break;
			case TouchPhase.Moved:
				m_touch = touch.position;
				Dragging ();
				break;
			case TouchPhase.Ended:
				pieEndPosition = gameObject.transform.position;
				Launch ();
				break;
			}

			//gameObject.transform.position += distance;
		}
		gameObject.transform.position += distance;
	}
	void Dragging()
	{
		
		//get the world position of the touch
    	Vector2 objPos = Camera.main.ScreenToWorldPoint (m_touch);
		//get the distance from the slingshot to the touch
		Vector2 slingToTouch = new Vector2((objPos.x - slingshot.transform.position.x), (objPos.y - slingshot.transform.position.y));

        //this basically ensures that if the player stretches further than max stretch then it will still be aligned
        if (slingToTouch.sqrMagnitude > maxStretchSqr)
        {
            rayToTouch.direction = slingToTouch;
            objPos = rayToTouch.GetPoint(maxStretch);
        }

        //set the pies position to the position of the touch
		this.transform.position = objPos;
	}

	void MouseDragging()
	{
		//get the world position of the touch
		Vector2 objPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		//get the distance from the slingshot to the touch
		Vector2 slingToTouch = new Vector2((objPos.x - slingshot.transform.position.x), (objPos.y - slingshot.transform.position.y));

		//this basically ensures that if the player stretches further than max stretch then it will still be aligned
		if (slingToTouch.sqrMagnitude > maxStretchSqr)
		{
			rayToTouch.direction = slingToTouch;
			objPos = rayToTouch.GetPoint(maxStretch);
		}

		//set the pies position to the position of the touch
		this.transform.position = objPos;
	}

    void Launch()
	{
		//calculate the distance the pie has travelled
		distance = (pieStartPosition - pieEndPosition);
		//normalize the distance
		distance.Normalize();
    }

    void InitLineRenderer()
	{
		//create array to store line positions
		var leftPoints = new Vector3[3];

		//hardcoded values of the line position points
		leftPoints [0] = new Vector3(-2.0f, -2.5f, 0.0f);
		leftPoints [1] = new Vector3 (-1.0f, -3.6f, 0.0f);
		leftPoints [2] = this.transform.position;

		//set those position points
		leftLine.SetPositions (leftPoints);
		 
		//create array to store line positions
		var rightPoints = new Vector3[3];

		//hardcoded values of the line position points
		rightPoints [0] = new Vector3 (2.0f, -2.5f, 0.0f);
		rightPoints [1] = new Vector3 (1.0f, 3.6f, 0.0f);
		rightPoints [2] = this.transform.position;

		//set those position points
		rightLine.SetPositions (rightPoints);

		//set the order layer so that it displays the lines on top
        leftLine.sortingOrder = 2;
        rightLine.sortingOrder = 2;
    }

    void LineUpdate()
    {
		
        Vector2 slingshotToPie = slingshot.transform.position - leftLine.transform.position;
        slingToPie.direction = slingshotToPie;

        Vector3 holdPoint = slingToPie.GetPoint(slingshotToPie.magnitude + circleRadius);
        leftLine.SetPosition(1, holdPoint);
        rightLine.SetPosition(1, holdPoint);
    }


	void OnMouseDown()
	{
		newMouseDown = Input.GetMouseButton (0);

		if (newMouseDown && !oldMouseDown)
		{
			//store the initial position
			pieStartPosition = this.transform.position;

		}
		if (newMouseDown && oldMouseDown)
		{
			MouseDragging ();
			LineUpdate ();
		}
		if (!newMouseDown && oldMouseDown)
		{
			pieEndPosition = this.transform.position;
			Launch ();
		}


		oldMouseDown = newMouseDown;
	}



}
