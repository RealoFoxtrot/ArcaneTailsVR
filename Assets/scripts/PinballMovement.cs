using UnityEngine;
using System.Collections;

public class PinballMovement : MonoBehaviour
{

    public float speed = 2f;
    public float jumpHeight = 2f;
    public bool beenhit = false;
    public float boomForce = 10f;
    public float boomRadius = 1f;
    public Transform Floor;

    private Rigidbody rb;
    private float horizontal;
    private float vertical;
    private Vector3 movement;
    private Vector3 cameraLocation;
    private Vector3 LookLocation;
    
    private Vector3 pointAtCamera;
    private Vector3 boomPosition;
    private float boomMultiplier;
    private bool jump;

    public bool CanJump;
    public GameObject Camera;



    Vector3 explosionPos;
    Collider[] colliders;
    GameObject[] Enemies;
   public Collider[] IgnoreColliders;


    private float timer = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();


         explosionPos = transform.position;



        CanJump = false;
         colliders = Physics.OverlapSphere(explosionPos, 6);
        //Physics.IgnoreCollision(Floor.GetComponent<Collider>(), GetComponent<Collider>());

        //work around for layer based colliders clashing with raycasting.
        if (IgnoreColliders.Length > 0)
        {
            for (int i = 0; i < IgnoreColliders.Length; i++)
            {

                Physics.IgnoreCollision(IgnoreColliders[i], GetComponent<Collider>());

            }
        }


       // Physics.IgnoreLayerCollision(0, 9);
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Escape))
        {

            Application.Quit();

        }


    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jump = Input.GetButtonDown("Jump");
        
        
        boomMultiplier = boomForce * 1000;




        // swap between world control and physics pinball
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

        if (timer >= 2)
        {

            beenhit = false;
        }

    }

    //Disable the controls and start spinning the player
    void pinballhit()
    {
        rb.constraints = RigidbodyConstraints.None;
        //rb.AddExplosionForce(boomMultiplier, boomPosition, boomRadius, 0.1f);
    }

    //Tony's physics based movement converted to Elina's game - not used DELETE
    void physicsPinballControl()
    {
        if (horizontal != 0)
        {

           // rb.AddRelativeForce(horizontal * speed * rb.mass * -500 * Time.deltaTime, 0, 0);
            rb.AddRelativeForce(horizontal * speed * rb.mass * 500 * Time.deltaTime, 0, 0);
        }

        if (vertical != 0)
        {

          //  rb.AddRelativeForce(0, 0, vertical * speed * rb.mass * -500 * Time.deltaTime);
            rb.AddRelativeForce(0, 0, vertical * speed * rb.mass * 500 * Time.deltaTime);
        }

        if (jump == true && CanJump)
        {
            rb.AddForce(0, jumpHeight * 10000, 0);
        }
        parentturning();
    }

 

    void parentturning()
    {
        //get the location of the camera
        LookLocation = Camera.GetComponent<CameraAim>().hitTransform;
        cameraLocation.y = transform.position.y;

        //create a vector between the crosshair and the player
        pointAtCamera = LookLocation - transform.position;
        //pointAtCrossHair.y = 0.3f;

        Quaternion turnPlayer = Quaternion.LookRotation(pointAtCamera);

        rb.MoveRotation(turnPlayer);
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Floor")
        {

            CanJump = true;

        }

    }

    void OnCollisionExit(Collision col)
    {

        if (col.gameObject.tag == "Floor")
        {

            CanJump = false;

        }



    }

}