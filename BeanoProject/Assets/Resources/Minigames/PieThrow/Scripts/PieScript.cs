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

    //bool variables
	private bool isReloading;
	private bool isDestroyed;
	private static bool hasLaunched;
	private bool oldMouseDown;
    private bool newMouseDown;

	private SpriteRenderer sr;

    //Ray variables
    private Ray rayToTouch;

    //GameObject variables
	private GameObject slingshot;
	public GameObject piePrefab;
    private GameObject pie;
	public GameObject gameManager;


	private PieThrowManagerScript managerScript;

    //Collider variables
    private CircleCollider2D circle;

    void Awake()
    {
        //On launch find the slingshot gameObject
		slingshot = GameObject.FindGameObjectWithTag ("slingshot");

        Orientor.pieThrow = true;

    }
		
	void Start()
	{
        pie = (GameObject)Instantiate(piePrefab, new Vector3(0.0f, -2.5f,0.0f), Quaternion.identity);
        //initialise the rays
        rayToTouch = new Ray(pie.transform.position, Vector3.zero);

		sr = pie.GetComponent<SpriteRenderer>();
		managerScript = gameManager.GetComponent<PieThrowManagerScript> ();

        //get the square of maxstretch
        maxStretchSqr = maxStretch * maxStretch;

		hasLaunched = false;
		isReloading = false;
		isDestroyed = false;
	}


	void Update()
    {
		if (managerScript.GetState () == 1) {
			//Mouse Controls
			if (!isReloading && !hasLaunched) {
				OnMouseDown ();
			}
			//Touch Controls
			if (Input.touchCount == 1) {
				Touch touch = Input.GetTouch (0);

				//Switch statement determining which type of touch it is
				switch (touch.phase) {
				case TouchPhase.Began:
                    //store the initial position
					pieStartPosition = pie.transform.position;
					break;
				case TouchPhase.Moved:
					m_touch = touch.position;
					if (!isReloading && !hasLaunched) {
						Dragging ();
					}
					break;
				case TouchPhase.Ended:
					pieEndPosition = pie.transform.position;
					Launch ();
					break;
				}
			}
			//only respawn if the pie has been launched 
			if (hasLaunched || isDestroyed) {
				//translates the pies position
				pie.transform.position += distance;

				Respawn ();
			}
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
        pie.transform.position = objPos;

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
        pie.transform.position = objPos;
    }

    void Launch()
	{
        //calculate the distance the pie has travelled
        distance = (pieStartPosition - pieEndPosition);

		//normalize the distance
		distance.Normalize();
        hasLaunched = true;
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
			pieStartPosition = pie.transform.position;

        }

        //if the click is being held down
        if (newMouseDown && oldMouseDown)
		{
	        MouseDragging ();
		}
 
        //if the there is no longer a click being held down
		if (!newMouseDown && oldMouseDown)
		{
			pieEndPosition = pie.transform.position;
			Launch ();
        }

        //set the current mouse input to the old one for comparison
		oldMouseDown = newMouseDown;
	}

	public void Respawn()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
		{
            isReloading = true;
            //instansiate the new pie at the respawn position
            pie = (GameObject)Instantiate(piePrefab, new Vector3(0.0f, -2.5f, 0.0f), Quaternion.identity);
			sr = pie.GetComponent<SpriteRenderer>();
            //reset pie variables 
            timer = 2.0f;
            isReloading = false;
            hasLaunched = false;
            isDestroyed = false;
        }
    }

	public void Destroy()
	{
		isDestroyed = true;
		Destroy (sr);
		Destroy (pie.gameObject,2.0f);
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
