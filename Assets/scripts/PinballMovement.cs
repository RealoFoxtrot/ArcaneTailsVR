using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PinballMovement : MonoBehaviour
{
    //Public Adjustables
    public float speed = 2f;
    public float jumpHeight = 2f;
    public bool beenhit = false;
    public float boomForce = 10f;
    public float boomRadius = 1f;
    public Transform Floor;
    public GameObject Camera;
    public GameObject respawnParticle;
    public float animationSpeed;

    //Private Information
    private Rigidbody rb;
    private float horizontal;
    private float vertical;
    private Vector3 movement;
    private Vector3 cameraLocation;
    private Vector3 LookLocation;
    private Vector3 pointAtCamera;
    private Vector3 boomPosition;
    private float boomMultiplier;
    private ParticleSystem respawnParticleSystem;
    private Vector3 lastPosition;

    //Jump Control
    private bool jump;
    public bool CanJump;

    //Respawn System
    public int lives = 3;
    private GameObject spawn1;
    private GameObject spawn2;
    private GameObject spawn3;
    private GameObject spawn4;
    private int randoSpawn;

    //Animators
    Animator playerAnim;

    //Colliders and explosion
    Vector3 explosionPos;
    Collider[] colliders;
    
  


    private float timer = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        explosionPos = transform.position;

        //Location of the SpawnPossitions Set
        spawn1 = GameObject.Find("Spawn1");
        spawn2 = GameObject.Find("Spawn2");
        spawn3 = GameObject.Find("Spawn3");
        spawn4 = GameObject.Find("Spawn4");

        // TODO: add diffrent types of player movement to test out.
        CanJump = false;
         colliders = Physics.OverlapSphere(explosionPos, 6);
        Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), GameObject.Find("Floor").GetComponent<MeshCollider>());

        //work around for layer based colliders clashing with raycasting.
        // Physics.IgnoreLayerCollision(0, 9);

        playerAnim = GameObject.FindGameObjectWithTag("Rat").GetComponent<Animator>();

    }

    void Update()
    {
        // test code for quitting application
        if (Input.GetKey(KeyCode.Escape))
        {

            Application.Quit();

        }

        if (Input.GetButton("Reset"))
        {
            SceneManager.LoadScene("MainMenu");


        }


    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jump = Input.GetButtonDown("Jump");
        
        
        boomMultiplier = boomForce * 500;

        animationSpeed = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;
        playerAnim.SetFloat("ParentSpeed", animationSpeed);

        // swap between stunned and physics pinball
        if (beenhit == true)
        {
            timer += 1.0f * Time.deltaTime;
            
            pinballhit();
        }
        else
        {
            timer = 0;
            physicsPinballControl();

        }

        // stuntime
        if (timer >= 2)
        {

            beenhit = false;
        }

        

        if (transform.position.y < -5)
        {
            lives -= 1;
            if (lives > 0)
            {
                //lives -= 1;
                randoSpawn = Random.Range(1, 4);
                if (randoSpawn == 1)
                { transform.position = spawn1.transform.position; }
                else if (randoSpawn == 2)
                { transform.position = spawn2.transform.position; }
                else if (randoSpawn == 3)
                { transform.position = spawn3.transform.position; }
                else if (randoSpawn == 4)
                { transform.position = spawn4.transform.position; }
                StartCoroutine(Respawn());

            }
            else
            {
                lives = 0;
            }




           

        }

    }
    IEnumerator Respawn()
    {
        yield return new WaitForEndOfFrame();
        respawnParticle.transform.position = transform.position;
        respawnParticle.SetActive(true);
        yield return new WaitForSeconds(3);
        respawnParticle.SetActive(false);
    }

    //Disable the controls and start spinning the player
    void pinballhit()
    {
        rb.constraints = RigidbodyConstraints.None;
        //rb.AddExplosionForce(boomMultiplier, boomPosition, boomRadius, 0.1f);
    }


    void parentturning()
    {

        //Stop Changing this! The Player Rotates around The Camera, NOT The Crosshair
        cameraLocation = Camera.transform.position;
        cameraLocation.y = transform.position.y;

        //create a vector between the Camera and the player
        pointAtCamera = cameraLocation - transform.position;


        Quaternion turnPlayer = Quaternion.LookRotation(pointAtCamera);

        rb.MoveRotation(turnPlayer);
    }

    void physicsPinballControl()
    {
        parentturning();
        
        if (horizontal > 0)
        {

            rb.AddRelativeForce(horizontal * speed * rb.mass * 500 * Time.deltaTime, 0, horizontal * speed * rb.mass * 250 * Time.deltaTime);
        }

        if (horizontal < 0)
        {

            rb.AddRelativeForce(horizontal * speed * rb.mass * 500 * Time.deltaTime, 0, horizontal * speed * rb.mass * -250 * Time.deltaTime);
        }

        if (vertical != 0)
        {

            rb.AddRelativeForce(0, 0, vertical * speed * rb.mass * 500 * Time.deltaTime);
        }

        if (CanJump && Input.GetButton("Jump"))
        {
            rb.AddForce(0, jumpHeight * 5000, 0);
        }
        
    }



    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Level")
        {

            CanJump = true;

        }
        

    }

    void OnCollisionExit(Collision col)
    {

        if (col.gameObject.tag == "Level")
        {

            CanJump = false;

        }



    }

}