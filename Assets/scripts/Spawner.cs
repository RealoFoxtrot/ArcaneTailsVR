using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

   public GameObject ModelRoot;
   public Transform RootPos;
   

	// Use this for initialization
    //This script is just for testing purposes!
	void Start () {

        Instantiate(ModelRoot);
        
        ModelRoot.transform.parent = RootPos;
        ModelRoot.transform.localPosition = new Vector3(0, 1, 0);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
