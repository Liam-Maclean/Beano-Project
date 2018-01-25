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


enum PlantComponentType
{
	//Enums must be indexed for RNG
	NORMALPLANT = 0,
	DOUBLESCOREPLANT = 1,
	DEBUFFPLANT = 2,
	ELECTRICPLANT = 3,

}


public class PlantScriptManager : MonoBehaviour 
{
	private Animator m_animator;

	private bool FirstTimeSpawn = true;

	//enum for plants
	PlantComponentType plantComponentType;

    //Plant type component
	BasePlant basePlant;

	public GameObject[] particleGameobjects;
	List<ParticleSystem> emmiters = new List<ParticleSystem>();

	//all the sprites the respawner requires
	public Sprite[] sprites;

    //timer for respawning
	float timer = 0.0f;


    //start function (initialises plant type)
	void Awake()
	{
		m_animator = GetComponent<Animator> ();

		AddNewPlantComponent ();
		for (int i = 0; i < particleGameobjects.Length; i++) {
			emmiters.Add (particleGameobjects [i].GetComponent<ParticleSystem> ());
		}
	}


	//add randomised plant component
	public void AddNewPlantComponent()
	{
		if (!FirstTimeSpawn) {
			m_animator.SetTrigger ("Spawn");
		}
		plantComponentType = (PlantComponentType) Random.Range (0, 4);

		//plantComponentType = 0;

		switch (plantComponentType) {
		case PlantComponentType.NORMALPLANT:
			basePlant = gameObject.AddComponent<NormalPlant> ();
			basePlant.SetSprite (sprites [0]);
			break;
		case PlantComponentType.DOUBLESCOREPLANT:
			basePlant = gameObject.AddComponent<DoubleScorePlant> ();
			basePlant.SetSprite (sprites [1]);
			break;
		case PlantComponentType.DEBUFFPLANT:
			basePlant = gameObject.AddComponent<DebuffPlant> ();
			basePlant.SetSprite (sprites [2]);
			break;
		case PlantComponentType.ELECTRICPLANT:
			basePlant = gameObject.AddComponent<ElectricPlant> ();
			basePlant.SetSprite (sprites [3]);
			break;
		}    

		FirstTimeSpawn = false;
	}


    //if tile is swiped over
    public int Swiped()
    {
		int tempScore = 0;
		if (basePlant.GetActive ()) {
			tempScore = basePlant.GetScore ();
			Debug.Log (basePlant.GetScore ());
			FloatingTextManager.CreateFloatingText (basePlant.GetScore ().ToString (), basePlant.transform);
			basePlant.SetActive (false);

			for (int i = 0; i < emmiters.Count; i++) {
				emmiters [i].Play ();
			}
		}
        return tempScore;
    }


    //update
	void Update()
	{
		//if the base plant isn't active
		if (!basePlant.GetActive ()) {  
			basePlant.SetSprite(sprites[4]);
            StartTimer ();
		}
    }

    //starts the timer to remove plant component
	void StartTimer()
	{
		//start timer 
		timer += Time.deltaTime;

		//if time has reached the limit 
		if (timer > 2.0f) {
			
			//remove component, add a new one
			basePlant.RemoveComponent();
            AddNewPlantComponent();
			timer = 0f;
		}
	}

}