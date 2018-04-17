using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionManagerScript : MonoBehaviour {
	
	private int m_animationChosen;
	private Animator m_animator;
	private string m_sceneNumberToChangeTo;
	private LoadSceneMode m_loadSceneMode;


	public LoadSceneMode GetLoadSceneMode()
	{
		return m_loadSceneMode;
	}

	public void SetLoadSceneMode(LoadSceneMode sceneMode)
	{
		m_loadSceneMode = sceneMode;
	}
		
	public string GetSceneNumberToChangeTo()
	{
		return m_sceneNumberToChangeTo;
	}

	public void SetSceneNumberToChangeTo(string number)
	{
		m_sceneNumberToChangeTo = number;
	}

	// Use this for initialization
	void Start () {
		Random.InitState (System.Environment.TickCount);
		m_animator = GetComponent<Animator> ();
		m_animationChosen = Random.Range (0, 2);

		if (m_animationChosen == 0) {
			m_animator.SetTrigger ("PieTransition");
		} else  if (m_animationChosen == 1){
			m_animator.SetTrigger ("TomatoTransition");
		}
	}
}
