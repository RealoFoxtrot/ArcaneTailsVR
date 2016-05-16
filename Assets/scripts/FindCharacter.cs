using UnityEngine;
using System.Collections;

public class FindCharacter : MonoBehaviour {

    private Rigidbody rb;
    public GameObject player;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.SetActive(true);
    }

    void Update()
    {

        if (player.GetComponent<PinballMovement>().lives <= 0 || EnemyArrayTracker.IsWinner)
        {

            gameObject.SetActive(false);

        }
       

    }

	// Update is called once per frame
	void FixedUpdate () {

        Vector3 playerLocation = player.transform.position;
        Vector3 PointAtPlayer = playerLocation - transform.position;

        Quaternion Finder = Quaternion.LookRotation(PointAtPlayer);

        rb.MoveRotation(Finder);
	}
}
