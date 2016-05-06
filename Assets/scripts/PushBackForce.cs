using UnityEngine;
using System.Collections;

public class PushBackForce : MonoBehaviour {

    private Vector3 boomPosition;
    private float boomMultiplier;
    public float boomRadius;

    public GameObject AttackParticleFlash;

    Vector3 explosionPos;
    Collider[] colliders;
    GameObject[] Enemies;
    NavMeshAgent agent;

    Animator playerAnim;

    // Use this for initialization
    void Start() {

        explosionPos = transform.position;

        colliders = Physics.OverlapSphere(explosionPos, boomRadius);

        playerAnim = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        //update positions 
        colliders = Physics.OverlapSphere(explosionPos, boomRadius);
        explosionPos = transform.position;

        // player pushback
        if (Input.GetButtonDown("Fire"))
        {

            StartCoroutine("AttackParticle");
            foreach (Collider hit in Physics.OverlapSphere(transform.position, boomRadius))
            {


                if (hit.attachedRigidbody != null && hit.gameObject.tag == "TestAttacker")
                {
                    hit.attachedRigidbody.AddExplosionForce(2000, explosionPos, boomRadius, 0.1f);
                }

                    if (hit.attachedRigidbody != null && hit.gameObject.tag == "Attacker")

                {

                    agent = hit.gameObject.GetComponent<NavMeshAgent>();
                    hit.gameObject.GetComponent<SimpleAgent>().BeenHit = true;


                    //agent.updatePosition = false;
                    //agent.updateRotation = false;
                    
                    //agent.enabled = false;
                   // hit.attachedRigidbody.isKinematic = false;
                   // hit.attachedRigidbody.constraints = RigidbodyConstraints.None;

                    hit.attachedRigidbody.AddExplosionForce(2000, explosionPos, boomRadius, 0.1f);

                }

                StartCoroutine("AttackParticle");

            }

        }



    }

    IEnumerator AttackParticle()
    {
        yield return new WaitForEndOfFrame();
        playerAnim.SetTrigger("Attack");
        AttackParticleFlash.transform.position = transform.position;
        AttackParticleFlash.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        AttackParticleFlash.SetActive(false);
    }

    void FixedUpdate()
    {

      


        



    }
}
