using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTextureManager : MonoBehaviour {

	public Sprite[] sprites;

	SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		sr = this.gameObject.GetComponent<SpriteRenderer> ();

		sr.sprite = sprites[Random.Range (0, sprites.Length)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
