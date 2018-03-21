using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconHandlerScripts : MonoBehaviour
{
    public float cooldownMax;
    public float alphaMax;
    private float m_currTime;
    private float m_alphaValue;

    public Sprite[] hammerSprite;
    private int m_hammerID;
    private int m_playerCount;

    // Use this for initialization
    void Start()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        m_currTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_currTime > 0)
        {
            m_alphaValue = (alphaMax / cooldownMax) * m_currTime;
            this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, m_alphaValue);
            m_currTime -= Time.deltaTime;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }

    public void InitHammer(int playerCount, int currActor, int hammerID)
    {
        this.GetComponent<SpriteRenderer>().sprite = hammerSprite[currActor];
        m_hammerID = hammerID;
        m_playerCount = playerCount;
    }

    public void Touched(float xPos, float yPos)
    {
        transform.position = new Vector3(xPos, yPos, -3.0f);
        m_currTime = cooldownMax;
    }
}