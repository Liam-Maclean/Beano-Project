using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public Sprite[] backSprite;

    // Use this for initialization
    void Start()
    {

    }

    public void SetSprite(int currBiome)
    {
        this.GetComponent<SpriteRenderer>().sprite = backSprite[currBiome];
    }
}
