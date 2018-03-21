using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera class
public class CameraScript : MonoBehaviour
{
    // Public variables
    public float zoomSpeed;
    public float swipeSpeed;
    public float spaceBuffer;
    public float cooldownMax;

    public Camera orthoCamera;

    // Private variables
    private float m_cooldownMoveCounter;
    private float m_cooldownZoomCounter;
    private float m_momentum;
    private float m_targetZoom;
    private Vector3 m_targetPos;
    private Vector3 m_moveVelo;
    private Vector3 m_leadPos;

    // Fixed update
    void FixedUpdate()
    {
        // Reset variables based on current positions
        Vector3 currPos = transform.position;
        float currSize = orthoCamera.orthographicSize;
        m_targetZoom = currSize;

        // Camera movemnt based on user input
    //    if (Input.touchCount == 1)
    //    {
    //        // Set touch parameters
    //        Touch touch = Input.GetTouch(0);
    //        m_targetPos.x += touch.deltaPosition.x;
    //        m_targetPos.y += touch.deltaPosition.y;

    //        // Caluculate movement
    //        Vector2 touchOldPos = touch.position - touch.deltaPosition;
    //        float touchMag = (touchOldPos - touch.position).magnitude;

    //        // Translate movement
    //        m_targetPos.Normalize();
    //        m_targetPos *= touchMag * swipeSpeed * m_targetZoom;

    //        // Apply movement
    //        transform.position -= m_targetPos * Time.deltaTime;

    //        // Reset counter
    //        m_cooldownMoveCounter = cooldownMax;
    //    }
    //    // Idle countdown
    //    else if (m_cooldownMoveCounter > 0)
    //    {
    //        m_cooldownMoveCounter -= Time.deltaTime;
    //    }
    //    // Base movement on last set node
    //    else
    //    {
    //        // Set target to node
    //        m_targetPos = m_leadPos;

    //        // Avoid snapping
    //        if (currPos.x <= m_targetPos.x - spaceBuffer || currPos.x >= m_targetPos.x + spaceBuffer || currPos.y <= m_targetPos.y - spaceBuffer || currPos.y >= m_targetPos.y + spaceBuffer)
    //        {
    //            // Create a velocity based on target position
    //            m_moveVelo.x = m_targetPos.x - currPos.x;
    //            m_moveVelo.y = m_targetPos.y - currPos.y;
    //            m_moveVelo.Normalize();

    //            // Calculate momentum
    //            if (m_momentum == 0)
    //            {
    //                m_momentum = 0.1f;
    //            }
    //            else
    //            {
    //                if (m_momentum < 0.75f)
    //                {
    //                    m_momentum = m_momentum * 1.25f;
    //                }
    //                else
    //                {
    //                    m_momentum = 1.0f;
    //                }
    //            }
                
    //            // Apply velocity based on momentum
    //            transform.position += (m_moveVelo * m_momentum * Time.deltaTime);
    //        }
    //        // Stop if micro movement
    //        else
    //        {
    //            m_momentum = 0;
    //        }
    //    }

    //    // Set variable with updated position
    //    currPos = transform.position;

    //    // Restrict movement based within overworld
    //    bool isLimited = false;
    //    if (currPos.x < -8.0f)
    //    {
    //        currPos.x = -8.0f;
    //        isLimited = true;
    //    }
    //    else if (currPos.x > 8.0f)
    //    {
    //        currPos.x = 8.0f;
    //        isLimited = true;
    //    }

    //    if (currPos.y < -4.5f)
    //    {
    //        currPos.y = -4.5f;
    //        isLimited = true;
    //    }
    //    else if (currPos.y > 4.5f)
    //    {
    //        currPos.y = 4.5f;
    //        isLimited = true;
    //    }

    //    // Set position based on updated movement
    //    if (isLimited)
    //    {
    //        transform.position += currPos - transform.position;
    //    }

    //    // Pinch zoom control
    //    if (Input.touchCount == 2)
    //    {
    //        // Set touch parameters
    //        Touch touch0 = Input.GetTouch(0);
    //        Touch touch1 = Input.GetTouch(1);

    //        // Caluculate zoom magnitude
    //        Vector2 touchOldPos0 = touch0.position - touch0.deltaPosition;
    //        Vector2 touchOldPos1 = touch1.position - touch1.deltaPosition;
    //        float oldTouchMag = (touchOldPos0 - touchOldPos1).magnitude;
    //        float currTouchMag = (touch0.position - touch1.position).magnitude;
    //        float magDifference = oldTouchMag - currTouchMag;

    //        // Change zoom
    //        if (magDifference > 0)
    //        {
    //            m_targetZoom += zoomSpeed;
    //        }
    //        else if (magDifference < 0)
    //        {
    //            m_targetZoom += -zoomSpeed;
    //        }

    //        // Apply zoom
    //        orthoCamera.orthographicSize = m_targetZoom;

    //        // Reset zoom counter
    //        m_cooldownZoomCounter = cooldownMax;
    //    }
    //    // Idle countdown
    //    else if (m_cooldownZoomCounter > 0)
    //    {
    //        m_cooldownZoomCounter -= Time.deltaTime;
    //    }
    //    // Reset zoom to default value
    //    else
    //    {
    //        if (m_targetZoom < 2.5f - spaceBuffer)
    //        {
    //            m_targetZoom += zoomSpeed;
    //            orthoCamera.orthographicSize = m_targetZoom;
    //        }
    //        else if (m_targetZoom > 2.5 + spaceBuffer)
    //        {
    //            m_targetZoom += -zoomSpeed;
    //            orthoCamera.orthographicSize = m_targetZoom;
    //        }
    //    }

    //    // Restrict zoom to overworld
    //    if (orthoCamera.orthographicSize < 1)
    //    {
    //        orthoCamera.orthographicSize = 1;
    //    }
    //    else if (orthoCamera.orthographicSize > ZoomScaler(currPos))
    //    {
    //        orthoCamera.orthographicSize = ZoomScaler(currPos);
    //    }
    //}

    //// Zoom scaler based on position (LOTS A MATH)
    //float ZoomScaler(Vector3 currPos)
    //{
    //    float xScale = currPos.x;
    //    float yScale = currPos.y;

    //    // Keep positive
    //    if (xScale < 0.0f)
    //    {
    //        xScale *= -1.0f;
    //    }

    //    // Limit max position
    //    if (xScale > 8.0f)
    //    {
    //        xScale = 8.0f;
    //    }

    //    // Apply matrix (Max zoom at centre minus ((change in zoom devided by max position) times current position)
    //    xScale = 5.0f - (0.5f * xScale);

    //    // Keep positive
    //    if (yScale < 0.0f)
    //    {
    //        yScale *= -1.0f;
    //    }

    //    // Limit max position
    //    if (yScale > 4.5f)
    //    {
    //        yScale = 4.5f;
    //    }

    //    // Apply matrix (Max zoom at centre minus ((change in zoom devided by max position) times current position)
    //    yScale = 5.0f - ((2.0f / 3.0f) * yScale);

    //    // Output lowest axis value
    //    if (xScale > yScale)
    //    {
    //        return yScale;
    //    }
    //    else
    //    {
    //        return xScale;
    //    }
    }

    // Update current node
    public void SetTargets(Vector3 pos)
    {
        m_leadPos = pos;
    }
}
