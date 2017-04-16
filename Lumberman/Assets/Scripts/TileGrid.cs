using UnityEngine;
using System.Collections;

public class TileGrid : MonoBehaviour
{
    public GameObject parent;
    public LayerMask layer;

    GameObject temp;
    int i = 0;
    int x = 0;
    Vector3 direction;
    Vector3 directionX;
    Vector3 directionZ;
    public bool processed;

    float distance;
    RaycastHit ray;
    public float[,] nodeCoords;
    public GameObject[] tileNodes;

    public float timer;
    

    // Use this for initialization
    void Start()
    {

        tileNodes = GameObject.FindGameObjectsWithTag("TileNode");
        for (int x = 0; x < tileNodes.Length; x++)
        {
            for (int y = 0; y < tileNodes.Length; y++)
            {

                if (tileNodes[x].transform.position != tileNodes[y].transform.position)
                {
                    direction = (tileNodes[y].transform.position - tileNodes[x].transform.position).normalized;
                    directionX = Vector3.right.normalized;
                    directionZ = Vector3.forward.normalized;
                    //Debug.Log(direction);

                    if (Physics.Raycast(tileNodes[x].transform.position, directionZ, out ray, 2))
                    {
                        if (ray.transform == tileNodes[y].transform)
                        {
                            //Debug.DrawLine(tileNodes[x].transform.position, tileNodes[y].transform.position, Color.green, 100f);
                            tileNodes[x].GetComponent<TileNeighbours>().neighbourNodes.Add(tileNodes[y]);
                        }

                    }

                    if (Physics.Raycast(tileNodes[x].transform.position, -directionZ, out ray, 2))
                    {
                        if (ray.transform == tileNodes[y].transform)
                        {
                            //Debug.DrawLine(tileNodes[x].transform.position, tileNodes[y].transform.position, Color.green, 100f);
                            tileNodes[x].GetComponent<TileNeighbours>().neighbourNodes.Add(tileNodes[y]);
                        }
                    }

                    if (Physics.Raycast(tileNodes[x].transform.position, -directionX, out ray, 2))
                    {
                        if (ray.transform == tileNodes[y].transform)
                        {
                            //Debug.DrawLine(tileNodes[x].transform.position, tileNodes[y].transform.position, Color.green, 100f);
                            tileNodes[x].GetComponent<TileNeighbours>().neighbourNodes.Add(tileNodes[y]);
                        }
                    }

                    if (Physics.Raycast(tileNodes[x].transform.position, directionX, out ray, 2))
                    {
                        if (ray.transform == tileNodes[y].transform)
                        {
                            //Debug.DrawLine(tileNodes[x].transform.position, tileNodes[y].transform.position, Color.green, 100f);
                            tileNodes[x].GetComponent<TileNeighbours>().neighbourNodes.Add(tileNodes[y]);
                        }
                    }
                }
            }
        }



    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0)
        {
            GetComponent<PathfindingTileGrid>().enabled = true;
            GetComponent<PathfindingTileGrid1>().enabled = true;
            GetComponent<PathfindingTileGrid2>().enabled = true;
        }
    }
}
