using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TileNeighbours : MonoBehaviour
{
    public List<GameObject> neighbourNodes;
    public float nodeCost;
    public float heuristicCost;

    public float finalCost
    {
        get { return nodeCost + heuristicCost; }
    }

    public GameObject parent;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
