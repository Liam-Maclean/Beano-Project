using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAnimationScript : MonoBehaviour {

	//animator and animation state info (time info)
	public Animator m_animator;
	private AnimatorStateInfo m_stateInfo;

	// Use this for initialization
	void Start () {
		AnimatorClipInfo[] clipInfo = m_animator.GetCurrentAnimatorClipInfo (0);
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
	}
}
