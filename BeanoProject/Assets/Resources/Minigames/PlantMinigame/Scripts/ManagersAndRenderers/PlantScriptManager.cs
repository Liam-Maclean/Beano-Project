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
	private float itterator = 1.0f;
	private float frame = 0.5f;
	private Animator m_animator;
	private AnimatorStateInfo m_stateInfo;
	private bool FirstTimeSpawn = true;

	bool spawnOnceLightning = false;
	GameObject lightning;
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

    bool bStartSpawn = false;
    public float lowestSpawnTime;
    public float highestSpawnTime;
    private float startSpawnTime;

    //start function (initialises plant type)
	void Awake()
	{
        //initialise values from the gameobjects components
		sr = this.GetComponent<SpriteRenderer> ();
		m_animator = GetComponent<Animator> ();
		AnimatorClipInfo[] clipInfo = m_animator.GetCurrentAnimatorClipInfo (0);

        //Add a new plant component to initialise with
		AddNewPlantComponent ();

        //for every particle system added
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
        //Set up a random percentage chance
		int chance = Random.Range (0, 100);


        //if the chance is greater than 90 (10% chance)
		if (chance >= 75)
        {
            //spawn debuff plant (lightning plant)
			plantComponentType = PlantComponentType.DEBUFFPLANT;
		}
        //if the chance is greater than 70 but less than 90 (20% chance)
        else if (chance > 55 && chance < 75)
        {
            //Spawn double score plant
			plantComponentType = PlantComponentType.DOUBLESCOREPLANT;
		}
        //otherwise just spawn a generic 1 point plant (70%)
        else
        {
            //Spawn Normal Plant
			plantComponentType = PlantComponentType.NORMALPLANT;
		}

		m_animator.SetTrigger ("Spawn");

		switch (plantComponentType) {
		case PlantComponentType.NORMALPLANT:
			m_animator.enabled = false;
			basePlant = gameObject.AddComponent<NormalPlant> ();
			basePlant.SetSprite (sprites [0]);
			sr.sprite = sprites [0];
			m_animator.enabled = true;
			m_animator.SetTrigger ("BulbPlant");
			break;
		case PlantComponentType.DOUBLESCOREPLANT:
			m_animator.enabled = false;
			basePlant = gameObject.AddComponent<DoubleScorePlant> ();
			basePlant.SetSprite (sprites [1]);
			sr.sprite = sprites [1];
			m_animator.enabled = true;
			m_animator.SetTrigger ("VenusPlant");
			break;
		case PlantComponentType.DEBUFFPLANT:
			m_animator.enabled = false;
			basePlant = gameObject.AddComponent<DebuffPlant> ();
			basePlant.SetSprite (sprites [2]);
			sr.sprite = sprites [2];
			m_animator.enabled = true;
			m_animator.SetTrigger ("YellowPlant");
			break;
		case PlantComponentType.ELECTRICPLANT:
			basePlant = gameObject.AddComponent<ElectricPlant> ();
			basePlant.SetSprite (sprites [3]);
			sr.sprite = sprites [3];
		
			break;
		}    
		basePlant.SetActive (true);
		FirstTimeSpawn = false;
	}


    //if tile is swiped over
	public int Swiped(out BasePlant plant)
    {
		int tempScore = 0;
		plant = basePlant;

        //if the plant is already active
		if (basePlant.GetActive ()) {

            //get the score from the plant
			tempScore = basePlant.GetScore ();
			Debug.Log (basePlant.GetScore ());

            //if the plant is type of double plant
            if (basePlant is DoubleScorePlant)
            {
                //white text
                FloatingTextManager.CreateFloatingText("+" + basePlant.GetScore().ToString(), basePlant.transform, Color.white);
            }
            else if (basePlant is NormalPlant)
            {
                //yellow text
                FloatingTextManager.CreateFloatingText("+" + basePlant.GetScore().ToString(), basePlant.transform);
            }
            else if (basePlant is DebuffPlant)
            {
                FloatingTextManager.CreateFloatingText("+" + basePlant.GetScore().ToString(), basePlant.transform, Color.blue);
                DebuffPlant debuff = basePlant as DebuffPlant;
				Destroy (lightning);
            }
  
            //activate the dead plant animation
			m_animator.SetTrigger ("DeadPlant");

            //Set the plant state to be inactive
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
		//the game has ended
		endGame = true;
        
        //Kill the plant and don't respawn
		basePlant.SetActive (false);
		m_animator.SetTrigger ("DeadPlant");
		for (int i = 0; i < emmiters.Count; i++) {
			emmiters [i].Play ();
		}
	}

	//swaps over lightning debuff on plant 
	void SwapLightningDebuff()
	{
		if (basePlant.GetActive ()) {
			if ((m_stateInfo.normalizedTime % 1) <= 0.5f) {
				DebuffPlant debuff = basePlant as DebuffPlant;
				debuff.SetLightning (true);
				debuff.SetScore (0);
				if (spawnOnceLightning == false) {
					lightning = Instantiate (Resources.Load ("Minigames/PlantMinigame/Prefabs/LightningStrike")) as GameObject;
					lightning.transform.SetParent (basePlant.transform);
					lightning.transform.localPosition = new Vector3 (0, 0, -1);
					spawnOnceLightning = true;
				}

			} else {
				spawnOnceLightning = false;
				DebuffPlant debuff = basePlant as DebuffPlant;
				debuff.SetLightning (false);
				debuff.SetScore (20);
			}
		}
	}

    //update
	void Update()
	{
        //keep refreshing the state information on the animator
		m_stateInfo = m_animator.GetCurrentAnimatorStateInfo (0);

		//if the base plant isn't active
		if (!basePlant.GetActive ()) {  
            //Start respawning a plant
			basePlant.SetSprite(sprites[4]);
            StartTimer ();
		}

        //if the baseplant is a debuff plant
		if (basePlant is DebuffPlant) {
            //start swapping the lightning buff on the plant
			SwapLightningDebuff ();
		}

    }

    //starts the timer to remove plant component
	void StartTimer()
	{
        //if it's not the end of the game 
		if (!endGame) {
            //if we haven't started the spawn cycle yet
			if (!bStartSpawn)
            {
                //set a random respawn time between the times specified in the editor
                startSpawnTime = Random.Range(lowestSpawnTime, highestSpawnTime);
                bStartSpawn = true;
            }
 
            //start timer 
			timer += Time.deltaTime;

			//if time has reached the limit 
			if (timer > startSpawnTime) {
				//remove component, add a new one
				basePlant.RemoveComponent ();
				AddNewPlantComponent ();
				timer = 0f;
                bStartSpawn = false;
            }
		}
	}

}