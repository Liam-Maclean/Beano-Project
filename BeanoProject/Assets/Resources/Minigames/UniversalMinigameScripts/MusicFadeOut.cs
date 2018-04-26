using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFadeOut : MonoBehaviour {


	public float fadeOutRate;
    private static AudioSource m_backgroundMusic;

	// Use this for initialization
	void Start ()
    {
        m_backgroundMusic = this.gameObject.GetComponent<AudioSource>();
	}

    public void FadeOut()
    {
		m_backgroundMusic.volume -= fadeOutRate / 1000;
    }


	public static void SetMusicSettings(float vol)
	{
		m_backgroundMusic.volume = vol;
	}
}
