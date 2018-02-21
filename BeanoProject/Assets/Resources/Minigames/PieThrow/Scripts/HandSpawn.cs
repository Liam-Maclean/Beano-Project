using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSpawn : MonoBehaviour
{ 
    private SpriteRenderer sr;
    private bool oldMouseDown;
    private bool newMouseDown;
    public Sprite handClosed;
    public Sprite handOpen;
    public GameObject handPrefab;
    private GameObject clone;


    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        newMouseDown = Input.GetMouseButtonDown(0);

        if (newMouseDown && !oldMouseDown)
        {
            clone = (GameObject)Instantiate(handPrefab, mousePos, Quaternion.identity);
           
        }

        clone.transform.position = mousePos;
        
        if (!newMouseDown && oldMouseDown)
        {
            Destroy(clone);
        }

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
