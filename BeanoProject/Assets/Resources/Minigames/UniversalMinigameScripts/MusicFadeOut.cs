using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFadeOut : MonoBehaviour {



    private AudioSource m_backgroundMusic;

	// Use this for initialization
	void Start ()
    {
        m_backgroundMusic = this.gameObject.GetComponent<AudioSource>();
	}

    public void FadeOut()
    {
        m_backgroundMusic.volume -= 0.0005f;
    }

}
