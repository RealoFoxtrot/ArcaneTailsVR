using UnityEngine;
using System.Collections;

public class CameraAim : MonoBehaviour {

    public Camera playerCamera;
    public Vector3 hitTransform;
    public GameObject hitCube;
    public GameObject Player;
    int floorMask;

	// Use this for initialization
	void Start () {
         
        playerCamera = GetComponent<Camera>();
        hitTransform = new Vector3();
        floorMask = LayerMask.GetMask("Floor");
    }
	
	// Update is called once per frame
	void Update () {

        //Vector3 Worldhit = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, playerCamera.farClipPlane));
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

        if (Physics.Raycast(ray, out hit, 100, floorMask))
        {


                hitTransform = new Vector3(hit.point.x, hit.point.y, hit.point.z);



        }
        float x = Input.mousePosition.x / 2;
        float y = Input.mousePosition.y / 2;
       

        //make camera move with mouse for a bit.



        // make the object appear at the hit point.
        //transform.rotation = Quaternion.Euler(y, x, 0);
        hitCube.transform.position = hitTransform;
      

    }
}
