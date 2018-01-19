
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==============================================
//
// Touch Manager Script
//
// This class controls all the necessary functionality involving touch controls on Android and iOS
//
// This class will most likely be CHANGED after prototyping
// 
// Contains vector maths using Physics2D library and touch controls
// 
// Also deals with screen orientation a bit
//
// Liam MacLean - 25/10/2017 03:42
public class TouchScript : MonoBehaviour
{
    private int m_combinedScore;
    private List<int> m_plantScore = new List<int>();

    private ManagerScript manager;


    private Vector2 m_touchBegin;
    private Vector2 m_touchEnd;
    private Vector2 m_swipeDirection;
    private Vector2 m_distance;

    // Use this for initialization
    void Start()
    {
		//manager script initialisation
        manager = this.GetComponent<ManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        SwipeLine();
    }


    //does Kevins work but appeals to touch controls for the specific minigame
    void SwipeLine()
    {
        //if there is touch input
        if (Input.touchCount == 1)
        {
            //get that touch input
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                //if the touch has began
                case TouchPhase.Began:
                {
                        //store that start position
                        m_touchBegin = touch.position;
                }
                break;

                //if the touch has ended
				case TouchPhase.Ended:
                {
                        //store the end position
                   		m_touchEnd = touch.position;

                        var ray2 = Camera.main.ScreenPointToRay(m_touchBegin);

					Vector2 directionPreNorm = (m_touchEnd - m_touchBegin);
                        m_swipeDirection = (m_touchEnd - m_touchBegin);

                        //normalize
                        m_swipeDirection.Normalize();

                        //check for multiple hits from a raycast and store them
						RaycastHit2D[] hit = Physics2D.RaycastAll(ray2.origin, m_swipeDirection, m_swipeDirection.magnitude);

                        //for everything hit by the raycast
                        for (int i = 0; i < hit.Length; i++)
                        {
                            //check if any of them are plants, if they are
                            if (hit[i].collider.tag == "Plant")
                            {
                                //Get that plants script and set it to swiped
                                PlantScriptManager tempPlantScript = hit[i].collider.gameObject.GetComponent<PlantScriptManager>();

                                //add the plants score to the list of scores
                                m_plantScore.Add(tempPlantScript.Swiped());
                            }
                        }

                        //for each score swiped
                        for (int i = 0; i < m_plantScore.Count; i++)
                        {
                            //add that to the game score (DO SOMETHING FUNKY WITH MULTIPLIERS HERE)
                            m_combinedScore += m_plantScore[i];
                        }
                        m_combinedScore *= m_plantScore.Count;
                        //manager.IncrementScore(m_combinedScore);
                        m_combinedScore = 0;
                        m_plantScore.Clear();
                }
                break;
           }
        }
    }
}
