using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnerScript : MonoBehaviour
{
    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Pedestrian")
        {
            collision.gameObject.GetComponent<PedScript>().Despawn();
        }
	}
}
