using UnityEngine;
using System.Collections;

public class AttackingPoint : MonoBehaviour {


    public bool inRange = false;
    public bool blastSpellFired = false;
    public Vector3 attackLocation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        attackLocation = transform.position;

        if (Input.GetButtonDown("Fire") == true)
        {
            blastSpellFired = true;
            //playanimation 

        }
        else
        {
            blastSpellFired = false;
        }



    }

    void OnTriggerEnter (Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }


    void OnTriggerExit(Collider other)
    {

        if (other.GetComponent<Collider>().tag == "Player")
        {
            inRange = false;
        }


    }

}
