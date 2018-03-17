using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CharacterSelect : MonoBehaviour {

	private GameObject[] characterList;
	public List<string> textList; 


	private int index;
    private int left;
    private int right;

	//Text nameText;


    //public InputField handle;

    private Vector2 touchNewPos;
    private Vector2 touchOldPos;

    private static bool onPortrait = false;

	private void Start()
	{
		index = PlayerPrefs.GetInt ("Avatar");
        left = index + 1;
        right = index - 1;

        //Initialise lists
        characterList = new GameObject[transform.childCount];
		//nameText = GameObject.FindGameObjectWithTag ("Text").GetComponent<Text>();


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
			//nameText.text = textList[index];

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
        left = index + 1;
        right = index - 1;
        if (left > characterList.Length)
        {
            left = 0;
        }
        if (right < 0)
        {
            right = characterList.Length;
        }

        //Toggle on the new sprite
        characterList[index].SetActive(true);
		//nameText.text = textList[index];

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
        left = index + 1;
        right = index - 1;
        if (left > characterList.Length)
        {
            left = 0;
        }
        if (right < 0)
        {
            right = characterList.Length;
        }

        //Toggle on the new sprite
        characterList[index].SetActive(true);
		//nameText.text = textList [index];
	}

	public void ConfirmButton()
	{
		PlayerPrefs.SetInt ("Avatar", index);
        //PlayerPrefs.SetString("Handle", handle.text);
        //SceneManager.UnloadSceneAsync(2);
        //FindObjectOfType<Navigator>().GetComponent<Navigator>().ReturnFromSelect();
	}

    public void Enter()
    {
        onPortrait = true;
    }

    public void Exit()
    {
        onPortrait = true; ;
    }

	// Update is called once per frame
	void Update () {
        
		if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            touchNewPos = touch.position;
            if (touchOldPos.x != 99999) //Vector2 is non nullable, this is a simple but flawed way around that
            {
                Vector2 direction = touchNewPos - touchOldPos;
                if (onPortrait)
                {
                    if (direction.x > 0)
                    {
                        ToggleRight();
                        onPortrait = false;
                    }
                    else if (direction.x < 0)
                    {
                        Toggleleft();
                        onPortrait = false;
                    }
                }
            }
            touchOldPos = touchNewPos;
        }
        else
        {
            touchOldPos.x = 99999;
        }
	}
}
