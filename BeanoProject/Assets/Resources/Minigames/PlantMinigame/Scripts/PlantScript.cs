using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;


public enum PlantType
{
	normal,
	bonus,
	water,
	dirt
}

public class PlantScript : MonoBehaviour {

	public PlantType plantType = PlantType.normal;
	public PlantType changedType = PlantType.normal;
	public bool bActive = true;
	public Sprite normalSprite, bonusSprite, dirtSprite, waterSprite;
	private SpriteRenderer sr;

	private Dictionary<PlantType, Sprite> dictTxr = new Dictionary<PlantType, Sprite>();

	float fRechargeTimer;


	public PlantType GetPlantType()
	{
		return plantType;
	}

	public void SeedPlantType()
	{
	}
		
	void Start()
	{
		dictTxr.Add (PlantType.normal, normalSprite);
		dictTxr.Add (PlantType.bonus, bonusSprite);
		dictTxr.Add (PlantType.water, waterSprite);
		dictTxr.Add (PlantType.dirt, dirtSprite);

		sr = GetComponent<SpriteRenderer> ();
		Sprite tempSprite = new Sprite();
		dictTxr.TryGetValue (PlantType.normal, out tempSprite);

		sr.sprite = tempSprite;
	}

	void Update()
	{
		if (plantType != changedType) {

			plantType = changedType;
		}
	}



}
