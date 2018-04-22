using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPortaitScript : MonoBehaviour {

    public GameObject portrait;
    public GameObject readyStatus;

    public List<Sprite> portraitList;
    public List<Sprite> readyList;
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        readyStatus.transform.position.Set(0, -200, 0);
        
	}
}
