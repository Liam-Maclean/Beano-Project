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

    private static List<GameObject> opponents = new List<GameObject>();

    #endregion

    void Awake()
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

   void Start()
    {
        

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
       
        foreach (GameObject player in players)
        {
            if (player.GetComponent<CustomLobby>().playerDetails.Identifier != CustomLobby.local.playerDetails.Identifier)
            {
                opponents.Add(player);
            }
        }
        
    }

    void Update()
    {
        CustomLobby.local.Score(20);

        foreach(GameObject opponent in opponents)
        {
            CustomLobby.local.SendDetailsRequestForNetId(opponent.GetComponent<CustomLobby>().playerDetails.Identifier);
        }

        score.text = CustomLobby.local.playerDetails.MiniScore.ToString();
        score1.text = opponents[0].GetComponent<CustomLobby>().playerDetails.MiniScore.ToString();
        score2.text = opponents[1].GetComponent<CustomLobby>().playerDetails.MiniScore.ToString();
        score3.text = opponents[2].GetComponent<CustomLobby>().playerDetails.MiniScore.ToString();

        

    }
}
