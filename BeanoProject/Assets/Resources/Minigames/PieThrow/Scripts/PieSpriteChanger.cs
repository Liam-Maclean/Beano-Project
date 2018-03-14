using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieSpriteChanger : MonoBehaviour {

    private PieScript pieScript;
    private GameObject pieSpawner;


    //Sprite variables
    private SpriteRenderer sr;
    public Sprite angledRightPie;
    public Sprite angledLeftPie;
    public Sprite originPie;

	public Animation pieSplat;

    // Use this for initialization
    void Start ()
    {
        //acces the pie spawner game object and get its attached script
        pieSpawner = GameObject.FindGameObjectWithTag("PieSpawner");
		pieScript = pieSpawner.GetComponent<PieScript> ();
    }
	
	// Update is called once per frame
	void Update ()
    {
		sr = gameObject.GetComponent<SpriteRenderer> ();
        //get the game objects current x position
        float xPos = gameObject.transform.position.x;

        //while the pie hasnt been thrown 
        if (pieScript.GetLaunched () == false)
        {
            //change the sprites according the the pie's x position
            PieRotate(xPos);
        }	
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

	public void PlaySplat()
	{
		pieSplat.Play ();
		Debug.Log(pieSplat.isPlaying);

	}
}
