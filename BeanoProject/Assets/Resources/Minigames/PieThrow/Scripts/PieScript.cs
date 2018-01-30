using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieScript : MonoBehaviour 
{

	EnemyManagerScript enemyManager;

	private Vector2 m_touch;
	private Vector2 m_endTouch;

	public float maxStretch;
    private float maxStretchSqr;
    private float circleRadius;
    private Transform slingshot;


	private SpringJoint2D spring;
	private Rigidbody2D rb;

    public LineRenderer leftLine;
    public LineRenderer rightLine;

	private bool touched;


    private Ray rayToTouch;
    private Ray slingToPie;


    void Awake()
    {
        //slingshot = spring.connectedBody.transform;
        spring = gameObject.GetComponent<SpringJoint2D>();
    }


	void Start()
	{
        InitLineRenderer();

        rayToTouch = new Ray(gameObject.transform.position, Vector3.zero);
        slingToPie = new Ray(leftLine.transform.position, Vector3.zero);

        rb = gameObject.GetComponent<Rigidbody2D>();

        maxStretchSqr = maxStretch * maxStretch;
        // ask liam if this is okay to do
        CircleCollider2D circle = GetComponent<Collider2D>() as CircleCollider2D;
        circleRadius = circle.radius;
	}


	void Update () 
	{
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (spring != null)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        spring.enabled = true;
                        Dragging();
                        break;
                    case TouchPhase.Ended:
                        spring.enabled = false;
                        rb.isKinematic = false;
                        Launch();
                        break;
                }

            }

        }
        else
        {

        }

			
	}
	void Dragging()
	{
    	Vector3 objPos = Camera.main.ScreenToWorldPoint (m_touch);
        Vector2 slingToTouch = objPos - slingshot.transform.position;

        //this basically ensures that if the player stretches further than max stretch then it will still be aligned
        if (slingToTouch.sqrMagnitude > maxStretchSqr)
        {
            rayToTouch.direction = slingToTouch;
            objPos = rayToTouch.GetPoint(maxStretch);
        }


        objPos.z = 0.0f;
		gameObject.transform.position = objPos;
	}

    void Launch()
    {
        if (!rb.isKinematic)
        {
            Destroy(spring);
        }
    }

    void InitLineRenderer()
    {
        leftLine.SetPosition(0, leftLine.transform.position);
        rightLine.SetPosition(0, rightLine.transform.position);

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
}
