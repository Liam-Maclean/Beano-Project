using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FloatingText : MonoBehaviour {

	public Animator m_animator;
	private Text m_indicatorText;

	// Use this for initialization
	void Awake () {
		AnimatorClipInfo[] clipInfo = m_animator.GetCurrentAnimatorClipInfo (0);
		Destroy (gameObject, clipInfo [0].clip.length);
		m_indicatorText = m_animator.GetComponent<Text> ();
	}

	//set string in text
	public void SetText(string text)
	{
		m_indicatorText.text = text;
	}

	//set color of indicator text
	public void SetColor(Color color)
	{
		m_indicatorText.color = color;
	}

}
