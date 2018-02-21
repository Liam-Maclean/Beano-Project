using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSpawn : MonoBehaviour
{ 
    private bool oldMouseDown;
    private bool newMouseDown;
    private SpriteRenderer sr;
    public Sprite handClosed;
    public Sprite handOpen;
    public GameObject handPrefab;
    private GameObject clone;


    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        for (int i = 0; i  < 1; i ++)
        {
            clone = (GameObject)Instantiate(handPrefab, new Vector2(0.0f, -3.0f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //get the current mouse click 
        newMouseDown = Input.GetMouseButton(0);

        //if there has been a click
        if (newMouseDown && !oldMouseDown)
        {
            clone.transform.position = mousePos;
        }

        //if the click is being held down
        if (newMouseDown && oldMouseDown)
        {
        }

        //if the there is no longer a click being held down
        if (!newMouseDown && oldMouseDown)
        {
            sr.sprite = handOpen;
            Destroy(clone);
        }
        //set the current mouse input to the old one for comparison
        oldMouseDown = newMouseDown;
    }


    void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log("hit");


        if (collisionInfo.collider.tag == "pie")
        {
            sr.sprite = handClosed;
        }

    }
}
