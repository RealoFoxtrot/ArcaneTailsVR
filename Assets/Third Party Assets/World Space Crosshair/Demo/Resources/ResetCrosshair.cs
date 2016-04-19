using UnityEngine;
using System.Collections;

public class ResetCrosshair : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetAxis("Fire1") != 0)
            ResetCrosshairLoc();
	}

    void ResetCrosshairLoc()
    {
        transform.localEulerAngles = Vector3.zero;
    }
}
