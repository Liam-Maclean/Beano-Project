using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieScript : MonoBehaviour 
{

	EnemyManagerScript enemyManager;

	private Vector2 m_touch;
	private Vector2 m_endTouch;

	public float maxStretch;

	private SpringJoint2D spring;
	private Rigidbody2D rb;


	private bool touched;


	void Start()
	{
		spring = GetComponent<SpringJoint2D>;
		rb = GetComponent<Rigidbody2D>;
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.touchCount == 1)
		{
			Touch touch = Input.GetTouch (0);
			switch (touch.phase)
			{
			case TouchPhase.Began:
				spring.enabled = true;
				Dragging ();
				break;
			case TouchPhase.Ended:
				spring.enabled = false;
				rb.isKinematic = false;
				Launch ();
				break;
			}
		}

			
	}
	void Dragging()
	{
		Vector2 objPos = Camera.main.ScreenToWorldPoint (m_touch);
		gameObject.transform.position = objPos;
	}
}
