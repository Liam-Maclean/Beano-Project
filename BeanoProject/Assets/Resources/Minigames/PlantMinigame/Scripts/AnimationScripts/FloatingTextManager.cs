using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour {
	private static FloatingText m_floatingText;
	private static GameObject m_canvas;

	public static void Initialise()
	{
		m_canvas = GameObject.Find ("MinigameCanvas");

		if (!m_floatingText) {
			m_floatingText = Resources.Load<FloatingText> ("Minigames/PlantMinigame/Prefabs/IndicatorTextParent");
		}
	}
		
	public static void CreateFloatingText(string text, Transform position)
	{
		FloatingText instance = Instantiate (m_floatingText);
		Vector2 screenPosition = Camera.main.WorldToScreenPoint (position.position);
		instance.transform.SetParent (m_canvas.transform, false);
		instance.transform.position = screenPosition;
		instance.SetText (text);
	}

	public static void CreateFloatingText(string text, Transform position, Color color)
	{
		FloatingText instance = Instantiate (m_floatingText);
		Vector2 screenPosition = Camera.main.WorldToScreenPoint (position.position);
		instance.transform.SetParent (m_canvas.transform, false);
		instance.transform.position = screenPosition;
		instance.SetText (text);
		instance.SetColor (color);
	}
}
