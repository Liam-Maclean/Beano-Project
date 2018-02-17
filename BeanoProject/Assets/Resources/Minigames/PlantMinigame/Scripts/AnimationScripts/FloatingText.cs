using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Floating text class
/// 
/// Used as a baseline for the public static utility class used to create indicator text on the screen
/// 
/// Contains an animator to animate the floating text, and a text component
/// 
/// Liam MacLean 17/02/2018 15:59
/// </summary>

public class FloatingText : MonoBehaviour {

	//animation for floating text
	public Animator m_animator;

	//text component
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
