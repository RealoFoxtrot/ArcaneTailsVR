using UnityEngine;
using System.Collections;

public class UITurning : MonoBehaviour {

    public GameObject Camera;

    private int lives;
    private Vector3 cameraLocation;
    private Vector3 pointAtCamera;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {

        lives = GetComponentInParent<SimpleAgent>().lives;


        GetComponent<TextMesh>().text = "Lives: " + lives;
	}

    void FixedUpdate()
    {
        cameraLocation = Camera.transform.position;
        

        //create a vector between the Camera and the player
        pointAtCamera = cameraLocation - transform.position;


        Quaternion turnUI = Quaternion.LookRotation(pointAtCamera);

        rb.MoveRotation(turnUI);
    }
}
