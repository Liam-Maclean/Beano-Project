using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Double Score Plant
/// 
/// Plant component with glow particle system attached.
/// 
/// When swiped over, has a score of 5 and a tint of gold.
/// 
/// encapsulated particle system that destroys when not in use
/// 
/// Liam MacLean 17/02/2018, 16:02
/// </summary>


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
		SetScore (50);
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

	//override activate plant method (not in use)
	public override void ActivatePlant(out int score)
	{
		base.ActivatePlant (out score);
	}
}
