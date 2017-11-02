using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class Selector : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
}
