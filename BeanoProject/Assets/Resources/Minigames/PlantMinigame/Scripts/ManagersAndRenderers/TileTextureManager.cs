using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTextureManager : MonoBehaviour {

	public Sprite[] sprites;

	SpriteRenderer sr;
	// Use this for initialization
	void Awake () {
		sr = this.gameObject.GetComponent<SpriteRenderer> ();

		sr.sprite = sprites[Random.Range (0, 6)];
	}

    public void SetTexture(int id)
    {
        sr.sprite = sprites[id];
    }

	// Update is called once per frame
	void Update () {
		
	}
}
