using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScorePlant : BasePlant {
	

	//particle system object
	GameObject m_particleSystemObject;

	//particle system
	ParticleSystem m_particleSystem;

	//initialise (start)
	void Start()
	{
		sr = this.gameObject.GetComponent<SpriteRenderer> ();
		sr.color = new Color (255, 255, 255);
		SetScore (5);
		ActivateGlow ();
	}

	//activate spark particle system
	private void ActivateGlow()
	{
		//Instantiate the particle object from the folder of prefabs
		m_particleSystemObject = Instantiate (Resources.Load ("Minigames/PlantMinigame/Prefabs/ParticleSystems/GlowParticleSystem")) as GameObject;

		//set the parent up
		m_particleSystemObject.transform.SetParent (this.transform);
		m_particleSystemObject.transform.localPosition = new Vector3(0,0, this.transform.position.z+1);

		//get the particle system component and emit from the particle system.
		m_particleSystem = m_particleSystemObject.GetComponent<ParticleSystem> ();
		m_particleSystem.Play ();
	}

	//update
	void Update()
	{
		if (!GetActive()) {
			m_particleSystem.Stop ();
		}
	}


	//override for deleting the component (destroys the particle system object as well)
	public override void RemoveComponent()
	{
		Destroy (m_particleSystemObject);
		base.RemoveComponent ();
	}

	//override for activating plant (swipe over)
	public override void ActivatePlant(out int score)
	{
		base.ActivatePlant (out score);
	}
}
