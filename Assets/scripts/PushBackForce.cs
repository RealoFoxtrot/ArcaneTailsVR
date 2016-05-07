﻿using UnityEngine;
using System.Collections;

public class PushBackForce : MonoBehaviour {

    private Vector3 boomPosition;
    private float boomMultiplier;
    public float boomRadius;
    public float boomForce = 2000;

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
        if (Input.GetButton("Fire"))
        {

            
            foreach (Collider hit in Physics.OverlapSphere(transform.position, boomRadius))
            {


                if (hit.attachedRigidbody != null && hit.gameObject.tag == "TestAttacker")
                {
                    hit.GetComponent<TrainingEnemy>().isHit = true;
                    hit.attachedRigidbody.constraints = RigidbodyConstraints.None;
                    hit.attachedRigidbody.AddExplosionForce(boomForce, explosionPos, boomRadius, 0.1f);
                }

                    if (hit.attachedRigidbody != null && hit.gameObject.tag == "Attacker")

                {

                    agent = hit.gameObject.GetComponent<NavMeshAgent>();
                    hit.gameObject.GetComponent<SimpleAgent>().BeenHit = true;

                    hit.attachedRigidbody.AddExplosionForce(boomForce, explosionPos, boomRadius, 0.1f);

                }

            }

        }
        if (Input.GetButtonDown("Fire"))
        {
            StartCoroutine("AttackParticle");
        }



    }

    IEnumerator AttackParticle()
    {
        // Tread the road cross the abyss
        // Take a look down at the madness
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
