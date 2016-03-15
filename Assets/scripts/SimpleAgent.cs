using UnityEngine;
using System.Collections;

public class SimpleAgent : MonoBehaviour {

    public Transform Target;
    NavMeshAgent agent;
    Rigidbody rb;
    Collider col;
    public Transform Floor;
    GameObject HitObject;


    
    private float boomForce = 1000;
    public float boomRadius = 2f;
    public GameObject BoomPos;
    float timer = 0;
    float hitTimer = 0;

	// Use this for initialization
	void Start () {

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        rb = GetComponent<Rigidbody>();
        if (Floor)
        {
            Physics.IgnoreCollision(Floor.GetComponent<Collider>(), GetComponent<Collider>());
        }
     }
	
	// Update is called once per frame
	void Update () {

        // Timer countdown to the AI hitting the player.
        hitTimer += 1 * Time.deltaTime;
        if (hitTimer > 5) // every 5 seconds
        {
            HitPlayer();
            hitTimer = 0;
        }
        
            
        
        


        if (!agent.enabled )
        {

            timer += 1.0f * Time.deltaTime;
            
           

            if (timer > 4 && agent.transform.position.y < 1.5f)
            {
                agent.enabled = true;
                rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                agent.updatePosition = true;
                agent.SetDestination(Target.transform.position);
                timer = 0;
            }

        }
	
	}



    void FixedUpdate()
    {

        


        if (agent.enabled)
        {

            Vector3 targetLookAt = Target.transform.position - transform.position;

            Quaternion AILook = Quaternion.LookRotation(targetLookAt);


            rb.MoveRotation(AILook);
            agent.SetDestination(Target.transform.position);
        }

        


    }


    void OnCollisionEnter(Collision col)
    {

        if (agent.enabled == false && col.gameObject.tag == "Floor")
        {
            agent.enabled = true;

        }



    }

   

    void HitPlayer()
    {


        foreach (Collider hit in Physics.OverlapSphere(BoomPos.transform.position, boomRadius))
        {



            if (hit.attachedRigidbody != null && hit.gameObject.tag == "Player") // if it is the player, then hit.
            {
                print("Hitting Player");
                HitObject = hit.gameObject;

                HitObject.GetComponent<PinballMovement>().beenhit = true;
                hit.attachedRigidbody.constraints = RigidbodyConstraints.None;
                hit.attachedRigidbody.AddExplosionForce(boomForce * hit.attachedRigidbody.mass, BoomPos.transform.position, boomRadius, 0.1f);

            }

          
        }





    }


    
}
