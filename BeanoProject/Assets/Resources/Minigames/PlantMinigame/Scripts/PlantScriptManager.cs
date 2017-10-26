using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//==============================================
//
// Plant Script Manager class
//
// This class controlls all the respawning, plant types and timer variables for respawning plants
//
// This class adds a random component (plant type) and when the plant is swiped, a new random
// plant is put in it's place after a timer and the old one removed (add and remove component)
//
// See base plant to see how the inheritence works for plant types
//
// Liam MacLean - 25/10/2017 03:39


public class PlantScriptManager : MonoBehaviour 
{
    //Plant type component
	NormalPlant ps;

    //all the sprites the respawner requires
	public Sprite[] sprites;

    //timer for respawning
	float timer = 0.0f;


    //start function (initialises plant type)
	void Start()
	{
		AddNewPlantComponent ();
	}


	//add randomised plant component
	public void AddNewPlantComponent()
	{
        //gameObject.AddComponent<NormalPlant>();
        ps = gameObject.AddComponent<NormalPlant> ();
		ps.SetSprite (sprites [0]);
	
	}

    //if tile is swiped over
    public int Swiped()
    {
        int tempScore = ps.score;
        ps.SetActive(false);
        return tempScore;
    }


    //update
	void Update()
	{
		if (!ps.GetActive ()) {  
            ps.SetSprite(sprites[3]);
            StartTimer ();
		}
    }

    //starts the timer to remove plant component
	void StartTimer()
	{
		timer += Time.deltaTime;

		if (timer > 2.0f) {
            ps.RemoveComponent();
            AddNewPlantComponent();
			timer = 0f;
		}
	}

}