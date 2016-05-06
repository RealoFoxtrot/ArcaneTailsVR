using UnityEngine;
using System.Collections;

public class TrainingEnemy : MonoBehaviour {

    // Use this for initialization
    GameObject[] spawnPositions;
    Rigidbody rb;
	void Start () {
        Physics.IgnoreCollision(GetComponent<SphereCollider>(), GameObject.Find("Floor").GetComponent<MeshCollider>());
        rb = GetComponent<Rigidbody>();
        spawnPositions = GameObject.FindGameObjectsWithTag("Spawn");
    }
	

	void Update () {

        //unlimited lives for the simplified balls
        if (transform.position.y <= -5)
        {
            Vector3 spanwPos = spawnPositions[Random.Range(0, spawnPositions.Length)].transform.position;
            transform.position = new Vector3(spanwPos.x, spanwPos.y - 0.4f, spanwPos.z);
            rb.velocity = new Vector3(0, 0, 0);

        }
	
	}
}
