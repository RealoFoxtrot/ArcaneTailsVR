using UnityEngine;
using System.Collections;

public class EnemyPinballMovement : MonoBehaviour
{

    public float speed = 15f;
    public bool beenhit = false;
    public float boomForce = 10f;
    public float boomRadius = 1f;

    private Rigidbody rb;
    private float horizontal;
    private float vertical;
    private Vector3 movement;

    private Vector3 Target;

    private bool IsHitting;
    private Vector3 pointAtCrossHair;
    private Vector3 boomPosition;
    private float boomMultiplier;


    public GameObject HitObject;


    private float timer = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        boomPosition = GameObject.Find("Attacker").transform.position;
        boomMultiplier = boomForce * 1000;

       

        


        // swap between world control and physics pinball
        if (beenhit == true)
        {
            timer += 1.0f * Time.deltaTime;
            pinballhit();
        }
        else
        {
            //pinballcontrol(horizontal, vertical);
            timer = 0;
            physicsPinballControl();
        }

        if (timer >= 2)
        {

            beenhit = false;
        }

    }

    void Update()
    {
        

    }

    //Disable the controls and start spinning the player
    void pinballhit()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.AddExplosionForce(boomMultiplier, boomPosition, boomRadius, 0.1f);
    }

    //World Co-Ordinate Controlled Movement
    void pinballcontrol(float horizontal, float vertical)
    {
        // Set the movement vector based on the axis input.
        movement.Set(horizontal, 0f, vertical);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        rb.MovePosition(transform.position + movement);

        //turn the player towards the crosshair
        turning();
    }

    //Tony's physics based movement converted to Elina's game

    //Replace this for AI, basic face towards player and move.
    void physicsPinballControl()
    {
        /* if (horizontal > 0 || horizontal < 0)
         {
             rb.AddForce(horizontal * speed * 1000 * Time.deltaTime, 0, 0);
         }
         if (vertical > 0 ||vertical < 0)
         {
             rb.AddForce(0, 0, vertical * speed * 1000 * Time.deltaTime);
         }*/


        rb.AddRelativeForce(0, 0, 1000 * Time.deltaTime);

        turning();
    }


    void turning()
    {
        //get the location of the crosshair from the script in the camera
        Target = GameObject.Find("PlayerSphere").transform.position;

        //create a vector between the crosshair and the player
        pointAtCrossHair = Target - transform.position;
        //pointAtCrossHair.y = 0.3f;



        Quaternion turnPlayer = Quaternion.LookRotation(pointAtCrossHair);

        rb.MoveRotation(turnPlayer);
    }


    


    
}