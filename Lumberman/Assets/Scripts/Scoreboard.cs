using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour 
{
    GameObject[] players;
    public GameObject[] pellets;
    bool player1Won;
    bool player2Won;
    List<int> playerScores = new List<int>();

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
        players = GameObject.FindGameObjectsWithTag("Player1");
        pellets = GameObject.FindGameObjectsWithTag("Pellet");
        foreach (GameObject g in players)
        {
              playerScores.Add(g.GetComponent<PlayerMovement>().score);
        }
		
        if (pellets.Length == 0)
        {
            Debug.Log("no more pellets bro!");
            for (int i = 0; i < playerScores.Count; i++)
            {
                for (int j = 0; j < playerScores.Count; j++)
                {
                    if (i != j)
                    {
                        if (playerScores[i] > playerScores[j])
                            player1Won = true;
                        else if (playerScores[i] < playerScores[j])
                            player2Won = true;
                    }
                }
            }
        }
	}

    void OnGUI()
    {
        if (player1Won == true)
            GUILayout.Label("Player 1 Won!!");
        else if (player2Won == true)
            GUILayout.Label("Player 2 Won!!");       
    }
}
