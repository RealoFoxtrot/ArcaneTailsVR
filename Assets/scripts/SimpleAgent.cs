using UnityEngine;
using System.Collections;

public class SimpleAgent : MonoBehaviour {

    public Vector3 Target;
    NavMeshAgent agent;
    Rigidbody rb;
    Collider col;
    public Transform Floor;
    GameObject HitObject;
    public GameObject EnemyDebug;
    public GameObject respawnParticle;



    private float boomForce = 200;
    private float DistanceToEnemy;


    public float boomRadius = 2f;
    public GameObject BoomPos;
    float timer = 0;
    float RespawnTimer = 0;
    float waitTimer = 0;
    float hitTimer = 0;
    public float DisToEn;
    public GameObject enemy;
    public bool BeenHit = false;
    public GameObject[] spawns;
    public GameObject LivesText;

    //Respawn System
    public int lives;
    private int randoSpawn;
    //Enums
    enum EnemyState {Moving, Attacking, Attacked};
    // Use this for initialization

    void Awake() {
        DontDestroyOnLoad(enemy);
        

    }

	void Start () {


        waitTimer = 5;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        rb = GetComponent<Rigidbody>();
        if (Floor)
        {
            Physics.IgnoreCollision(Floor.GetComponent<Collider>(), GetComponent<Collider>());
        }


       
        if(!enemy)
        {
            enemy = EnemyArrayTracker.EnemyList[Random.Range(0, EnemyArrayTracker.EnemyList.Count)];
        }
        //Location of the SpawnPossitions Set in an array
        spawns = GameObject.FindGameObjectsWithTag("Spawn");

      
     }
	
	// Update is called once per frame
	void Update () {

        if (waitTimer > 0)
        {
            //wait for 5 seconds;
            agent.updatePosition = false;
            agent.enabled = false;
            waitTimer -= 1.0f * Time.deltaTime;

        }
        else {
            agent.updatePosition = true;
            agent.enabled = true;

        }
        
        // if there is an enemy and the enemy's tag is player then make that the target
        if (enemy && enemy.tag == "Player")
        {
            if (enemy.GetComponent<PinballMovement>().lives == 0 || enemy.name == name)
            {
                enemy = EnemyArrayTracker.EnemyList[Random.Range(0, EnemyArrayTracker.EnemyList.Count)];
                EnemyDebug = enemy;

            }

        }

        // if enemy
        if (enemy && enemy.tag == "Attacker")
        {
            if (enemy.name == name || lives == 0) //enemy.GetComponent<SimpleAgent>().lives == 0) Huh...
            {
                //testing
                // cast to a gameobject for the arraylist
                enemy = EnemyArrayTracker.EnemyList[Random.Range(0, EnemyArrayTracker.EnemyList.Count)];
                EnemyDebug = enemy;
            }
        }

        //set target
        if (enemy)
        {
            Target = enemy.transform.position;
            DistanceToEnemy = Vector3.Distance(transform.position, Target);

        }
        

        if (Input.GetButton("Reset"))
        {
            //reset enemies
            transform.position = spawns[Random.Range(0, spawns.Length)].transform.position;
        }


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
        hitTimer += 1 * Time.deltaTime + Random.Range(0.0f,0.5f);
        if (hitTimer > 5 && !BeenHit) // every 5 seconds
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
                BeenHit = false;
                agent.enabled = true;
                rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                agent.updatePosition = true;
                agent.SetDestination(Target);
                timer = 0;
            }

        }

        if (transform.position.y < -10)
        {
            lives -= 1;


            if (lives > 0)
            {
                
                

                // grab random spwan point in array.
                transform.position = spawns[Random.Range(0, spawns.Length)].transform.position;
                agent.enabled = true;
                rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                agent.updatePosition = true;
                agent.SetDestination(Target);

                RespawnParticles();

            }
            else {
                //disable renderer when dead? 
                agent.updatePosition = false;
                agent.updateRotation = false;
                GetComponent<Collider>().attachedRigidbody.detectCollisions = false;
                
            }

        }

    }

    

    void RespawnParticles()
    {
        RespawnTimer += 1.0f * Time.deltaTime;
        respawnParticle.transform.position = transform.position;
        respawnParticle.SetActive(true);
        if (RespawnTimer > 3)
        {
            RespawnTimer = 0;
            respawnParticle.SetActive(false);

        }

    }

    void FixedUpdate()
    {

        


        if (agent.enabled)
        {

            Vector3 targetLookAt = Target - transform.position;
            targetLookAt.y = 0;
            Quaternion AILook = Quaternion.LookRotation(targetLookAt);


            rb.MoveRotation(AILook);
            agent.SetDestination(Target);
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

                if (hit.gameObject.tag == "Attacker" && hit.attachedRigidbody != null && hit.gameObject.name != name)
                {
                    HitObject.GetComponent<SimpleAgent>().BeenHit = true;
                    hit.attachedRigidbody.constraints = RigidbodyConstraints.None;
                    hit.attachedRigidbody.AddExplosionForce(boomForce * hit.attachedRigidbody.mass, BoomPos.transform.position, boomRadius, 0.1f);
                }
           }

          
        }





    }


    
}
