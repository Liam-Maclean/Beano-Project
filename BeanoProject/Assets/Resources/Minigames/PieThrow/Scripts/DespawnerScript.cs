using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Simple destruction script for when the enemy and frienly targets go out of the screen view>
/// Despawner script.
/// <The reason there has to be a timer is because the targets spawn out of the screen and you dont 
///  want them to be destroyed as soon as they have spawned>




public class DespawnerScript : MonoBehaviour
{
	private float timer = 5.0f;
	public float leftBound;
	public float rightBound;

	void Update()
	{
		//counting down 1 second 
		timer -= Time.deltaTime;

		//if the gameObject is out of the camera screen on the left side destroy
		if(timer <= 0.0f && gameObject.transform.position.x <= leftBound)
			{
				Destroy(gameObject);
			}

		//if the gameObject is out of the camera screen on the right side destroy
		if (timer <= 0.0f && gameObject.transform.position.x >= rightBound)
		{
			Destroy (gameObject);
		}
	}
}
