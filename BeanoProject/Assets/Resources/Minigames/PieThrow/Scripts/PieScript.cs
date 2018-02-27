using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieScript : MonoBehaviour 
{
	//vector2 variables
	private Vector2 m_touch;

	//float variables
	public float maxStretch;
    private float maxStretchSqr;
	private	float timer = 2.0f;

	//distance variables
	private Vector3 pieStartPosition;
	private Vector3 pieEndPosition;
	private Vector3 distance;

	//rigidbody object
	private Rigidbody2D rb;

    //bool variables
	private bool isReload;
	private bool isDestroyed;
	private static bool hasLaunched;
	private bool oldMouseDown;
    private bool newMouseDown;



    //Ray variables
    private Ray rayToTouch;
    private Ray slingToPie;

    //GameObject variables
	private GameObject slingshot;
	public GameObject piePrefab;

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
    }
		
	void Start()
	{
        //initialise the rays
        rayToTouch = new Ray(gameObject.transform.position, Vector3.zero);
		slingToPie = new Ray(slingshot.transform.position, Vector3.zero);

        //intialise the necessary components
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();


        //get the square of maxstretch
        maxStretchSqr = maxStretch * maxStretch;

		hasLaunched = false;
		isReload = false;
		isDestroyed = false;
	}


	void Update()
    {
        //Mouse Controls
        if (!isReload && !hasLaunched)
        {
            OnMouseDown();
        }
        //Touch Controls
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            //Switch statement determining which type of touch it is
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    //store the initial position
                    pieStartPosition = gameObject.transform.position;
                    break;
                case TouchPhase.Moved:
                    m_touch = touch.position;
                    if (!isReload && !hasLaunched)
                    {
                        Dragging();
                    }
                    break;
                case TouchPhase.Ended:
                    pieEndPosition = gameObject.transform.position;
                    Launch();
                    Respawn();
                    break;
            }
        }

        //only respawn if the pie has been launched 
        if (hasLaunched)
        {
            //translates the pies position
            gameObject.transform.position += distance;
            Respawn();
        }
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

        //rotate the pie depending on the angle of aim
        PieRotate(objPos.x);
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

        //update the sprite of the pie depending on the angle of aim
        PieRotate(objPos.x);
       // HoldPoint();
    }

    void Launch()
	{

        //calculate the distance the pie has travelled
        distance = (pieStartPosition - pieEndPosition);

		//normalize the distance
		distance.Normalize();
        hasLaunched = true;
        isReload = true;
	}

    void HoldPoint()
	{
        //get the distance between the slingshot to the pie
		Vector2 slingshotToPie = slingshot.transform.position - gameObject.transform.position;
        slingToPie.direction = slingshotToPie;

        Vector3 holdPoint = slingToPie.GetPoint(slingshotToPie.magnitude);
    }


	void PieRotate(float pos)
    {
        //swap the sprite depending on whether the x value is negative or positive
        if (pos < 0)
        {
            sr.sprite = angledLeftPie;
        }
        else if (pos >= 0 || pos < 1)
        {
            sr.sprite = originPie;
        }
        if (pos > 1)
        {
            sr.sprite = angledRightPie;
        }
    }


    //this function does the exact same thing as the touch controls only with mouse controls instead
	void OnMouseDown()
	{
        //get the current mouse click 
		newMouseDown = Input.GetMouseButton (0);


        //if there has been a click
		if (newMouseDown && !oldMouseDown)
		{
			//store the initial position
			pieStartPosition = this.transform.position;

        }

        //if the click is being held down
        if (newMouseDown && oldMouseDown)
		{
		
				MouseDragging ();
			
		}
 
        //if the there is no longer a click being held down
		if (!newMouseDown && oldMouseDown)
		{
			pieEndPosition = this.transform.position;
			Launch ();
        }

        //set the current mouse input to the old one for comparison
		oldMouseDown = newMouseDown;
	}

    void Respawn()
    {
		GameObject newPie;
	    
        //takes away 1 second per frame
		timer -= Time.deltaTime;
        //if the timer hits 0
		if (timer <= 0.0f)
        {
            //instansiate the new pie at the respawn position
			newPie = (GameObject)Instantiate (piePrefab, new Vector3 (0.0f, -2.5f, 0.0f), Quaternion.identity);
            //destroy the old pie if it hasnt collided
			Destroy (this.gameObject);
            //reset pie variables 
			timer = 2.0f;
			isReload = false;
			hasLaunched = false;
            isDestroyed = false;
            //set the sprite back to the original sprite
			sr.sprite = originPie;
		}

    }

	//GETTERS
	public bool GetLaunched()
	{
		return hasLaunched;
	}

	public bool GetDestroyed ()
	{
		return isDestroyed;
	}

	//SETTERS
	public void SetDestroyed(bool destroyed)
	{
		isDestroyed = destroyed;
	}
	public void SetLaunched(bool launched)
	{
		hasLaunched = launched;
	}

}
