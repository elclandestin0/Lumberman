using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBehaviour1 : MonoBehaviour
{
    private PhotonView myPhotonView; 
    private float seekSpeed = 3f;
    private float relativeSpeed;
    private float acceleration = 0.05f;
    private float velocity = 0.0f;
    public Transform currentTarget;
    public List<GameObject> targetNodes;
    public GameObject goalFromClickedNode;

    void Start()
    {
        Debug.Log("Activated");
        myPhotonView = GetComponent<PhotonView>();
        //this.transform.position = GameObject.FindGameObjectWithTag("Connections").GetComponent<PathfindingTileGrid>().startingNode.transform.position;
        goalFromClickedNode = GameObject.FindGameObjectWithTag("Connections").GetComponent<PathfindingTileGrid1>().goalNode;
        relativeSpeed = 1f;
    }
    void Update()
    {
        targetNodes = GameObject.FindGameObjectWithTag("Connections").GetComponent<PathfindingTileGrid1>().finalPath;

        FollowPath();
    }
    void SeekTarget()
    {
        Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(currentTarget.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        float realSpeed = seekSpeed * relativeSpeed;
        transform.Translate(direction * (realSpeed + acceleration) * Time.deltaTime, Space.World);
    }
    void ChangeTarget()
    {
        if (targetNodes.Count == 0)
        {
            //Debug.Log("Pretty Princess Cupcake has reached her destination or gotten lost.");
        }
        else
        {
            currentTarget = targetNodes[0].GetComponentInParent<Transform>();
            targetNodes.RemoveAt(0);
            //Debug.Log(currentTarget.transform.position);
        }
    }
    void FollowPath()
    {
        if (goalFromClickedNode != null)
        {
            if (targetNodes.Count == 0)
            {
                goalFromClickedNode = null;
            }
        }
        if ((currentTarget == null) ||
            ((transform.position - currentTarget.position).sqrMagnitude < 1.0f))
        {
            //GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            transform.Translate(0.0f, 0.0f, 0.0f);
            ChangeTarget();
        }
        if (currentTarget != null)
        {
            SeekTarget();
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
            stream.SendNext(transform.position);
        else
            transform.position = (Vector3)stream.ReceiveNext();
    }

    void OnTriggerEnter(Collider collider)
    { 
    }
 
}
