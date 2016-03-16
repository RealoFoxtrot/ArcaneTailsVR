using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CameraAim : MonoBehaviour {

    public Camera playerCamera;
    public Vector3 hitTransform;
    public GameObject hitCube;
    public GameObject Player;
    public GameObject CurrentHit;
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

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (CurrentHit)
            {
                if (CurrentHit.name == "StartBox" && Input.GetButton("Jump"))
                {

                    SceneManager.LoadScene("Level Layout Test");

                }
            }
            
        }
        //print(CurrentHit);
        if (Physics.Raycast(ray, out hit, 1000, floorMask))
        {

               
                hitTransform = new Vector3(hit.point.x, hit.point.y, hit.point.z);



        }

        if (Physics.Raycast(ray, out hit, 1000))
        {
            CurrentHit = hit.collider.gameObject;
           
            print(hit.collider.gameObject);
        }





            // make the object appear at the hit point.

            hitCube.transform.position = hitTransform;

        
      

    }
}
