using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedScript : MonoBehaviour
{
    public Sprite[] pedSprites;

    public float moveSpeed;
    private bool m_isLeft;
    private int m_zPos;

	void Start ()
    {
	    	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_isLeft)
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }
    }

    public void InitPed(bool isLeft, int zPos)
    {
        m_isLeft = isLeft;
        m_zPos = zPos;
    }

    public void Despawn()
    {
        Destroy(this.gameObject);
    }
}
