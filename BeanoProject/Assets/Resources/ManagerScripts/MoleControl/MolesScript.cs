﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolesScript : MonoBehaviour
{
    public enum MoleType { Normal, Evil, Frozen };
    private MoleType m_currMole;

    public enum MoleState { Enter, Idle, Hit, Exit };
    private MoleState m_currState;

    public Sprite[] moleSprites;

    private int m_posID;
    public float goodRate;
    public float freezeRate;

    public float maxTime;
    public float minTime;
    private float m_currTime;

    private GameObject m_moleManager;

    private bool m_hitOnce;

    private void Awake()
    {
        m_currTime = Random.Range(minTime, maxTime);
        m_moleManager = GameObject.FindGameObjectWithTag("MoleManager");
    }

    // Use this for initialization
    void Start ()
    {
        if (Random.Range(0.0f, 1.0f) <= goodRate)
        {
            m_currMole = MoleType.Normal;
        }
        else if (Random.Range(0.0f, 1.0f) <= freezeRate)
        {
            m_currMole = MoleType.Frozen;
        }
        else
        {
            m_currMole = MoleType.Evil;
        }

        m_hitOnce = false;

        this.GetComponent<SpriteRenderer>().sprite = moleSprites[(int)m_currMole];
        //this.GetComponent<Animator>().con = moleSprites[(int)m_currMole];

        m_currState = MoleState.Enter;
    }
	
	// Update is called once per frame
	void Update ()
    {
        switch(m_currState)
        {
            case MoleState.Enter:
                // Enter Animation
                m_currState = MoleState.Idle;
                break;
            case MoleState.Idle:
                if (m_currTime > 0)
                {
                    m_currTime -= Time.deltaTime;
                }
                else
                {
                    m_currState = MoleState.Exit; 
                }
                break;
            case MoleState.Hit:
                m_moleManager.GetComponent<MoleGameManagerScript>().ResetSpawner(m_posID);
                // Hit Animation
                Object.Destroy(this.gameObject);
                break;
            case MoleState.Exit:
                m_moleManager.GetComponent<MoleGameManagerScript>().ResetSpawner(m_posID);
                // Leave Aniamtion
                Object.Destroy(this.gameObject);
                break;
            default:
                break;
        }
	}

    public void InitMole(int posID)
    {
        m_posID = posID;

        switch (m_posID)
        {
            case 0:
                transform.position = new Vector3(-3.0f, 2.0f, -1.00f);
                break;
            case 1:
                transform.position = new Vector3(0.0f, 2.0f, -1.01f);
                break;
            case 2:
                transform.position = new Vector3(3.0f, 2.0f, -1.02f);
                break;

            case 3:
                transform.position = new Vector3(-3.0f, 0.0f, -1.10f);
                break;
            case 4:
                transform.position = new Vector3(0.0f, 0.0f, -1.11f);
                break;
            case 5:
                transform.position = new Vector3(3.0f, 0.0f, -1.12f);
                break;

            case 6:
                transform.position = new Vector3(-3.0f, -2.0f, -1.20f);
                break;
            case 7:
                transform.position = new Vector3(0.0f, -2.0f, -1.21f);
                break;
            case 8:
                transform.position = new Vector3(3.0f, -2.0f, -1.22f);
                break;

            default:
                transform.position = new Vector3(0.0f, 0.0f, -1.0f);
                break;
        }
    }

    public void Hit()
    {
        switch (m_currMole)
        {
            case MoleType.Normal:
                // REMOVE SCORE
                m_currState = MoleState.Hit;
                Debug.Log("Normal Mole Hit");
                m_moleManager.GetComponent<MoleGameManagerScript>().IncrementScore(-10, this.transform.position.x, this.transform.position.y);
                break;
            case MoleType.Frozen:
                if (m_hitOnce == false)
                {
                    Debug.Log("Ice Mole Hit");
                    m_hitOnce = true;
                    m_moleManager.GetComponent<MoleGameManagerScript>().IncrementScore(5, this.transform.position.x, this.transform.position.y);
                }
                else
                {
                    m_currMole = MoleType.Normal;
                    this.GetComponent<SpriteRenderer>().sprite = moleSprites[(int)m_currMole];
                    Debug.Log("Ice Mole Convert");
                    m_moleManager.GetComponent<MoleGameManagerScript>().IncrementScore(15, this.transform.position.x, this.transform.position.y);
                }
                break;
            case MoleType.Evil:
                m_currState = MoleState.Hit;
                Debug.Log("Bad Mole Hit");
                m_moleManager.GetComponent<MoleGameManagerScript>().IncrementScore(20, this.transform.position.x, this.transform.position.y);
                break;
            default:
                break;
        }
    }

    public void End()
    {
        m_currState = MoleState.Exit;
    }
}