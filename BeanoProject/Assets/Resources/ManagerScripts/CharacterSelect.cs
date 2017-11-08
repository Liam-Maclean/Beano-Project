using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//==============================================
//
// Character Selection Script
//
// This class controls the selection of characters
//
// Kevin Conaghan - 07/11/17



public class CharacterSelect : MonoBehaviour {

	//Initialise list of character sprites and names
	private GameObject[] characterList;
	public List<string> textList; 

	private int index;

	Text nameText;


	private void Start()
	{
		index = PlayerPrefs.GetInt ("CharacterSelected");

		//Initialise lists
		characterList = new GameObject[transform.childCount];
		nameText = GameObject.FindGameObjectWithTag ("Text").GetComponent<Text>();


		//Fill the array with sprites
		for (int i = 0; i < transform.childCount; i++)
		{
			characterList[i] = transform.GetChild(i).gameObject;
		}
	
		//Toggle of the sprites
		foreach (GameObject go in characterList )
		{
			go.SetActive (false);

		}

		//Toggle on the selected character
		if (characterList [index])
		{
			characterList [index].SetActive (true);
			nameText.text = textList[index];

		}
	}

	//Left button 
	public void Toggleleft()
	{
		//Toggle off the current sprite
		characterList[index].SetActive(false);


		index--; 
		if (index < 0) 
		{
			index = characterList.Length - 1;

		}

		//Toggle on the new sprite
		characterList[index].SetActive(true);
		nameText.text = textList[index];

	}

	//Right button
	public void ToggleRight()
	{
		//Toggle off the current sprite
		characterList[index].SetActive(false);

		//Increment index so it can cycle through the list of characters
		index++;

		if (index == characterList.Length)
		{
			// When at maximum character list reset to return to original character creating a loop effect
			index = 0;
		}

		//Toggle on the new sprite
		characterList[index].SetActive(true);
		nameText.text = textList [index];
	}

	//Confirm button
	public void ConfirmButton()
	{
		//Save the player's choice of character
		PlayerPrefs.SetInt ("CharacterSelected", index);

		//Load the next scene
		SceneManager.LoadScene ("PlantMinigamescene");
	}

	// Update is called once per frame
	void Update ()
	{
		
		
	}
}
