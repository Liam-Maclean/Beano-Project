using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class Selector : MonoBehaviour
{

    public static List<int> activeMinigames = new List<int>();
    public int sceneIndex;

    public Image onButton;
    public Image offButton;

    public Image indicator;
    public Sprite onSprite;
    public Sprite offSprite;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        activeMinigames.Add(sceneIndex);
        onButton.enabled = false;
        offButton.enabled = true;
        indicator.sprite = onSprite;
    }

    //take text from input field, convert to int, load scene from build index
    public void loadLevel(Text indexString)
    {
        int indexInt;
        //convert string to int
        Int32.TryParse(indexString.text, out indexInt);
        //load scene using build index
        SceneManager.LoadScene(indexInt, LoadSceneMode.Single);
    }

    public void ToggleMinigameOff()
    {
        activeMinigames.RemoveAll(AlreadyThere);
        indicator.sprite = offSprite;
        onButton.enabled = true;
        offButton.enabled = false;
    }

    public void ToggleMinigameOn()
    {
        activeMinigames.RemoveAll(AlreadyThere);
        activeMinigames.Add(sceneIndex);
        indicator.sprite = onSprite;
        offButton.enabled = true;
        onButton.enabled = false;
    }

    bool AlreadyThere(int x)
    {
        return x == sceneIndex;
    }

}
