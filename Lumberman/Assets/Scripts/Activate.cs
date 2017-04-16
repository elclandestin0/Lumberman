using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour 
{
    public GameObject[] Nodes;
    public GameObject[] Pellets;
    public GameObject Node;
    public float timer = 3.0f;
	// Use this for initialization
	void Start ()
    {
        Nodes = GameObject.FindGameObjectsWithTag("TileNode");
	}
	
	// Update is called once per frame
	void Update () 
    {
        


        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
           GameObject.FindGameObjectWithTag("Ghost1").GetComponent<AIBehaviour>().enabled = true;
        }

        if (timer <= -3.0f)
        {
            GameObject.FindGameObjectWithTag("Ghost2").GetComponent<AIBehaviour1>().enabled = true;
        }

        if (timer <= -6.0f)
        {
            GameObject.FindGameObjectWithTag("Ghost3").GetComponent<AIBehaviour2>().enabled = true;
        }



       
	}

    void OnTriggerEnter(Collider collider)
    {
        for (int i = 0; i < Nodes.Length; i++)
        {
            if (collider.gameObject == Nodes[i])
            {
                Node = Nodes[i];
            }

            
        }

    }
}
