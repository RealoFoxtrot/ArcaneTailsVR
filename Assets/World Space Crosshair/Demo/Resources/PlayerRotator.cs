using UnityEngine;
using System.Collections;

public class PlayerRotator : MonoBehaviour 
{
    public float rotationSpeed;


	// Use this for initialization
	void Start () 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        if (Input.GetKey(KeyCode.E))
            transform.localEulerAngles += new Vector3(0, rotationSpeed * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.Q))
            transform.localEulerAngles -= new Vector3(0, rotationSpeed * Time.deltaTime, 0);

	}
}
