using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public float musicVolume;
    public float soundVolume;
    public Slider sound;
    public Slider music;
    
    // Use this for initialization
	void Start () {
        //get stored values for music and sfx
        musicVolume = PlayerPrefs.GetFloat("Music Volume");
        soundVolume = PlayerPrefs.GetFloat("Sound Volume");
        //change sliders to reflect those values
        music.value = musicVolume;
        sound.value = soundVolume;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MusicVolume()
    {
        //change music volume to match slider value and store it
        musicVolume = music.value;
        PlayerPrefs.SetFloat("Music Volume", musicVolume);
    }

    public void SoundVolume()
    {
        //change sfx volume to match slider value and store it
        soundVolume = sound.value;
        PlayerPrefs.SetFloat("Sound Volume", soundVolume);
    }
}
