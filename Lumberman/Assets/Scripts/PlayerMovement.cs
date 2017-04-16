using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    public int score;
    public float speedTimer = 3.0f;
    private PhotonView myPhotonView;
    public bool activateTimer = false;

    public Vector3 initialPosition;

    AudioSource audio;
    public LayerMask layer;
    public float speed = 4.0f;
    Rigidbody rigidbody;
    RaycastHit ray;
    private Vector3 changePos;
    private Vector3 checkUp;
    private Vector3 checkRight;
    [SerializeField] AudioClip chomp;
    public bool check;
	// Use this for initialization
    public GameObject playerLocation;

	void Start () 
    {
        audio = GetComponent<AudioSource>();
        initialPosition = new Vector3(-3.28f, 0.947f, 3.08f);
        myPhotonView = GetComponent<PhotonView>();
        QualitySettings.vSyncCount = 0;
        rigidbody = GetComponent<Rigidbody>();
        changePos.z = transform.position.z;
        checkUp = Vector3.forward.normalized;
        checkRight = Vector3.right.normalized;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (myPhotonView.isMine)
            Movement();

        if (transform.position.x > 12.42)
        {
            check = true;
            changePos.x = -16.29f;
            transform.position = new Vector3(changePos.x, transform.position.y, transform.position.z);
        }

        else if (transform.position.x < -16.29)
        {
            check = true;
            changePos.x = 12.42f;
            transform.position = new Vector3(changePos.x, transform.position.y, transform.position.z);
        }


        if (activateTimer == true)
        {
            speedTimer -= Time.deltaTime;
            if (speedTimer <= 0.0f)
                activateTimer = false;
        }

        else if (activateTimer == false)
        {
            speed = 4.0f;
            speedTimer = 3.0f;
        }

       // myPhotonView.RPC("OnTriggerEnter"

            
    }
    void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Physics.Raycast(this.transform.position, -checkUp, out ray, 0.5f, layer))
                Debug.Log("Saw wall!");
            else
                transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Physics.Raycast(this.transform.position, checkUp, out ray, 0.5f, layer))
                Debug.Log("Saw wall!");
            else
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (Physics.Raycast(this.transform.position, checkRight, out ray, 0.5f, layer))
                Debug.Log("Saw wall!");
            else
                transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (Physics.Raycast(this.transform.position, -checkRight, out ray, 0.5f, layer))
                Debug.Log("Saw wall!");
            else
                transform.Translate(-Vector3.right * Time.deltaTime * speed);
        }
    }
    void OnGUI()
    {
        GUILayout.Label("Player score: " + score);
    }

    [PunRPC]
    public void playAudio()
    {
        AudioSource audioRPC = gameObject.AddComponent<AudioSource>();
        audioRPC.clip = chomp;
        audioRPC.spatialBlend = 1;
        audioRPC.minDistance = 25;
        audioRPC.maxDistance = 100;
        audioRPC.Play();
    } 

    void OnTriggerEnter(Collider collider)
    {
       
        if (collider.gameObject.tag == "TileNode")
        {
            playerLocation = collider.gameObject;
        }


        if (collider.gameObject.tag == "Ghost1"|| collider.gameObject.tag == "Ghost2" || collider.gameObject.tag == "Ghost3")
        {
            myPhotonView.transform.position = initialPosition;
        }
            
        


        if (collider.gameObject.tag == "Pellet")
        {
            playAudio();
            PhotonNetwork.Destroy(collider.gameObject);
            score += 5;
        }
           

        else if (collider.gameObject.tag == "PowerPellet")
        {
            playAudio();
            PhotonNetwork.Destroy(collider.gameObject);
            score += 5;
            speed *= 2.0f;
            activateTimer = true;
        }

    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
            stream.SendNext(transform.position);
        else
            transform.position = (Vector3)stream.ReceiveNext();
    }
}
