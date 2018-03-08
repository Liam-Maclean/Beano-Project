using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour {

    int scoreChange;
    bool direction; // true for clockwise
    int i;
    int scoreMultiplier;
    float prevRot;
    public GameObject wrench;

	// Use this for initialization
	void Start () {
        i = 0;
        direction = true;
        scoreMultiplier = 1;
        prevRot = -90;
	}
	
	// Update is called once per frame
	void Update () {
        scoreChange = 0;
        scoreChange = Mathf.RoundToInt(wrench.transform.rotation.z - prevRot);
        scoreChange *= scoreMultiplier;
        if (i > (Random.value * 1000))
        {
            direction = !direction;
            scoreMultiplier *= -1;
            i = 0;
        }
        else i++;
        CustomLobby.local.Score(scoreChange);
	}
}
