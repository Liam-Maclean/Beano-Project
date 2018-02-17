using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Floating Text Manager Script
/// 
/// This script creates indicator text whenever something happens (such as score increase)
/// 
/// Is a public static class, which means it does not need to be declared, only called
/// 
/// Liam MacLean 17/02/2018, 15:56
/// </summary>


//floating text manager class
public class FloatingTextManager : MonoBehaviour {

	//variables
	private static FloatingText m_floatingText;
	private static GameObject m_canvas;

	//public static method to initialise class
	public static void Initialise()
	{
		//finds game canvas
		m_canvas = GameObject.Find ("MinigameCanvas");

		//if the text hasn't been initialised, initialise the text with the prefab
		if (!m_floatingText) {
			m_floatingText = Resources.Load<FloatingText> ("Minigames/PlantMinigame/Prefabs/IndicatorTextParent");
		}
	}
		
	//Public static metod to create floating text (default method yellow text)
	public static void CreateFloatingText(string text, Transform position)
	{
		FloatingText instance = Instantiate (m_floatingText);
		Vector2 screenPosition = Camera.main.WorldToScreenPoint (position.position);
		instance.transform.SetParent (m_canvas.transform, false);
		instance.transform.position = screenPosition;
		instance.SetText (text);
	}

	//public static method to create floating Text (Override method, color can be chosen)
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
