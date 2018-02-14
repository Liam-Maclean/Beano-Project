using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataDisplay : MonoBehaviour {

    #region UI Objects
    public Text score;
    public Text effect;
    public Image av;


    public Text score1;
    public Text effect1;
    public Image av1;

    public Text score2;
    public Text effect2;
    public Image av2;

    public Text score3;
    public Text effect3;
    public Image av3;
    #endregion

    #region players

    private static List<GameScript> opponents;

    #endregion

    private void Awake()
    {
        score = GameObject.Find("Score").GetComponent<Text>();
        effect = GameObject.Find("Effect").GetComponent<Text>();

        score1 = GameObject.Find("Score1").GetComponent<Text>();
        effect1 = GameObject.Find("Effect1").GetComponent<Text>();

        score2 = GameObject.Find("Score2").GetComponent<Text>();
        effect2 = GameObject.Find("Effect2").GetComponent<Text>();

        score3 = GameObject.Find("Score3").GetComponent<Text>();
        effect3 = GameObject.Find("Effect3").GetComponent<Text>();
    }

    private void Start()
    {
        Repeat:
        GameScript gameScript = GameScript.local;
        GameScript[] players = FindObjectsOfType(typeof(GameScript)) as GameScript[];
        if (players.Length != gameScript.playerCount)
        {
            goto Repeat;
        }
        foreach (GameScript player in players)
        {
            if (player.playerDetails.Identifier != gameScript.playerDetails.Identifier)
            {
                opponents.Add(player);
            }
        }
        
    }

    private void Update()
    {
        score.text = GameScript.local.playerDetails.MiniScore.ToString();
        score1.text = opponents[0].playerDetails.MiniScore.ToString();
        score2.text = opponents[1].playerDetails.MiniScore.ToString();
        score3.text = opponents[2].playerDetails.MiniScore.ToString();
        score.text = "testval";
    }
}
