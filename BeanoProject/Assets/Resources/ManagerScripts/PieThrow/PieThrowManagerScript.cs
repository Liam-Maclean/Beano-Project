using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieThrowManagerScript : MonoBehaviour
{
    private enum GAMESTATE { Start, Playing, Finished};
    private GAMESTATE m_currState;
    //private int m_clientID;

    public Animator readyMenuAnim;

    public GameObject readyMenu;

    void Awake()
    {
        m_currState = GAMESTATE.Start;
    }

	// Use this for initialization
	void Start ()
    {
        // Set Client ID
        // Set Portraits
        // Set Powerup state

        // Call start menu
        StartMenu();
	}
	
	// Update is called once per frame
	void Update ()
    {
		switch (m_currState)
        {
            case GAMESTATE.Start:
                //start FUNC only;
                break;
            case GAMESTATE.Playing:
                // Normal Gameplay
                break;
            case GAMESTATE.Finished:
                // Outro Plz
                break;
            default:
                Debug.Log("GameState Error");
                break;
        }
	}

    // Called at the begining of the game to make sure all users are loaded into the game correctly
    void StartMenu()
    {
        readyMenuAnim.SetBool("Active", true);
    }

    // Called to start the active game & timers
    void StartGame()
    {
        
    }

    IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
    }
}
