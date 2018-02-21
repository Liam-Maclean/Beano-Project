using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour {

	public void Score()
    {
        CustomLobby.local.Score(10);
    }

    public void Powerup()
    {

    }
}
