using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CharacterSelect : MonoBehaviour {

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


	public void ToggleRight()
	{
		//Toggle off the current sprite
		characterList[index].SetActive(false);

		index++;
		if (index == characterList.Length)
		{
			index = 0;
		}

		//Toggle on the new sprite
		characterList[index].SetActive(true);
		nameText.text = textList [index];
	}

	public void ConfirmButton()
	{
		PlayerPrefs.SetInt ("CharacterSelected", index);
		SceneManager.LoadScene ("TestScene");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
