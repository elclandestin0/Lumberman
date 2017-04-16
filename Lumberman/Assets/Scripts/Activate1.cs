using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate1 : MonoBehaviour 
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
            GetComponent<AIBehaviour1>().enabled = true;

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
