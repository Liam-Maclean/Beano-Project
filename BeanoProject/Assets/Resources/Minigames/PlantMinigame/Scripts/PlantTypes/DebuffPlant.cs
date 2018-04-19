using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Debuff plant (swipe over and you will not be able to swipe for a while
public class DebuffPlant : BasePlant {

	//particle system object
	GameObject m_particleSystemObject;

	//particle system
	ParticleSystem m_particleSystem;

	private float m_duration;
	private bool bLightningOnOff;

	//Setter
	public void SetLightning(bool lightning)
	{
		bLightningOnOff = lightning;
	}

	//Getter
	public bool GetLightning()
	{
		return bLightningOnOff;
	}

	//initialise (start)
	void Start()
	{
		//set up timer 
		m_duration = Random.Range (1, 2);
		sr = this.gameObject.GetComponent<SpriteRenderer> ();
		sr.color = new Color (255, 255, 0);
		SetScore (0);
		ActivateSparks ();
		bLightningOnOff = true;
	}

	//activate spark particle system
	private void ActivateSparks()
	{
		//Instantiate the particle object from the folder of prefabs
		m_particleSystemObject = Instantiate (Resources.Load ("Minigames/PlantMinigame/Prefabs/ParticleSystems/LightningParticleSystem")) as GameObject;

		//set the parent up
		m_particleSystemObject.transform.SetParent (this.transform);
		m_particleSystemObject.transform.localPosition = new Vector3(0,0, this.transform.position.z+1);

		//get the particle system component and emit from the particle system.
		m_particleSystem = m_particleSystemObject.GetComponent<ParticleSystem> ();
		m_particleSystem.Play ();
	}

	//Duration ended
	private bool DurationEnded()
	{
		if (m_duration <= 0) {
			return true;
		} else {
			return false;
		}
	}

	//CountDown Duration of Debuff
	private void CountdownTimer()
	{
		//Duration countdown
		m_duration -= Time.deltaTime;
	}

	//update
	void Update()
	{
		CountdownTimer ();
		//if (DurationEnded ()) {
		//	SetActive(false);
		//}

		if (!GetActive()) {
			m_particleSystem.Stop ();
		}
	}

	//override for deleting the component (destroys the particle system object as well)
	public override void RemoveComponent()
	{
		//Destroy particle system and component system
		Destroy (m_particleSystemObject);
		base.RemoveComponent ();
	}

	//override activate plant method (not in use)
	public override void ActivatePlant(out int score)
	{
		base.ActivatePlant (out score);
		//base.ActivatePlant (out score);
	}
}
