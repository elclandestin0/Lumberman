using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PathfindingTileGrid1 : MonoBehaviour
{
    public GameObject startingNode;
    public GameObject goalNode;
    public GameObject[] players;
    public GameObject currentNode;
    public List<GameObject> finalPath;
    public GameObject[] tileNodes;
    private GameObject temp;

    private List<GameObject> neighbourNodes;

    public Material startMaterial;
    public Material pathMaterial;
    public Material goalMaterial;

    // Use this for initialization
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player1");
        tileNodes = GameObject.FindGameObjectsWithTag("TileNode");
        startingNode = GameObject.FindGameObjectWithTag("Ghost2").GetComponent<Activate>().Node;
        goalNode = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerMovement>().playerLocation;
        //startingNode.GetComponent<Renderer>().material = pathMaterial;
        //goalNode.GetComponent<Renderer>().material = goalMaterial;

        Euclidean();
        //Djikstra();
       
    }

    public void Euclidean()
    {

        List<GameObject> openList = new List<GameObject>();
        List<GameObject> closedList = new List<GameObject>();

        openList.Add(startingNode);

        while (openList.Count > 0)
        {
            //Debug.Log(openList[0].transform.name);
            
            currentNode = openList[0];
            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].GetComponent<TileNeighbours>().finalCost < currentNode.GetComponent<TileNeighbours>().finalCost || openList[i].GetComponent<TileNeighbours>().finalCost == currentNode.GetComponent<TileNeighbours>().finalCost)
                {
                    if (openList[i].GetComponent<TileNeighbours>().heuristicCost < currentNode.GetComponent<TileNeighbours>().heuristicCost)
                        currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);


            foreach (GameObject neighbour in currentNode.GetComponent<TileNeighbours>().neighbourNodes)
            {
                if (closedList.Contains(neighbour))
                {
                    continue;
                }
                float newCostToNeighbour = currentNode.GetComponent<TileNeighbours>().nodeCost + Vector3.Distance(currentNode.transform.position, neighbour.transform.position);
                if (newCostToNeighbour < neighbour.GetComponent<TileNeighbours>().nodeCost || !openList.Contains(neighbour))
                {
                    neighbour.GetComponent<TileNeighbours>().nodeCost = newCostToNeighbour;
                    neighbour.GetComponent<TileNeighbours>().heuristicCost = Vector3.Distance(neighbour.transform.position, goalNode.transform.position);
                    neighbour.GetComponent<TileNeighbours>().parent = currentNode;

                    if (!openList.Contains(neighbour))
                        openList.Add(neighbour);
                }
            }



            if (currentNode == goalNode)
            {
                goalNode.GetComponent<Renderer>().material = goalMaterial;
                RetracePath(startingNode, goalNode);
                return;
            }

        }
    }

    

    void RetracePath(GameObject startNode, GameObject endNode)
    {

        finalPath = new List<GameObject>();
        GameObject currentNode = endNode;

        while (currentNode != startNode)
        {
            Debug.DrawLine(currentNode.transform.position, currentNode.GetComponent<TileNeighbours>().parent.transform.position, Color.green, 200f);
            finalPath.Add(currentNode);
            currentNode = currentNode.GetComponent<TileNeighbours>().parent;
        }
        finalPath.Reverse();
    }
    // Update is called once per frame
    void Update()
    {
        startingNode = GameObject.FindGameObjectWithTag("Ghost2").GetComponent<Activate>().Node;
        
        goalNode = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerMovement>().playerLocation;
        Euclidean();
    }
}
