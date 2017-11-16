﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Vector3 startPos;
    public float moveSpeed;
    public float spaceBuffer;

    private float newSpaceBuffer;
    private float m_momentum = 0;
    private Vector3 m_moveVelo = new Vector3(0, 0, 0);

    private Vector3 m_currPos;
    private Vector3 m_targetPos;
    private Vector3 m_oldTargetPos;

    private bool m_oldTargetMet;

    //PUBLIC FOR TESTING PERPOSES ONLY ---to be made private!---
    public int m_playerID;
    public int m_sausageCount;
    public int m_ranking; // To be utailised once scoring is inplace

    enum CharacterID {Dennis, Gnasher, Walter, DennisDad};
    private CharacterID m_currChar;

    void Start ()
    {
        m_oldTargetMet = false;
	}

    public void InitPlayer(int playerID, int currChar, int sausageCount)
    {
        m_playerID = playerID;
        m_currChar = (CharacterID)currChar;
        m_sausageCount = sausageCount;

        /// CHANGE PLAYER ID WITH RANKINGS WHEN IMPLEMENTED
        newSpaceBuffer = spaceBuffer + playerID;
    }

    void FixedUpdate()
    {
        m_currPos = transform.position;

        if (m_oldTargetMet)
        {
            if (m_currPos.x <= m_targetPos.x - newSpaceBuffer || m_currPos.x >= m_targetPos.x + newSpaceBuffer || m_currPos.y <= m_targetPos.y - newSpaceBuffer || m_currPos.y >= m_targetPos.y + newSpaceBuffer)
            {
                m_moveVelo.x = m_targetPos.x - m_currPos.x;
                m_moveVelo.y = m_targetPos.y - m_currPos.y;
                m_moveVelo.Normalize();

                if (m_momentum == 0)
                {
                    m_momentum = 0.1f;
                }
                else
                {
                    if (m_momentum < 0.75f)
                    {
                        m_momentum = m_momentum * 1.25f;
                    }
                    else
                    {
                        m_momentum = 1.0f;
                    }
                }

                transform.position += (m_moveVelo * moveSpeed * m_momentum * Time.deltaTime);

                Vector3 newPos = transform.position;
                newPos.z = newPos.y;
                transform.position = newPos;
            }
            else
            {
                m_momentum = 0;

                if (m_playerID == 0)
                {
                    Debug.Log("Destination Reached");
                    GameObject GM = GameObject.FindGameObjectWithTag("GameManager");
                    GM.GetComponent<OverworldScript>().NodeReached();
                }
            }
        }
        else
        {
            if (m_currPos.x <= m_oldTargetPos.x - spaceBuffer || m_currPos.x >= m_oldTargetPos.x + spaceBuffer || m_currPos.y <= m_oldTargetPos.y - spaceBuffer || m_currPos.y >= m_oldTargetPos.y + spaceBuffer)
            {
                m_moveVelo.x = m_oldTargetPos.x - m_currPos.x;
                m_moveVelo.y = m_oldTargetPos.y - m_currPos.y;
                m_moveVelo.Normalize();

                if (m_momentum == 0)
                {
                    m_momentum = 0.1f;
                }
                else
                {
                    if (m_momentum < 0.75f)
                    {
                        m_momentum = m_momentum * 1.25f;
                    }
                    else
                    {
                        m_momentum = 1.0f;
                    }
                }

                transform.position += (m_moveVelo * moveSpeed * m_momentum * Time.deltaTime);

                Vector3 newPos = transform.position;
                newPos.z = newPos.y;
                transform.position = newPos;
            }
            else
            {
                m_oldTargetMet = true;
            }
        }
    }

    public void SetTargetPos(Vector3 newTargetPos)
    {
        m_oldTargetPos = m_targetPos;
        m_targetPos = newTargetPos;

        if (m_currPos.x <= m_oldTargetPos.x - spaceBuffer || m_currPos.x >= m_oldTargetPos.x + spaceBuffer || m_currPos.y <= m_oldTargetPos.y - spaceBuffer || m_currPos.y >= m_oldTargetPos.y + spaceBuffer)
        {
            m_oldTargetMet = false;
        }
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }
}