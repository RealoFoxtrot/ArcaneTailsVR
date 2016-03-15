using UnityEngine;
using System.Collections;

public class Turning : MonoBehaviour {


    private Rigidbody rb;
    private Vector3 cameraLocation;
    private Vector3 crossHair;
    private Vector3 pointAtCrossHair;

    public GameObject Camera;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        

    }
	
	// Update is called once per frame
	void FixedUpdate () {


        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PinballMovement>().beenhit == true)
        {

        }
        else
        {
            //get the location of the crosshair from the script in the camera
            crossHair = Camera.GetComponent<CameraAim>().hitTransform;

            //create a vector between the crosshair and the player
            pointAtCrossHair = crossHair - transform.position;
            //pointAtCrossHair.y = 0.3f;

            Quaternion turnPlayer = Quaternion.LookRotation(pointAtCrossHair);

            rb.MoveRotation(turnPlayer);
        }

	}
}
