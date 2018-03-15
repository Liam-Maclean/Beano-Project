using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitsHandlerScript : MonoBehaviour {

    public Sprite[] portraitSprite;
    private int m_portraitID;
    private int m_playerCount;
    private int m_rank;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_playerCount > 0)
        {

        }
    }

    public void InitPortrait(int playerCount, int currActor, int portraitID)
    {
        this.GetComponent<SpriteRenderer>().sprite = portraitSprite[currActor];
    }

    public void SetRank(int newRank)
    {
        m_rank = newRank;
    }
}
