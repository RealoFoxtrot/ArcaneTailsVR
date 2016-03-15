using UnityEngine;
using System.Collections;

public class PushBackForce : MonoBehaviour {

    private Vector3 boomPosition;
    private float boomMultiplier;
    public float boomRadius;

    Vector3 explosionPos;
    Collider[] colliders;
    GameObject[] Enemies;
    NavMeshAgent agent;


    // Use this for initialization
    void Start() {

        explosionPos = transform.position;

        colliders = Physics.OverlapSphere(explosionPos, boomRadius);

    }

    // Update is called once per frame
    void Update() {

        //update positions 
        colliders = Physics.OverlapSphere(explosionPos, boomRadius);
        explosionPos = transform.position;

        // player pushback
        if (Input.GetButton("Fire"))
        {
            foreach (Collider hit in Physics.OverlapSphere(transform.position, boomRadius))
            {


                if (hit.attachedRigidbody != null && hit.gameObject.tag == "Attacker")
                {

                    agent = hit.gameObject.GetComponent<NavMeshAgent>();



                    agent.updatePosition = false;
                    agent.updateRotation = false;
                    
                    agent.enabled = false;
                    hit.attachedRigidbody.isKinematic = false;
                    hit.attachedRigidbody.constraints = RigidbodyConstraints.None;





                    hit.attachedRigidbody.AddExplosionForce(2000, explosionPos, boomRadius, 0.1f);




                }
            }

        }



    }

    void FixedUpdate()
    {

      


        



    }
}
