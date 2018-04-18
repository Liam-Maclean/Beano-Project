using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Transition manager scripts
/// 
/// contains all the data for transitions made within the scene
/// 
/// Holds a bool to check if the scene should be loaded asynchronously, a string of the scene name or number
/// and the load scene mode of the scene we want to load (additive or single)
/// 
/// Liam MacLean, 14:39 17/04/2018
/// </summary>
public class TransitionManagerScript : MonoBehaviour {

	//Variables
	private int m_animationChosen;
	private Animator m_animator;
	private string m_sceneNumberToChangeTo;
	private LoadSceneMode m_loadSceneMode;
	private bool m_loadAsynchronously;

	//getter
	public bool GetLoadAsynchronously()
	{
		return m_loadAsynchronously;
	}

	//setter
	public void SetLoadAsynchronously(bool sync)
	{
		m_loadAsynchronously = sync;
	}

	//getter
	public LoadSceneMode GetLoadSceneMode()
	{
		return m_loadSceneMode;
	}

	//setter
	public void SetLoadSceneMode(LoadSceneMode sceneMode)
	{
		m_loadSceneMode = sceneMode;
	}
		
	//getter
	public string GetSceneNumberToChangeTo()
	{
		return m_sceneNumberToChangeTo;
	}
		
	//setter
	public void SetSceneNumberToChangeTo(string number)
	{
		m_sceneNumberToChangeTo = number;
	}

	// Use this for initialization
	void Start () {
		//on start, randomise transition
		Random.InitState (System.Environment.TickCount);
		m_animator = GetComponent<Animator> ();
		m_animationChosen = Random.Range (0, 2);

		//if 0 
		if (m_animationChosen == 0) {
			//pie transition in animator
			m_animator.SetTrigger ("PieTransition");
		//if 1 
		} else  if (m_animationChosen == 1){
			//tomato transition in animator
			m_animator.SetTrigger ("TomatoTransition");
		}
	}
}
