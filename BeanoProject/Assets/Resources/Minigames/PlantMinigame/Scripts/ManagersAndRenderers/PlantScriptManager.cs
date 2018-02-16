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
	SpriteRenderer sr;


    //Plant type component
	BasePlant basePlant;

	public GameObject[] particleGameobjects;
	List<ParticleSystem> emmiters = new List<ParticleSystem>();

	//all the sprites the respawner requires
	public Sprite[] sprites;

    //timer for respawning
	float timer = 0.0f;

	private bool endGame = false;


    //start function (initialises plant type)
	void Awake()
	{
		sr = this.GetComponent<SpriteRenderer> ();
		m_animator = GetComponent<Animator> ();
		
		AddNewPlantComponent ();
		for (int i = 0; i < particleGameobjects.Length; i++) {
			emmiters.Add (particleGameobjects [i].GetComponent<ParticleSystem> ());
		}
	}

	//returns the plant component currently active on the spawner
	public BasePlant GetPlantComponent()
	{
		return basePlant;
	}
		

	//add randomised plant component
	public void AddNewPlantComponent()
	{
		m_animator.SetTrigger ("Spawn");
		plantComponentType = (PlantComponentType) Random.Range (0, 3);

		//plantComponentType = 0;

		switch (plantComponentType) {
		case PlantComponentType.NORMALPLANT:
			m_animator.enabled = false;
			basePlant = gameObject.AddComponent<NormalPlant> ();
			basePlant.SetSprite (sprites [0]);
			sr.sprite = sprites [0];
			m_animator.enabled = true;
			m_animator.SetTrigger ("VenusPlant");
			break;
		case PlantComponentType.DOUBLESCOREPLANT:
			m_animator.enabled = false;
			basePlant = gameObject.AddComponent<DoubleScorePlant> ();
			basePlant.SetSprite (sprites [1]);
			sr.sprite = sprites [1];
			m_animator.enabled = true;
			m_animator.SetTrigger ("YellowPlant");
			break;
		case PlantComponentType.DEBUFFPLANT:
			m_animator.enabled = false;
			basePlant = gameObject.AddComponent<DebuffPlant> ();
			basePlant.SetSprite (sprites [2]);
			sr.sprite = sprites [2];
			m_animator.enabled = true;
			m_animator.SetTrigger ("BulbPlant");
			break;
		case PlantComponentType.ELECTRICPLANT:
			basePlant = gameObject.AddComponent<ElectricPlant> ();
			basePlant.SetSprite (sprites [3]);
			sr.sprite = sprites [3];
		
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

			//if the plant is type of double plant
			if (basePlant is DoubleScorePlant) {
				//white text
				FloatingTextManager.CreateFloatingText (basePlant.GetScore ().ToString () + " (x2)", basePlant.transform, Color.white);
			} else {
				//yellow text
				FloatingTextManager.CreateFloatingText (basePlant.GetScore ().ToString (), basePlant.transform);
			}

			m_animator.SetTrigger ("DeadPlant");

			basePlant.SetActive (false);

			for (int i = 0; i < emmiters.Count; i++) {
				emmiters [i].Play ();
			}
		}
        return tempScore;
    }

	//kill the plant
	public void KillPlant()
	{
		endGame = true;
		basePlant.SetActive (false);
		m_animator.SetTrigger ("DeadPlant");
		for (int i = 0; i < emmiters.Count; i++) {
			emmiters [i].Play ();
		}
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
		if (!endGame) {
			//start timer 
			timer += Time.deltaTime;

			//if time has reached the limit 
			if (timer > 2.0f) {
			
				//remove component, add a new one
				basePlant.RemoveComponent ();
				AddNewPlantComponent ();
				timer = 0f;
			}
		}
	}

}