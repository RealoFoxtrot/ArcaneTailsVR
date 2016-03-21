using UnityEngine;
using System.Collections;

public class SimpleAgent : MonoBehaviour {

    public Vector3 Target;
    NavMeshAgent agent;
    Rigidbody rb;
    Collider col;
    public Transform Floor;
    GameObject HitObject;


    
    private float boomForce = 1000;
    private float DistanceToEnemy;


    public float boomRadius = 2f;
    public GameObject BoomPos;
    float timer = 0;
    float hitTimer = 0;
    public float DisToEn;
    public GameObject enemy;
    public bool BeenHit = false;
    public GameObject[] spawns;

    //Respawn System
    public int lives;
    private GameObject spawn1;
    private GameObject spawn2;
    private GameObject spawn3;
    private GameObject spawn4;
    private int randoSpawn;

	// Use this for initialization
	void Start () {

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        rb = GetComponent<Rigidbody>();
        if (Floor)
        {
            Physics.IgnoreCollision(Floor.GetComponent<Collider>(), GetComponent<Collider>());
        }


        enemy = EnemyArrayTracker.EnemyArray[Random.Range(0,2)];
        while(enemy.name == name || enemy == null)
        {
            enemy = EnemyArrayTracker.EnemyArray[Random.Range(0, 2)];
        }
        //Location of the SpawnPossitions Set in an array
        spawns = GameObject.FindGameObjectsWithTag("Spwan");

        //Location of the SpawnPossitions Set
        /*spawn1 = GameObject.Find("Spawn1");
        spawn2 = GameObject.Find("Spawn2");
        spawn3 = GameObject.Find("Spawn3");
        spawn4 = GameObject.Find("Spawn4");
        */

     }
	
	// Update is called once per frame
	void Update () {
        Target = enemy.transform.position;
        DistanceToEnemy = Vector3.Distance(transform.position, Target);




        if (BeenHit)
        {
            agent.enabled = false;
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.None;
            agent.updatePosition = false;
            agent.updateRotation = false;

        }

        
           



        //print(DistanceToEnemy);
        // Timer countdown to the AI hitting the player.
        hitTimer += 1 * Time.deltaTime;
        if (hitTimer > 5) // every 5 seconds
        {
            HitPlayer();
            hitTimer = 0;
        }


        if (transform.position.y < 0 || rb.velocity.y > 0.5f)
        {
            agent.enabled = false;


        }
        


        if (!agent.enabled )
        {

            timer += 1.0f * Time.deltaTime;
            
           

            if (timer > 4 && agent.transform.position.y > 0)
            {
                agent.enabled = true;
                rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                agent.updatePosition = true;
                agent.SetDestination(Target);
                timer = 0;
            }

        }
	
	}



    void FixedUpdate()
    {

        


        if (agent.enabled)
        {

            Vector3 targetLookAt = Target - transform.position;

            Quaternion AILook = Quaternion.LookRotation(targetLookAt);


            rb.MoveRotation(AILook);
            agent.SetDestination(Target);
        }

        if (transform.position.y < -10)
        {
            if (lives > 1)
            {
                lives = lives - 1;
               // randoSpawn = Random.Range(1, 4);

                // grab random spwan point in array.
                transform.position = spawns[Random.Range(0, spawns.Length)].transform.position;

            }

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
            else
            {
                //for Enemies
                HitObject = hit.gameObject;

                if (hit.gameObject.tag != "Untagged" && hit.attachedRigidbody != null)
                {
                    hit.attachedRigidbody.constraints = RigidbodyConstraints.None;
                    hit.attachedRigidbody.AddExplosionForce(boomForce * hit.attachedRigidbody.mass, BoomPos.transform.position, boomRadius, 0.1f);
                }
           }

          
        }





    }


    
}
