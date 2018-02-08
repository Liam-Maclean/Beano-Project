using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchScript : MonoBehaviour {

    public GameObject finger;
    public GameObject wrench;
    Rigidbody2D wrenchbody;
    Vector2 oldFingerPos;

	// Use this for initialization
	void Start () {
        oldFingerPos = new Vector2(6.96f, 1.79f);
        wrenchbody = wrench.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 currentFingerPos = finger.transform.position;
        Ray2D movement = new Ray2D(oldFingerPos, (oldFingerPos - currentFingerPos));
        for (float i = 0 ; i <= Mathf.Sqrt(((oldFingerPos.x - currentFingerPos.x) * (oldFingerPos.x - currentFingerPos.x)) + ((oldFingerPos.y - currentFingerPos.y) - (oldFingerPos.y - currentFingerPos.y))) ; i += 0.01f)
        {
            if (wrenchbody.OverlapPoint(movement.GetPoint(i)))
            {
                wrenchbody.AddForceAtPosition(movement.GetPoint(i) - currentFingerPos, movement.GetPoint(i));
            }
        }

	}
}
