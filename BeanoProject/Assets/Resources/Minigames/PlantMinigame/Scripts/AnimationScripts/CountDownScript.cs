using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Script used to check if an animation has ended 
/// 
/// contains baseline functions for the countdown animation at the start of the game
/// 
/// Liam MacLean 17/02/2018, 15:58
/// </summary>

public class CountDownScript : MonoBehaviour {
	public Animator m_animator;
	private Text m_countDownText;
	private AnimatorStateInfo m_stateInfo;

	// Use this for initialization
	void Start () {
		m_countDownText = GetComponent<Text> ();
		AnimatorClipInfo[] clipInfo = m_animator.GetCurrentAnimatorClipInfo (0);
	}

	//return current animation time (in float)
	public float ReturnNormalizedAnimationTime()
	{
		return m_stateInfo.normalizedTime;
	}

	//Has animation ended
	public bool AnimationEnded()
	{
		if (m_stateInfo.normalizedTime >= 1.0f) {
			return true;
		} else {
			return false;
		}
	}

		
	// Update is called once per frame
	void Update () {
		
		//get animation state information every frame (time information)
		m_stateInfo = m_animator.GetCurrentAnimatorStateInfo (0);

		//if animation is in the first 0-25% 
		if (m_stateInfo.normalizedTime >= 0.0f && m_stateInfo.normalizedTime <= 0.25f) {
			m_countDownText.text = "3";	
		}
		//if animation is in the first 25-50% 
		if (m_stateInfo.normalizedTime >= 0.25f && m_stateInfo.normalizedTime <= 0.50f) {
			m_countDownText.text = "2";	
		}
		//if animation is in the first 50-75% 
		if (m_stateInfo.normalizedTime >= 0.50f && m_stateInfo.normalizedTime <= 0.75f) {
			m_countDownText.text = "1";	
		}
		//if animation is in the last 75-100% 
		if (m_stateInfo.normalizedTime >= 0.75f && m_stateInfo.normalizedTime <= 1.0f) {
			m_countDownText.text = "GO!";	
		}

		//if animation is at the end of it's duration
		if (m_stateInfo.normalizedTime >= 1.0f) {
			//kill object and animation since we no longer need it
			Destroy (gameObject, 3.0f);
		}
	}
}
