using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlantScriptManager : MonoBehaviour 
{
	NormalPlant ps;

	public Sprite[] sprites;
	float timer = 0.0f;


	void Start()
	{
		this.AddNewPlantComponent ();
	}


	//add randomised plant component
	public void AddNewPlantComponent()
	{
		
		ps = this.gameObject.AddComponent<NormalPlant> ();
		ps.SetSprite (sprites [0]);
	
	}


	void Update()
	{
		if (!ps.GetActive ()) {
			StartTimer ();
		}
	}

	void StartTimer()
	{
		timer += Time.deltaTime;

		if (timer > 2.0f) {
			ps.RemoveComponent ();
			AddNewPlantComponent();
			timer = 0f;
		}
	}

}