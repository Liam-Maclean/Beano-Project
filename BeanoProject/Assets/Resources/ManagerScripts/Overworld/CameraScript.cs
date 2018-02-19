using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float zoomSpeed;

    public float targetZoom;
    public Vector3 targetPos;
    public Camera orthoCamera;

    private float newSpaceBuffer = 0.2f;
    private float m_momentum = 0;
    private Vector3 m_moveVelo = new Vector3(0, 0, 0);

    void FixedUpdate()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touchOldPos0 = touch0.position - touch0.deltaPosition;
            Vector2 touchOldPos1 = touch1.position - touch1.deltaPosition;

            float oldTouchMag = (touchOldPos0 - touchOldPos1).magnitude;
            float currTouchMag = (touch0.position - touch1.position).magnitude;

            float magDifference = oldTouchMag - currTouchMag;

            orthoCamera.orthographicSize += magDifference * zoomSpeed;
            orthoCamera.orthographicSize = Mathf.Max(orthoCamera.orthographicSize, 0.1f);
        }
        else
        {

            Vector3 currPos = transform.position;

            if (currPos.x <= targetPos.x - newSpaceBuffer || currPos.x >= targetPos.x + newSpaceBuffer || currPos.y <= targetPos.y - newSpaceBuffer || currPos.y >= targetPos.y + newSpaceBuffer)
            {
                m_moveVelo.x = targetPos.x - currPos.x;
                m_moveVelo.y = targetPos.y - currPos.y;
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

                transform.position += (m_moveVelo * m_momentum * Time.deltaTime);
            }
            else
            {
                m_momentum = 0;
            }

            float currSize = orthoCamera.orthographicSize;

            if (currSize != targetZoom)
            {
                if (targetZoom > currSize && currSize < 10)
                {
                    currSize += 0.01f;
                }
                else if (currSize > 3)
                {
                    currSize -= 0.01f;
                }

                orthoCamera.orthographicSize = currSize;
            }
        }
    }

    public void SetTargets(Vector3 pos)
    {
        targetPos = pos;

        float distance = Vector3.Distance(Vector3.zero, targetPos);
        targetZoom = distance / 2;

        if (targetZoom < 0)
        {
            targetZoom *= -1;
        }
    }
}
