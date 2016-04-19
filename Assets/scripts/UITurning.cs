using UnityEngine;
using System.Collections;

public class UITurning : MonoBehaviour {

    public GameObject Camera;

    private int lives;
    private Vector3 cameraLocation;
    private Vector3 pointAtCamera;
	
	// Update is called once per frame
	void Update () {

        lives = GetComponentInParent<SimpleAgent>().lives;


        GetComponent<TextMesh>().text = "Lives: " + lives;
	}

    void FixedUpdate()
    {
        cameraLocation = Camera.transform.position;


        transform.LookAt( 2 * transform.position - cameraLocation) ;
    }
}
