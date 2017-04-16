using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAll : MonoBehaviour 
{
    public float timer;
	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer -= Time.deltaTime;

        if (timer <= 6.00f)
            GameObject.FindGameObjectWithTag("Ghost1").GetComponent<AIBehaviour>().enabled = true;

        if (timer <= 3.00f)
            GameObject.FindGameObjectWithTag("Ghost2").GetComponent<AIBehaviour1>().enabled = true;

        //if (timer <= 0.00f)
        //    GameObject.FindGameObjectWithTag("Ghost3").GetComponent<AIBehaviour2>().enabled = true;
		
	}
}
