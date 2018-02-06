using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieScript : MonoBehaviour 
{
	//vector2 variables
	private Vector2 m_touch;

    //audio variables
    public AudioClip pullSound;
    public AudioClip launchSound;
    private AudioSource source;
    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;


	//float variables
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

    //bool variables
	private bool isTouched;
	private bool oldMouseDown;
    private bool newMouseDown;

    //Ray variables
    private Ray rayToTouch;
    private Ray slingToPie;

    //GameObject variables
	private GameObject slingshot;

    //Collider variables
    private CircleCollider2D circle;

    //Sprite variables
    private SpriteRenderer sr;
    public Sprite angledRightPie;
    public Sprite angledLeftPie;
    public Sprite originPie;


    void Awake()
    {
        //On launch find the slingshot gameObject
		slingshot = GameObject.FindGameObjectWithTag ("slingshot");
        source = GetComponent<AudioSource>();
    }


	void Start()
	{
        //initialise the line renderers
        InitLineRenderer();

         
        //initialise the rays
        rayToTouch = new Ray(gameObject.transform.position, Vector3.zero);
        slingToPie = new Ray(leftLine.transform.position, Vector3.zero);

        //intialise the necessary components
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<Collider2D>() as CircleCollider2D;

        //get the square of maxstretch
        maxStretchSqr = maxStretch * maxStretch;
        circleRadius = circle.radius;

	}


	void Update ()
    {

		//Mouse Controls
		OnMouseDown ();

        //Update the line renders
        //LineUpdate ();

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
                    source.PlayOneShot(pullSound, 1.0f);
                    break;
			case TouchPhase.Ended:
				pieEndPosition = gameObject.transform.position;
				Launch ();
				break;
			}
		}
		gameObject.transform.position += distance;
	}


	void Dragging()
	{

        source.PlayOneShot(pullSound, 1.0f);
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

        //rotate the pie depending on the angle of aim
        PieRotate(objPos);
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

        //rotate the pie depending on the angle of aim
        PieRotate(objPos);
    }

    void Launch()
	{
        source.PlayOneShot(launchSound);

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


    void PieRotate(Vector2 pos)
    {
        //swap the sprite depending on whether the x value is negative or positive
        if (pos.x < 0)
        {
            sr.sprite = angledLeftPie;
        }
        else if (pos.x >= 0 || pos.x < 1)
        {
            sr.sprite = originPie;
        }
        if (pos.x > 1)
        {
            sr.sprite = angledRightPie;
        }
    }


    //this function does the exact same thing as the touch controls only with the mouse
	void OnMouseDown()
	{
        //get the current mouse click 
		newMouseDown = Input.GetMouseButton (0);


        //if there has been a click
		if (newMouseDown && !oldMouseDown)
		{
			//store the initial position
			pieStartPosition = this.transform.position;
            source.PlayOneShot(pullSound, 0.5f);

        }

        //if the click is being held down
        if (newMouseDown && oldMouseDown)
		{
			MouseDragging ();
            LineUpdate ();
		}

       
        //if the there is no longer a click being held down
		if (!newMouseDown && oldMouseDown)
		{
			pieEndPosition = this.transform.position;
			Launch ();
            Respawn();
            source.PlayOneShot(launchSound, 0.5f);
        }

        //set the current mouse input to the old one for comparison
		oldMouseDown = newMouseDown;
	}

    void Respawn()
    {
        float timer = 5.0f;

        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            GameObject respawn = Instantiate(this.gameObject, new Vector3(0.0f, 0.0f, 0.0f), gameObject.transform.rotation);
            
        }
        
    }
}
